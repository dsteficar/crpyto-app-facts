const BinanceChartManager = (function () {

    let chart = null;
    let candlestickSeries = null;
    let earliestTimestamp = null;
    let historicalData = [];
    let debounceTimeout = null;
    let dotNetObjectRef = null; 

    let currentToolId = null;
    let currentToolDatabaseId = null;
    let currentEditHandler = null;
    let channelIdMap = new Map(); 

    const BINANCE_API_URL = 'https://api.binance.com/api/v3/klines';

    /**
     * Fetch historical candlestick data from Binance API.
     */
    async function fetchHistoricalData(symbol, interval, limit = 500, endTime = null) {
        try {
            const params = {
                symbol: symbol.toUpperCase(),
                interval: interval,
                limit: limit,
            };

            if (endTime) {
                params.endTime = endTime - 1;
            }

            const response = await axios.get(BINANCE_API_URL, { params });

            const data = response.data.map(candle => ({
                time: candle[0] / 1000,
                open: parseFloat(candle[1]),
                high: parseFloat(candle[2]),
                low: parseFloat(candle[3]),
                close: parseFloat(candle[4]),
            }));

            if (data.length > 0) {
                earliestTimestamp = data[0].time;
            }

            return data;
        } catch (error) {
            console.error('Error fetching historical data:', error);
            return [];
        }
    }

    /**
     * Connect to Binance WebSocket to get real-time candlestick updates.
     */
    function connectWebSocket(symbol, candlestickSeries) {
        const ws = new WebSocket(`wss://stream.binance.com:9443/ws/${symbol.toLowerCase()}@kline_1m`);

        ws.onmessage = (event) => {
            const message = JSON.parse(event.data);
            const candlestick = message.k;
            updateCandlestickSeries(candlestick, candlestickSeries);
        };

        ws.onclose = () => console.log('WebSocket closed');

        return ws;
    }

    /**
     * Update the candlestick series with new data.
     */
    function updateCandlestickSeries(candlestick, candlestickSeries) {
        candlestickSeries.update({
            time: candlestick.t / 1000,
            open: parseFloat(candlestick.o),
            high: parseFloat(candlestick.h),
            low: parseFloat(candlestick.l),
            close: parseFloat(candlestick.c),
        });
    }

    /**
     * Initialize the chart with symbol, interval, and dark mode theme.
     */
    async function initializeChart(selectedSymbol, selectedInterval = '1m', isDarkMode = true) {
        clearExistingChart();

        const chartContainer = document.getElementById('chart');

        if (chartContainer == null) return;

        const timeScaleSettings = getTimeScaleSettings(selectedInterval);

        chart = createChart(chartContainer, isDarkMode, timeScaleSettings);

        candlestickSeries = chart.addCandlestickSeries();
        historicalData = await fetchHistoricalData(selectedSymbol, selectedInterval);
        candlestickSeries.setData(historicalData);

        zoomToMostRecentData(historicalData);

        setupChartEvents(selectedSymbol, selectedInterval);  
        connectRealTimeWebSocket(selectedSymbol);

        const chartLoaderText = document.getElementById('chart-loading-text');

        if (chartLoaderText) {
            chartLoaderText.remove();  // Remove the loading indicator
        }
    }

    /**
     * Clear existing chart and WebSocket if present.
     */
    function clearExistingChart() {
        if (chart) {
            chart.remove();
            chart = null;
        }
        if (window.currentWebSocket) {
            window.currentWebSocket.close();
            window.currentWebSocket = null;
        }
        candlestickSeries = null;
        historicalData = [];
        earliestTimestamp = null;
    }

    /**
     * Create chart with dark mode options and custom time scale settings.
     */
    function createChart(container, isDarkMode, timeScaleSettings) {
            const darkThemeOptions = {
                layout: {
                    backgroundColor: '#1e1e1e',
                    textColor: '#ffffff',
                },
                grid: {
                    vertLines: { color: '#2B2B43' },
                    horzLines: { color: '#363c4e' },
                },
                priceScale: { borderColor: '#485c7b' },
                timeScale: { borderColor: '#485c7b', ...timeScaleSettings }
            };

            return LightweightCharts.createChart(container, {
                width: container.clientWidth,
                height: 500,
                ...darkThemeOptions,
            });
    }

    /**
     * Zoom to the most recent data in the chart.
     */
    function zoomToMostRecentData(historicalData) {
        const latestTimestamp = historicalData[historicalData.length - 1].time;
        chart.timeScale().setVisibleRange({
            from: latestTimestamp - (60 * 60 * 24), // 24 hours back
            to: latestTimestamp,
        });
    }

    /**
     * Setup events for time range and window resizing.
     */
    function setupChartEvents(selectedSymbol, selectedInterval) {
        chart.timeScale().subscribeVisibleTimeRangeChange(() => {
            onTimeRangeChange(selectedSymbol, selectedInterval);
        });
        window.addEventListener('resize', onResize);
        document.getElementById('deleteParallelChannelButton').addEventListener('click', deleteParallelChannelTool);
        document.getElementById('drawParallelChannelButton').addEventListener('click', () => {
            drawParallelChannelTool();  // Calls the function with no points (default is empty array)
        });
    }

    /**
     * Get the appropriate time scale settings based on the selected interval.
     * @param {string} interval - The selected interval (e.g., '1m', '1h', '1d', etc.).
     */
    function getTimeScaleSettings(interval) {
        const lastChar = interval.slice(-1);

        switch (lastChar) {
            case 'm':  // Minute-level intervals
                return {
                    timeVisible: true,
                    secondsVisible: true,
                    tickMarkFormatter: (time) => {
                        const date = new Date(time * 1000);
                        const hours = date.getUTCHours();
                        const minutes = date.getUTCMinutes();
                        return `${hours}:${minutes < 10 ? '0' : ''}${minutes}`; // Show hours and minutes
                    }
                };

            case 'h':  // Hour-level intervals
                return {
                    timeVisible: true,
                    secondsVisible: true,
                    tickMarkFormatter: (time) => {
                        const date = new Date(time * 1000);
                        const hours = date.getUTCHours();
                        return `${hours}:00`;  // Show only the hours
                    }
                };

            case 'd':  // Day-level intervals
            case 'w':  // Week-level intervals
                return {
                    timeVisible: true,
                    secondsVisible: false,
                    tickMarkFormatter: (time) => {
                        const date = new Date(time * 1000);
                        const day = date.getUTCDate();
                        const month = date.getUTCMonth() + 1;
                        const year = date.getUTCFullYear();
                        return `${day}-${month}-${year}`;  // Show day-month-year
                    }
                };

            case 'M':  // Month-level intervals
                return {
                    timeVisible: true,
                    secondsVisible: false,
                    tickMarkFormatter: (time) => {
                        const date = new Date(time * 1000);
                        const month = date.getUTCMonth() + 1;
                        const year = date.getUTCFullYear();
                        return `${month}-${year}`;  // Show month-year
                    }
                };

            default:
                return {
                    timeVisible: true,
                    secondsVisible: false,
                    tickMarkFormatter: (time) => {
                        const date = new Date(time * 1000);
                        return date.toUTCString();  // Default to full UTC string if unknown interval
                    }
                };
        }
    }

    /**
     * Handle time range changes to fetch more data.
     */
    function onTimeRangeChange(selectedSymbol, selectedInterval) {
        clearTimeout(debounceTimeout);

        debounceTimeout = setTimeout(async () => {
            const currentVisibleRange = chart.timeScale().getVisibleRange();

            if (currentVisibleRange && currentVisibleRange.from <= earliestTimestamp) {
                const moreData = await fetchHistoricalData(
                    selectedSymbol,
                    selectedInterval,
                    500,
                    earliestTimestamp * 1000
                );
                if (moreData.length > 0) {
                    const nonOverlappingData = moreData.filter(dataPoint => dataPoint.time < historicalData[0].time);
                    historicalData = [...nonOverlappingData, ...historicalData];
                    candlestickSeries.setData(historicalData);
                    earliestTimestamp = historicalData[0].time;
                }
            }
        }, 300);
    }

    /**
     * Draw or reload a parallel channel tool. 
     * If points are provided, it reloads the tool, otherwise it creates a new one.
     * @param {Array} points - The points to define the channel. If empty or undefined, creates a new tool.
     */
    function drawParallelChannelTool(points = [], dbChannelId = null) {

        if (chart !== null) {
            chart.addLineTool("ParallelChannel", points, {
                "channelLine": {
                    "color": "rgba(0,254,0,1)",
                    "width": 1,
                    "end": { "left": 0, "right": 0 },
                    "extend": { "right": false, "left": false }
                },
                "middleLine": {
                    "color": "rgba(0,0,0,1)",
                    "width": 1,
                    "end": { "left": 0, "right": 0 },
                    "extend": { "right": false, "left": false }
                },
                "showMiddleLine": true,
                "extend": { "right": false, "left": false },
                "background": {
                    "color": "rgba(80,227,194,0.1)",
                    "inflation": { "x": 0, "y": 0 }
                },
                "visible": true,
                "editable": true
            });

            const lineTools = JSON.parse(chart.exportLineTools());
            currentToolId = lineTools[lineTools.length - 1].id;

            if (dbChannelId !== null) {
                channelIdMap.set(dbChannelId, currentToolId);  // Map the database ID to the chart tool ID
            }

            //channelIdMap.forEach((toolId, dbId) => {
            //    console.log(`Database Channel ID: ${dbId}, Chart Tool ID: ${toolId}`);
            //});

            manageToolEditing();
        }
    }

    function addEntryToChannelIdMap(dbChannelId, currentToolId) {
        if (dbChannelId !== null && currentToolId !== null) {
            channelIdMap.set(dbChannelId, currentToolId);
            console.log(`Added to map: Database Channel ID = ${dbChannelId}, Chart Tool ID = ${currentToolId}`);
        } else {
            console.error("dbChannelId or currentToolId is null. Cannot add to map.");
        }
    }

    function initializeParallelChannels(channels) {
        for (const [dbId, points] of Object.entries(channels)) {
            drawParallelChannelTool(points, dbId);  // Use the points to draw the channel

            // Now you can store the mapping between dbId and the tool created
/*            console.log(`Channel with DB ID: ${dbId} has been added to the chart.`);*/
        }

        //channels.forEach(channel => {
        //    drawParallelChannelTool([channel[0], channel[1], channel[2]]);
        //});
    }

    /**
     * Manage tool editing by subscribing or unsubscribing to edit events.
     */
    function manageToolEditing() {
        if (currentEditHandler) {
            chart.unsubscribeLineToolsAfterEdit(currentEditHandler);
        }

        currentEditHandler = () => handleToolEdit(currentToolId);
        chart.subscribeLineToolsAfterEdit(currentEditHandler);
    }

    /**
     * Handle tool editing events.
     */
    function handleToolEdit(toolId) {
        //const lineTools = JSON.parse(chart.exportLineTools());
        //const tool = lineTools.find(tool => tool.id === toolId);

        const selectedLineTool = chart.getSelectedLineTools();

        if (selectedLineTool === null) return;

        let selectedLineToolsArray = JSON.parse(selectedLineTool);

        const selectedLineToolId = selectedLineToolsArray[0].id;
        const selectedLineToolPoints = selectedLineToolsArray[0].points;
        let selectedLineToolDatabaseId = null;

        for (let [key, value] of channelIdMap.entries()) {
            if (value === selectedLineToolId) {
                selectedLineToolDatabaseId = key;
                break;
            }
        }

        if (selectedLineToolPoints.length === 3) {
            dotNetObjectRef.invokeMethodAsync('HandleParallelChannelSave', selectedLineToolPoints, selectedLineToolId, selectedLineToolDatabaseId)
                .then(() => console.log("Blazor C# method: Added new parallel channel"))
                .catch(error => console.error("Error invoking C# method", error));
        }
    }

    /**
     * Delete the selected parallel channel tool.
     */
    function deleteParallelChannelTool() {
        const theSelectedLineTool = chart.getSelectedLineTools();

        if (theSelectedLineTool) {
            let selectedToolsArray = JSON.parse(theSelectedLineTool);

            const selectedToolId = selectedToolsArray[0].id; // Assuming the first tool is selected

            console.log("ID: " + selectedToolId);

            // Find the dbChannelId by looking up the selectedToolId in the map's values
            let dbChannelId = null;
            for (let [key, value] of channelIdMap.entries()) {
                if (value === selectedToolId) {
                    dbChannelId = key;
                    break;
                }
            }

            if (dbChannelId !== null) {
                dotNetObjectRef.invokeMethodAsync('HandleParallelChannelDelete', dbChannelId)
                    .then(() => console.log(`Blazor C# method: Removed parallel channel with database ID ${dbChannelId}`))
                    .catch(error => console.error("Error invoking C# method", error));
            } else {
                console.error("No matching DB Channel ID found for the selected tool.");
            }

            chart.removeSelectedLineTools(); // Remove the tool from the chart
        }
    }

    /**
     * Load external script and invoke callback after load.
     */
    function loadScript(src, callback) {
        const script = document.createElement('script');
        script.src = src;
        script.onload = callback;
        document.head.appendChild(script);
    }

    /**
     * Scroll to a channel using start and end timestamps.
     */
    async function scrollToChannel(startTimestamp, endTimestamp) {
        const visibleRange = chart.timeScale().getVisibleRange();
        const middleTimestamp = (startTimestamp + endTimestamp) / 2;
        const currentRange = visibleRange.to - visibleRange.from;

        chart.timeScale().setVisibleRange({
            from: middleTimestamp - currentRange / 2,
            to: middleTimestamp + currentRange / 2
        });
    }

    /**
     * Resize chart when window size changes.
     */
    function onResize() {
        const newWidth = document.getElementById('chart').clientWidth;
        chart.resize(newWidth, 500);
    }

    /**
     * Connect to WebSocket for real-time data.
     */
    function connectRealTimeWebSocket(symbol) {
        const ws = connectWebSocket(symbol, candlestickSeries);
        window.currentWebSocket = ws;
    }

    function disposeChart() {
        if (window.currentWebSocket) {
            window.currentWebSocket.close();  // Close WebSocket
            window.currentWebSocket = null;
        }

        if (currentEditHandler) {
            chart.unsubscribeLineToolsAfterEdit(currentEditHandler);  // Unsubscribe from any tool edits
            currentEditHandler = null;
        }

        // Clear any intervals or timeouts
        if (debounceTimeout) {
            clearTimeout(debounceTimeout);
            debounceTimeout = null;
        }

        // Remove event listeners
        window.removeEventListener('resize', onResize);
        document.getElementById('deleteParallelChannelButton')?.removeEventListener('click', deleteParallelChannelTool);
        document.getElementById('drawParallelChannelButton')?.removeEventListener('click', drawParallelChannelTool);

        dotNetObjectRef = null; // Clean up the reference

        if (chart) {
            chart.remove();
            chart = null;
        }
        console.log("Disposing CAHRT");

        delete window.BinanceChartManager;

    }
    function loadDependencies(callback) {
        loadScript('/js/lightweight-charts.standalone.production.js', () => {
            loadScript('/js/axios.min.js', callback);
        });
    }

    function loadChartEnvironment(dotNetObjectReference, selectedSymbol) {
        try {
            dotNetObjectRef = dotNetObjectReference;
            loadDependencies(() => {
                initializeChart(selectedSymbol);
            });
        }
        catch {

        }
    }

    function loadChartEnvironment(dotNetObjectReference, selectedSymbol, channels = {}) {
        try {
        dotNetObjectRef = dotNetObjectReference;

        loadDependencies(() => {
            initializeChart(selectedSymbol);

            if (Object.keys(channels).length > 0) {
                initializeParallelChannels(channels);  // Pass the channels dictionary to the function
            }
        });
        }
        catch {

        }
    }

    return {
        initializeChart,
        scrollToChannel,
        addEntryToChannelIdMap,
        disposeChart,
        loadChartEnvironment
    };

})();

window.BinanceChartManager = BinanceChartManager;