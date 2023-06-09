﻿using DbWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWpfApp.Data
{
    internal interface IAppItemsRepository
    {
        List<AppItem> GetApps();
        void SaveApp(AppItem appItem);
        void DeleteApp(int id);
        void UpdateApp(int id, AppItem appItem);
        AppItem GetAppItem(int id);
    }
}
