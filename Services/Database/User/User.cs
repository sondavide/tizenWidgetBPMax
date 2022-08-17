using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeartWatchface.Services.Database.User
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public long bornDate { get; set; }
    }
}
