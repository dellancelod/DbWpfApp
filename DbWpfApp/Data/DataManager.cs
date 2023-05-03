using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWpfApp.Data
{
    internal class DataManager
    {
        public IAppItemsRepository AppItems { get; set; }
        public DataManager(IAppItemsRepository appItemsRepository)
        {
            AppItems = appItemsRepository;
        }
    }
}
