using DbWpfApp.Data;
using DbWpfApp.Infrastructure.Commands;
using DbWpfApp.Models;
using DbWpfApp.Services;
using DbWpfApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DbWpfApp.ViewModels
{
    internal class MainWindowVM : ViewModel
    {

        #region Services
        private readonly DataManager _dataManager;
        private readonly ToastService _toastService; 
        #endregion

        #region Properties
        #region Id
        private int _id;

        public int Id
        {
            get => _id;
            set => Set(ref _id, value);
        }
        #endregion

        #region AppName
        private string _appName;

        public string AppName
        {
            get => _appName;
            set => Set(ref _appName, value);
        }
        #endregion

        #region UserName
        private string _userName;

        public string UserName
        {
            get => _userName;
            set => Set(ref _userName, value);
        }
        #endregion

        #region Comment
        private string _comment;

        public string Comment
        {
            get => _comment;
            set => Set(ref _comment, value);
        }
        #endregion 

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


        #region ToastProp
        private Toast _ToastProp = new Toast();
        public Toast ToastProp
        {
            get => _ToastProp;
            set => Set(ref _ToastProp, value);
        }
        #endregion

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
            if (!string.IsNullOrWhiteSpace(AppName) && !string.IsNullOrWhiteSpace(UserName))
                {
                    try
                    {
                        _dataManager.AppItems.SaveApp(new AppItem
                        {
                            AppName = AppName,
                            UserName = UserName,
                            Comment = Comment
                        });
                        _toastService.ShowToast($"Запис додано!", Toast.ToastIconType.Success);
                        AppList = _dataManager.AppItems.GetApps();
                    }
                    catch
                    {
                        _toastService.ShowToast("Під час додавання виникла помилка!", Toast.ToastIconType.Warning);
                    }
                }
                else
                    _toastService.ShowToast("Заповніть поля назви застосунку та імені!", Toast.ToastIconType.Warning);
        }
        #endregion

        #region DeleteFromDatabaseCommand
        public ICommand DeleteFromDatabaseCommand { get; }
        private bool CanDeleteFromDatabaseCommandExecute(object p) => true;
        private void OnDeleteFromDatabaseCommandExecute(object p)
        {
            try
            {
                if (_dataManager.AppItems.GetAppItem(Id) != null)
                {
                    _dataManager.AppItems.DeleteApp(Id);
                    _toastService.ShowToast($"Запис {Id} видалений!", Toast.ToastIconType.Success);
                    AppList = _dataManager.AppItems.GetApps();
                }
                else
                    _toastService.ShowToast($"Запису {Id} не існує!", Toast.ToastIconType.Warning);
            }
            catch
            {
                _toastService.ShowToast("Під час видалення виникла помилка!", Toast.ToastIconType.Warning);
            }
        }
        #endregion

        #region UpdateToDatabaseCommand
        public ICommand UpdateToDatabaseCommand { get; }
        private bool CanUpdateToDatabaseCommandExecute(object p) => true;
        private void OnUpdateToDatabaseCommandExecute(object p)
        {
            if (!string.IsNullOrWhiteSpace(AppName) && !string.IsNullOrWhiteSpace(UserName))
            {
                try
                {
                    if (_dataManager.AppItems.GetAppItem(Id) != null)
                    {
                        _dataManager.AppItems.UpdateApp(Id, new AppItem
                        {
                            AppName = AppName,
                            UserName = UserName,
                            Comment = Comment
                        });
                        _toastService.ShowToast($"Запис {Id} оновлено!", Toast.ToastIconType.Success);
                        AppList = _dataManager.AppItems.GetApps();
                    }
                    else
                        _toastService.ShowToast($"Запису {Id} не існує!", Toast.ToastIconType.Warning);
                }
                catch
                {
                    _toastService.ShowToast("Сталася помилка під час оновлення!", Toast.ToastIconType.Warning);
                }
            }
            else
                _toastService.ShowToast("Заповніть поля назви застосунку та імені!", Toast.ToastIconType.Warning);
        }

        #endregion

        public ICommand ShowInfoCommand { get; }
        private bool CanShowInfoCommandExecute(object p) => true;
        private void OnShowInfoCommandExecute(object p)
        {
            _toastService.ShowToast("Вітаю у застосунку для роботи з базою даних!", Toast.ToastIconType.None);
        }

        #endregion

        public MainWindowVM(DataManager dataManager, ToastService toastService)
        {
            // Database
            _dataManager = dataManager;
            AppList = _dataManager.AppItems.GetApps();

            // Commands
            AddToDatabaseCommand = new LambdaCommand(OnAddToDatabaseCommandExecute, CanAddToDatabaseCommandExecute);
            DeleteFromDatabaseCommand = new LambdaCommand(OnDeleteFromDatabaseCommandExecute, CanDeleteFromDatabaseCommandExecute);
            UpdateToDatabaseCommand = new LambdaCommand(OnUpdateToDatabaseCommandExecute, CanUpdateToDatabaseCommandExecute);
            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecute, CanCloseApplicationCommandExecute);
            ShowInfoCommand = new LambdaCommand(OnShowInfoCommandExecute, CanShowInfoCommandExecute);

            // Toasts
            _toastService = toastService;
            ToastProp.Opacity = 0;
            _toastService.ToastMessageRecieved += (message, icon) =>
            {
                ToastProp.Text = message;
                ToastProp.ToastIcon = icon;
                ToastProp.IsToastVisible = true;
            };
        }
    }
}
