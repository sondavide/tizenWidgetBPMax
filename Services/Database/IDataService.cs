using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeartWatchface.Services.Database
{
    public interface IDataService
    {
        SQLiteConnection getDataBaseConnection();
    }
}
