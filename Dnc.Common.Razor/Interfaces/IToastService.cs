using Dnc.Common.Razor.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dnc.Common.Razor.Interfaces
{
    public interface IToastService
    {
        Task ShowSuccessToast(string message, string header, TimeSpan? duration = null);
        Task ShowErrorToast(string message, string header, TimeSpan? duration = null);
        Task ShowWarningToast(string message, string header, TimeSpan? duration = null);
        Task ShowInfoToast(string message, string header, TimeSpan? duration = null);
        void Close();

    }
}
