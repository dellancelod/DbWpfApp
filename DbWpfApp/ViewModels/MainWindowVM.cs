using DbWpfApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWpfApp.ViewModels
{
    internal class MainWindowVM : ViewModel
    {
        #region Title
        private string _Title = "Робота з Базою Даних";

        /// <summary>
        /// Заголовок вікна
        /// </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        } 
        #endregion


    }
}
