using DbWpfApp.Data;
using DbWpfApp.Infrastructure.Commands;
using DbWpfApp.Models;
using DbWpfApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbWpfApp.ViewModels
{
    internal class AppItemVM : ViewModel
    {
        private readonly DataManager _dataManager;

        #region Commands
        #region AddToDatabaseCommand
        public ICommand AddToDatabaseCommand { get; }
        private bool CanAddToDatabaseCommandExecute(object p) => true;
        private void OnAddToDatabaseCommandExecute(object p)
        {
            _dataManager.AppItems.SaveApp(new AppItem
            {

            });
        }
        #endregion
        #endregion

        public AppItemVM(DataManager dataManager)
        {
            AddToDatabaseCommand = new LambdaCommand(OnAddToDatabaseCommandExecute, CanAddToDatabaseCommandExecute);
            _dataManager = dataManager; 
        }
    }
}
