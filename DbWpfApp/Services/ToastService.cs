using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWpfApp.Services
{
    internal class ToastService
    {
        public event Action<string, Toast.ToastIconType> ToastMessageRecieved;

        public void ShowToast(string message, Toast.ToastIconType icon)
        {
            ToastMessageRecieved(message, icon);
        }
    }
}
