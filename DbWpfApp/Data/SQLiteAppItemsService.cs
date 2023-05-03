using DbWpfApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbWpfApp.Data
{
    internal class SQLiteAppItemsService : IAppItemsRepository
    {
        public void DeleteApp(int id)
        {
            using (SQLiteConnection connection =
                 new SQLiteConnection(SQLiteDataAccess.LoadConnectionString()))
            {
                connection.Open();
                string deleteApp = $"delete from Apps where id = {id}";
                SQLiteCommand command = new SQLiteCommand(deleteApp, connection);
                command.ExecuteNonQuery();
            }
        }

        public List<AppItem> GetApps()
        {
            using (SQLiteConnection connection =
                new SQLiteConnection(SQLiteDataAccess.LoadConnectionString()))
            {
                List<AppItem> returnApps = new List<AppItem>();
                connection.Open();
                string getApps = "select * from apps;";
                SQLiteCommand command = new SQLiteCommand(getApps, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AppItem app = new AppItem
                    {
                        Id = reader.GetInt32(0),
                        AppName = (string)reader["app_name"],
                        UserName = (string)reader["user_name"],
                        Comment = (string)reader["comment"]
                    };
                        
                    returnApps.Add(app);
                }
                return returnApps;
            }
        }

        public void SaveApp(AppItem appItem)
        {
            using (SQLiteConnection connection =
                new SQLiteConnection(SQLiteDataAccess.LoadConnectionString()))
            {
                connection.Open();
                string addApp = $"insert into Apps(app_name, user_name, comment) " +
                    $"values ('{appItem.AppName}', '{appItem.UserName}', '{appItem.Comment}')";
                SQLiteCommand command = new SQLiteCommand(addApp, connection);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateApp(int id, AppItem appItem)
        {
            using (SQLiteConnection connection =
                new SQLiteConnection(SQLiteDataAccess.LoadConnectionString()))
            {
                connection.Open();
                string updateApps = $"update Apps set app_name = '{appItem.AppName}', " +
                    $"user_name = '{appItem.UserName}', comment = '{appItem.Comment}' " +
                    $"where id = {id}";
                SQLiteCommand command = new SQLiteCommand(updateApps, connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
