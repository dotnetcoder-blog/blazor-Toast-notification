using Dnc.Common.Razor.Enums;
using Dnc.Common.Razor.Interfaces;
using Dnc.Common.Razor.Wrapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.Common.Razor.Toast
{
    public class DncToastComponent : ComponentBase, IToastService, IDisposable
    {
        [CascadingParameter]
        protected DncWrapperComponent DncWrapper { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        protected bool Show { get; set; }
        private string currentLocation = string.Empty;

        protected string Header { get; set; }
        protected MarkupString Message { get; set; }
        protected string ToastBackgroundColor { get; set; }
        protected string ToastIconCss { get; set; }

        protected override void OnInitialized()
        {
            DncWrapper.SetToastService(this);
        }
        public void Close()
        {
            Show = false;
        }
        public void Dispose()
        {
            NavigationManager.LocationChanged -= NavigationHandler;
        }
        public async Task ShowSuccessToast(string message, string header, TimeSpan? duration = null)
        {
            await ShowToast(message, header, ToastType.Success, duration);
        }
        public async Task ShowErrorToast(string message, string header, TimeSpan? duration = null)
        {
            await ShowToast(message, header, ToastType.Error, duration);
        }
        public async Task ShowWarningToast(string message, string header, TimeSpan? duration = null)
        {
            await ShowToast(message, header, ToastType.Warning, duration);
        }
        public async Task ShowInfoToast(string message, string header, TimeSpan? duration = null)
        {
            await ShowToast(message, header, ToastType.Info, duration);
        }
        private async Task ShowToast(string message, string header, ToastType toastType, TimeSpan? duration = null)
        {
            Show = true;
            Message = (MarkupString)message;

            switch (toastType)
            {
                case ToastType.Success:
                    ToastBackgroundColor = "dnc-toast-success";
                    ToastIconCss = "check";
                    Header = string.IsNullOrEmpty(header) ? "Success" : header;
                    break;

                case ToastType.Error:
                    ToastBackgroundColor = "dnc-toast-error";
                    ToastIconCss = "times";
                    Header = string.IsNullOrEmpty(header) ? "Error" : header;
                    break;

                case ToastType.Warning:
                    ToastBackgroundColor = "dnc-toast-warning";
                    ToastIconCss = "exclamation";
                    Header = string.IsNullOrEmpty(header) ? "Warning" : header;
                    break;

                case ToastType.Info:
                    ToastBackgroundColor = "dnc-toast-info";
                    ToastIconCss = "info";
                    Header = string.IsNullOrEmpty(header) ? "Info" : header;
                    break;
            }

            currentLocation = NavigationManager.Uri;
            NavigationManager.LocationChanged -= NavigationHandler;
            NavigationManager.LocationChanged += NavigationHandler;
            StateHasChanged();

            if (duration != null)
            {
                await Task.Delay((TimeSpan)duration);
                Clear();
            }
        }
        private void NavigationHandler(object sender, LocationChangedEventArgs args)
        {
            if (!string.Equals(args.Location, currentLocation, StringComparison.OrdinalIgnoreCase))
            {
                Clear();
                NavigationManager.LocationChanged -= NavigationHandler;
                StateHasChanged();
            }
        }
        public void Clear()
        {
            Show = false;
            StateHasChanged();
        }
    }
}
