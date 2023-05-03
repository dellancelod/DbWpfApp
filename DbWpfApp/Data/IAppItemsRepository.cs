using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWpfApp.Data
{
    internal interface IAppItemsRepository
    {
        IQueryable<App> GetApps();
        void SaveApp(App appItem);
        void DeleteApp(int id);
        void UpdateApp(int id);
    }
}
