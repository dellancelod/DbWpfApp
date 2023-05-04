﻿using DbWpfApp.Data;
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
        private readonly DataManager _dataManager;
        private readonly ToastService _toastService;

        #region ToastProp
        private Toast _ToastProp = new Toast();
        public Toast ToastProp
        {
            get => _ToastProp;
            set => Set(ref _ToastProp, value);
        } 
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
            _dataManager.AppItems.SaveApp(new AppItem
            {
                AppName = AppName,
                UserName = UserName,
                Comment = Comment
            });

            AppList = _dataManager.AppItems.GetApps();

        }
        #endregion

        #region DeleteFromDatabaseCommand
        public ICommand DeleteFromDatabaseCommand { get; }
        private bool CanDeleteFromDatabaseCommandExecute(object p) => true;
        private void OnDeleteFromDatabaseCommandExecute(object p)
        {
            _dataManager.AppItems.DeleteApp(Id);

            AppList = _dataManager.AppItems.GetApps();

            _toastService.ShowToast("Hi!", Toast.ToastIconType.Success);

        }
        #endregion

        #region UpdateToDatabaseCommand
        public ICommand UpdateToDatabaseCommand { get; }
        private bool CanUpdateToDatabaseCommandExecute(object p) => true;
        private void OnUpdateToDatabaseCommandExecute(object p)
        {
            _dataManager.AppItems.UpdateApp(Id, new AppItem
            {
                AppName = AppName,
                UserName = UserName,
                Comment = Comment
            });

            AppList = _dataManager.AppItems.GetApps();
        }

        #endregion

        #endregion

        public MainWindowVM(DataManager dataManager, ToastService toastService)
        {
            _dataManager = dataManager;
            AppList = _dataManager.AppItems.GetApps();
            AddToDatabaseCommand = new LambdaCommand(OnAddToDatabaseCommandExecute, CanAddToDatabaseCommandExecute);
            DeleteFromDatabaseCommand = new LambdaCommand(OnDeleteFromDatabaseCommandExecute, CanDeleteFromDatabaseCommandExecute);
            UpdateToDatabaseCommand = new LambdaCommand(OnUpdateToDatabaseCommandExecute, CanUpdateToDatabaseCommandExecute);

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecute, CanCloseApplicationCommandExecute);

            _toastService = toastService;

            _toastService.ToastMessageRecieved += (message, icon) =>
            {
                ToastProp.Text = message;
                ToastProp.ToastIcon = icon;
                ToastProp.IsToastVisible = true;
            };
        }
    }
}
