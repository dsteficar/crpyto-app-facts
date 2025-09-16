
window.setDarkMode = (isDarkMode) => {
    if (isDarkMode) {
        document.body.setAttribute("data-theme", "dark");
    }
    else
    {
        document.body.setAttribute("data-theme", "light");
    }
}

const prefersDarkQuery = "(prefers-color-scheme: dark)"
window.isBrowserDarkMode = () => window.matchMedia(prefersDarkQuery).matches

window.addThemeEventListener = (dotnet) => {
    window.matchMedia(prefersDarkQuery).addEventListener("change", event => {
        dotnet.invokeMethodAsync("SetDarkMode", event.matches);
    });
};
