﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWpfApp.Data
{
    internal class SQLiteDataAccess
    {
        private static string connectionId = "Default";
        public static string LoadConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[connectionId].ConnectionString;
        }
    }
}