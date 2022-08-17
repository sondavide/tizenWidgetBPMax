using System;
using System.Threading.Tasks;
using System.Windows.Input;
using HeartWatchface.Mvvm;
using HeartWatchface.Services.Database.User;
using HeartWatchface.Views;
using Tizen;
using Tizen.Security;
using Tizen.Sensor;
using Xamarin.Forms;
using HRM = Tizen.Sensor.HeartRateMonitor;


namespace HeartWatchface.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private const double initialSpeed = 0.5;
        private IUserService UserService;
        private String _text = Resources.AppResources.MainPageTitle;

        public static HRM heartRateMonitor;

        private int _years;

        public int Years
        {
            get => _years;
            set
            {
                if (_years == value) return;
                _years = value;
                OnPropertyChanged(nameof(Years));
                OnPropertyChanged(nameof(FCMax));
            }
        }

     
        private double _speed = initialSpeed;

        public double Speed
        {
            get => _speed;
            set
            {
                if (_speed == value) return;
                _speed = value;
                OnPropertyChanged(nameof(Speed));
            }
        }

        public double FCMax
        { //FC_max = 208 – (0,7 × età)

            get => 226 - _years;
        }

        private int _currentHb = 0;
        public int CurrentHeartBeat
        {
            get => _currentHb;
            set
            {
                if (value == _currentHb) return;
                _currentHb = value;
                OnPropertyChanged(nameof(CurrentHeartBeat));
            }

        }

        public String Text
        {
            get => _text;
            set
            {
                if (_text == value) return;
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        int GetYears(DateTime start, DateTime end)
        {
            return (end.Year - start.Year - 1) +
                (((end.Month > start.Month) ||
                ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
        }

        public void check() {
            long bd = UserService.getBornDate();
            //UserService.deleteAllUser();
            if (bd == 0)
            {
                Text = "starting...";


                Device.BeginInvokeOnMainThread(() =>
                {
                    App.Current.MainPage.Navigation.PushAsync(new DatePickerPage());

                });
            }
            else
            {
                DateTime now = DateTime.Now;
                DateTime bornDateTime = new DateTime(bd);
                Years = GetYears(bornDateTime, now);

                if (heartRateMonitor == null || !heartRateMonitor.IsSensing)
                {
                    heartRateMonitor = new HRM() { PausePolicy= SensorPausePolicy.None};

                    heartRateMonitor.DataUpdated += OnDataUpdated;
                }

                Text = "---";
            }
        }

        public MainViewModel(IUserService userService)
        {
           
            Tizen.Log.Info("AAAAA", "sono qui nel mainviemodel");

            Click = new Command(() =>
             {
                 if (heartRateMonitor.IsSensing)
                 {
                     heartRateMonitor.Stop();
                     ButtonText = "Start";
                     ButtonColor = "#85e00d";
                     Speed = initialSpeed;
                 }
                 else
                 {
                     heartRateMonitor.Start();
                     ButtonText = "Stop";
                     ButtonColor = "#d40d17";
                 }
             });

            this.UserService = userService;
            check();
            
        }

        private string _buttonText = "Start";
        private string _buttonColor = "#85e00d";

        public string ButtonColor
        {
            get => _buttonColor;
            set
            {
                if (_buttonColor == value) return;
                _buttonColor = value;
                OnPropertyChanged(nameof(ButtonColor));
            }
        }

        public string ButtonText
        {
            get => _buttonText;
            set
            {
                if (_buttonText == value) return;
                _buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }


        public ICommand Click { protected set; get; }

        private void OnDataUpdated(object sender, HeartRateMonitorDataUpdatedEventArgs e)
        {
            Log.Debug("AAAAA", $"Rate:{e.HeartRate}");
            if(e.HeartRate!=-10 && e.HeartRate!=0 && e.HeartRate != -3)
            {
                double percentage = e.HeartRate / FCMax;
                CurrentHeartBeat = e.HeartRate;
                Text = Math.Round(100 * percentage, 2).ToString();
                Speed = initialSpeed + percentage > 0 ? Math.Pow(3, percentage) : 0;
            }
            
        }
    }
}
