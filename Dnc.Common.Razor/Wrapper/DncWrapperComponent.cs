using Dnc.Common.Razor.Enums;
using Dnc.Common.Razor.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.Common.Razor.Wrapper
{
    public class DncWrapperComponent:ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        protected IToastService ToastService { get; set; }

        public void SetToastService(IToastService toastService)
        {
            ToastService = toastService;
        }

        public void ShowSuccessToast(string message, string header = null, TimeSpan? duration = null)
        {
            ToastService?.ShowSuccessToast(message, header, duration);
        }
        public void ShowErrorToast(string message, string header = null, TimeSpan? duration = null)
        {
            ToastService?.ShowErrorToast(message, header, duration);
        }
        public void ShowWarningToast(string message, string header = null, TimeSpan? duration = null)
        {
            ToastService?.ShowWarningToast(message, header, duration);
        }
        public void ShowInfoToast(string message, string header =null, TimeSpan? duration = null)
        {
            ToastService?.ShowInfoToast(message, header, duration);
        }
    }
}
