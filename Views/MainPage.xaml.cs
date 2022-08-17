using HeartWatchface.ViewModels;
using System;

using Xamarin.Forms;

namespace HeartWatchface.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Tizen.Log.Info("AAAAA", "sono qui main 1");

            BindingContext = App.GetViewModel<MainViewModel>() as MainViewModel;
            Tizen.Log.Info("AAAAA", "sono qui main 2");

        }

    }
}
