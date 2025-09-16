using Microsoft.JSInterop;

namespace WebAdminUI.Common.AppTheme
{
    public class AppThemeService
    {
        private readonly IJSRuntime _jsRuntime;
        private bool _isDarkMode;

        public AppThemeService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public bool IsDarkMode
        {
            get => _isDarkMode;
            set
            {
                _isDarkMode = value;
                _jsRuntime.InvokeVoidAsync("setDarkMode", value);
                OnThemeChanged?.Invoke();
            }
        }

        public event Action? OnThemeChanged;

        public async Task InitializeThemeAsync()
        {
            _isDarkMode = await _jsRuntime.InvokeAsync<bool>("isBrowserDarkMode");
            _jsRuntime.InvokeVoidAsync("setDarkMode", _isDarkMode);
        }

        public async Task ListenForThemeChangesAsync()
        {
            var dotNetRef = DotNetObjectReference.Create(this);
            await _jsRuntime.InvokeVoidAsync("addThemeEventListener", dotNetRef);
        }

        [JSInvokable]
        public void SetDarkMode(bool isDarkMode)
        {
            IsDarkMode = isDarkMode;
        }
    }
}