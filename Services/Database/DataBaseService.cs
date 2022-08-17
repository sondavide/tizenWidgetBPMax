using HeartWatchface.Services.Database;
using SQLite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HeartWatchface.Services
{
    public class DataBaseService : IDataService
    {
        const string databaseFileName = "Spinning.db3";
        private static SQLiteConnection dbConnection;
        static string databasePath;

        public DataBaseService()
        {
            Tizen.Log.Info("AAAAA","inizializzando il database...");
            // Need to initialize SQLite
            raw.SetProvider(new SQLite3Provider_sqlite3());
            raw.FreezeProvider(true);
            string dataPath = global::Tizen.Applications.Application.Current.DirectoryInfo.Data;
            databasePath = Path.Combine(dataPath, databaseFileName);
            dbConnection = new SQLiteConnection(databasePath);
        }

        public SQLiteConnection getDataBaseConnection()
        {
            return dbConnection;
        }
    }
}
