using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeartWatchface.Services.Database.User
{
    public interface IUserService
    {
        long getBornDate();
        void saveBornDate(long bornDate);
        void deleteAllUser();

    }
}
