using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeartWatchface.Services.Database.User
{
    class UserService : IUserService
    {
        IDataService DataService;
        public UserService(IDataService dataService)
        {
            Tizen.Log.Info("AAAAA", "inizializzando il UserService...");

            DataService = dataService;
            DataService.getDataBaseConnection().CreateTable<User>();
        }

        public long getBornDate()
        {
            List<User> users = DataService.getDataBaseConnection().Table<User>().ToList();
            return users.Count == 0 ? 0 : users.First().bornDate;            
        }

        public void saveBornDate(long bornDate)
        {
            User user = new User();
            user.bornDate = bornDate;
            DataService.getDataBaseConnection().Insert(user);
        }

        public void deleteAllUser()
        {
            DataService.getDataBaseConnection().DeleteAll<User>();
        }
    }
}
