using HeartWatchface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HeartWatchface.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatePickerPage : ContentPage
    {
        public DatePickerPage()
        {
            Tizen.Log.Info("AAAAA", "sono qui in DateTimePicker");
            InitializeComponent();
            BindingContext = App.GetViewModel<DateTimeViewModel>() as DateTimeViewModel;

        }
    }
}