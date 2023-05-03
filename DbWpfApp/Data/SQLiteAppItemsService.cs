using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWpfApp.Data
{
    internal class AppItemsService : IAppItemsRepository
    {
        public void DeleteApp(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<App> GetApps()
        {
            throw new NotImplementedException();
        }

        public void SaveApp(App appItem)
        {
            throw new NotImplementedException();
        }

        public void UpdateApp(int id)
        {
            throw new NotImplementedException();
        }
    }
}
