using HeartWatchface.Mvvm;
using HeartWatchface.Services.Database.User;
using HeartWatchface.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HeartWatchface.ViewModels
{
    public class DateTimeViewModel : BaseViewModel
    {
        DateTime _dateTime = DateTime.Now;


        public DateTimeViewModel(IUserService userService)
        {
            Tizen.Log.Info("AAAAA", "sono qui in datetimeviewmodel");

            ButtonPressedExit = new Command(() =>{
                Tizen.Log.Info("AAAAAB", "dateTime era: " + _dateTime.ToString());

                //long unixTime =(long) (TimeZoneInfo.ConvertTimeToUtc(_dateTime) - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalMilliseconds;
                long unixTime = _dateTime.Ticks;
                Tizen.Log.Info("AAAAAB", "sto salvando: " + unixTime);
                userService.saveBornDate(unixTime);
                App.Current.MainPage.Navigation.PushAsync(new MainPage());
            });
        }

        public DateTime Datetime
        {
            get => _dateTime;
            set
            {
                if (_dateTime == value) return;
                _dateTime = value;
                OnPropertyChanged(nameof(Datetime));
            }
        }

        public ICommand ButtonPressedExit { protected set; get; }

    }
}
