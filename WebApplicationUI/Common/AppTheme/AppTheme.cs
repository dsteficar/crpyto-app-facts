using Microsoft.JSInterop;

namespace WebApplicationUI.Common.AppTheme
{
    public class AppTheme
    {
        private readonly IJSRuntime js;

        public AppTheme(IJSRuntime js)
        {
            this.js = js;
        }

        private bool isDarkMode = false;

        public bool IsDarkMode
        {
            get => isDarkMode;
            set
            {
                isDarkMode = value;
                js.InvokeVoidAsync("setDarkMode", value);
                OnChange?.Invoke();
            }
        }

        public async Task<bool> IsBrowserDarkMode() => await js.InvokeAsync<bool>("isBrowserDarkMode");

        public event Action OnChange;

        public async Task ListenForThemeChanges()
        {
            var dotnetHelper = DotNetObjectReference.Create(this);
            await js.InvokeVoidAsync("addThemeEventListener", dotnetHelper);
        }

        [JSInvokable]
        public async Task SetDarkMode(bool isDarkMode) => IsDarkMode = isDarkMode;
    }
}
