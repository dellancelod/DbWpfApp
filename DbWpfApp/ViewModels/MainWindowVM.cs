using DbWpfApp.Infrastructure.Commands;
using DbWpfApp.Models;
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

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region AppList

        private List<AppItem> _AppList;

        public List<AppItem> AppList
        {
            get => _AppList;
            set => Set(ref _AppList, value);
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

        #region AddToDatabaseCommand
        public ICommand AddToDatabaseCommand { get; }
        private bool CanAddToDatabaseCommandExecute(object p) => true;
        private void OnAddToDatabaseCommandExecute(object p)
        {

        }
        #endregion

        #endregion

        public MainWindowVM()
        {
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecute, CanCloseApplicationCommandExecute);
            AddToDatabaseCommand = new LambdaCommand(OnAddToDatabaseCommandExecute, CanAddToDatabaseCommandExecute);
        }
    }
}
