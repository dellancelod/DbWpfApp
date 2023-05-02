using DbWpfApp.Infrastructure.Commands;
using DbWpfApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

        #region Commands

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecute(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #endregion

        public MainWindowVM()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecute, CanCloseApplicationCommandExecute);
        }
    }
}
