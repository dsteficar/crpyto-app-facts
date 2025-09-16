using Application.DTOs.ParallelChannel.Requests;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using Radzen;
using WebApplicationUI.Models.Trading.Graphs;
using WebApplicationUI.States;


namespace WebApplicationUI.Pages.Trading.Graphs
{
    public partial class GraphManager
    {
        private DotNetObjectReference<GraphManager>? _componentReference;

        private List<UserParallelChannelModel> _parallelChannels = new();
        private List<GetParallelChannelDTO> _existingChannels = new();
        private readonly List<string> _timeIntervals = new List<string> { "1m", "3m", "5m", "15m", "30m", "1h", "2h", "4h", "6h", "12h", "1d", "1w", "1M" };

        private IEnumerable<GetParallelChannelDTO>? _userParallelChannelList;
        private IEnumerable<string>? _futuresPairs;

        private Density channelPageDensity = Density.Compact;

        private LoadDataArgs _userParallelChannelListLoadSettings = new LoadDataArgs { Skip = 0, Top = 3 };

        private string _selectedFuturesPair = "";
        private string _selectedTimeInterval = "1m";

        private bool _allowVirtualization = false;
        private bool _isLoading;
        private int _count;
        private int _userParallelChannelPageSize = 3;

        private bool isComponentActive = true;
        private IDisposable? _locationHandlerRegistration;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private IJSObjectReference? module;

        protected override async Task OnInitializedAsync()
        {
            var customAuthStateProvider = (CustomAuthenticationStateProvider)AuthStateProvider;
            var authenticationState = await customAuthStateProvider.GetAuthenticationStateAsync();
            var user = authenticationState.User;

            if (!user.Identity.IsAuthenticated || user.IsInRole("Anonymous"))
            {
                // Redirect to login page
                navManager.NavigateTo("login", forceLoad: true);
            }
            else
            {
                // Subscribe to navigation events
                navManager.LocationChanged += OnLocationChanged;

                var response = await tradingViewGraphClientService.GetAllFuturesPairSymbolsAsync(_cancellationTokenSource.Token);
                _futuresPairs = response;
                _selectedFuturesPair = _futuresPairs.FirstOrDefault(x => x == "BTCUSDT") ?? "";
                _existingChannels = await tradingViewGraphClientService.FindChannelsByUserIdAndStartTimestamp(2, 123, _cancellationTokenSource.Token);

                module = await jsRuntime.InvokeAsync<IJSObjectReference>("import", new object[] { "./js/binance-chart.js" });
                _componentReference = DotNetObjectReference.Create(this);
                var channelJavaScriptDictionaryModel = PrepareParallelChannelJavaScriptDictionary(_cancellationTokenSource.Token);

                //Console.WriteLine(channelJavaScriptDictionaryModel);
                //await Task.Delay(200);
                if (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    await jsRuntime.InvokeVoidAsync("BinanceChartManager.loadChartEnvironment", _componentReference, _selectedFuturesPair, channelJavaScriptDictionaryModel);
                    await LoadUserParallelChannelData(_userParallelChannelListLoadSettings);
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {




                    //StateHasChanged();

                }
                catch (OperationCanceledException ex)
                {
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            await jsRuntime.InvokeVoidAsync("BinanceChartManager.disposeChart");
            if (module != null) await module.DisposeAsync();
            _componentReference?.Dispose();
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
        {
            // Cancel ongoing tasks when navigation occurs
            Console.WriteLine("Location changed, gotta run.");
            _cancellationTokenSource?.Cancel();
        }

        private Dictionary<int, ParallelChannelPointModel[]> PrepareParallelChannelJavaScriptDictionary(CancellationToken cancelToken)
        {
            cancelToken.ThrowIfCancellationRequested();

            var channelDictionary = new Dictionary<int, ParallelChannelPointModel[]>();

            foreach (var channel in _existingChannels)
            {
                channelDictionary[channel.Id] = new[]
                {
                    new ParallelChannelPointModel { Price = channel.Price1, Timestamp = channel.Timestamp1 },
                    new ParallelChannelPointModel { Price = channel.Price2, Timestamp = channel.Timestamp2 },
                    new ParallelChannelPointModel { Price = channel.Price3, Timestamp = channel.Timestamp3 }
                };
            }

            return channelDictionary;
        }

        private async Task OnFuturesPairChanged(object value)
        {
            if (value is string selectedSymbol && !string.IsNullOrEmpty(selectedSymbol))
            {
                _selectedFuturesPair = selectedSymbol;
                await jsRuntime.InvokeVoidAsync("BinanceChartManager.initializeChart", _selectedFuturesPair, _selectedTimeInterval);
            }
        }

        private async Task OnTimeIntervalChanged(object value)
        {
            if (value is string selectedInterval && !string.IsNullOrEmpty(selectedInterval))
            {
                _selectedTimeInterval = selectedInterval;
                await jsRuntime.InvokeVoidAsync("BinanceChartManager.initializeChart", _selectedFuturesPair, _selectedTimeInterval);
            }
        }

        private void ParallelChannelPageChanged(PagerEventArgs args)
        {
            _userParallelChannelList = _existingChannels.AsQueryable();

            HandleParallelChannelPaging(args.Skip, args.Top);

            _userParallelChannelList = _userParallelChannelList.AsODataEnumerable();
        }

        private void HandleParallelChannelPaging(int skip, int top)
        {

            if (skip != 0)
            {
                _userParallelChannelList = _userParallelChannelList?.Skip(skip);
            }

            if (top != 0)
            {
                _userParallelChannelList = _userParallelChannelList?.Take(top);
            }
        }

        private async Task LoadUserParallelChannelData(LoadDataArgs args)
        {
            if (_existingChannels == null) return;

            _isLoading = true;
            _userParallelChannelList = _existingChannels.AsQueryable();

            var skip = args.Skip ?? 0;
            var top = args.Top ?? 0;

            HandleParallelChannelPaging(skip, top);

            _count = _existingChannels.Count();
            _userParallelChannelList = _userParallelChannelList.AsODataEnumerable();
            _isLoading = false;
        }

        public async Task SaveChannels()
        {
            foreach (var channel in _parallelChannels)
            {
                var parallelChannelDTO = new AddParallelChannelRequestDTO
                {
                    UserId = 2,
                    Price1 = channel.Points[0].Price,
                    Timestamp1 = channel.Points[0].Timestamp,
                    Price2 = channel.Points[1].Price,
                    Timestamp2 = channel.Points[1].Timestamp,
                    Price3 = channel.Points[2].Price,
                    Timestamp3 = channel.Points[2].Timestamp,
                };

                await tradingViewGraphClientService.AddParallelChannelForUser(parallelChannelDTO, _cancellationTokenSource.Token);
            }

            return;
        }

        private async Task CenterOnChannel(decimal timestamp1, decimal timestamp2)
        {
            // Calculate the start and end timestamps
            var startTimestamp = Math.Min(timestamp1, timestamp2);
            var endTimestamp = Math.Max(timestamp1, timestamp2);

            // Call the JavaScript method using JSInterop to center the chart on the channel
            await jsRuntime.InvokeVoidAsync("BinanceChartManager.scrollToChannel", startTimestamp, endTimestamp);
        }

        [JSInvokable("HandleParallelChannelSave")]
        public async Task HandleParallelChannelSave(ParallelChannelPointModel[] points, string parallelChannelJsId, string parallelChannelDatabaseId)
        {
            if (string.IsNullOrEmpty(parallelChannelDatabaseId))
            {
                var parallelChannelDTO = new AddParallelChannelRequestDTO
                {
                    UserId = 2,
                    Price1 = points[0].Price,
                    Timestamp1 = points[0].Timestamp,
                    Price2 = points[1].Price,
                    Timestamp2 = points[1].Timestamp,
                    Price3 = points[2].Price,
                    Timestamp3 = points[2].Timestamp,
                };

                var response = await tradingViewGraphClientService.AddParallelChannelForUser(parallelChannelDTO, _cancellationTokenSource.Token);

                var userParallelChannel = new UserParallelChannelModel
                {
                    UserId = 2,
                    Points = points
                };

                _parallelChannels.Add(userParallelChannel);

                await jsRuntime.InvokeVoidAsync("BinanceChartManager.addEntryToChannelIdMap", response.Id.ToString(), parallelChannelJsId.ToString());
            }
            else
            {
                var parallelChannelDTO = new UpdateParallelChannelRequestDTO
                {
                    Id = int.Parse(parallelChannelDatabaseId),
                    UserId = 2,
                    Price1 = points[0].Price,
                    Timestamp1 = points[0].Timestamp,
                    Price2 = points[1].Price,
                    Timestamp2 = points[1].Timestamp,
                    Price3 = points[2].Price,
                    Timestamp3 = points[2].Timestamp,
                };

                await tradingViewGraphClientService.UpdateParallelChannel(parallelChannelDTO, _cancellationTokenSource.Token);
            }
        }

        [JSInvokable("HandleParallelChannelDelete")]
        public async Task HandleParallelChannelDelete(string parallelChannelId)
        {
            await tradingViewGraphClientService.DeleteParallelChannel(int.Parse(parallelChannelId), _cancellationTokenSource.Token);
        }
    }
}
