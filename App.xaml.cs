using HeartWatchface.Mvvm;
using HeartWatchface.Services;
using HeartWatchface.Services.Database;
using HeartWatchface.Services.Database.User;
using HeartWatchface.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tizen;
using Tizen.Security;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace HeartWatchface
{
    public partial class App : Application
    {

        protected static ServiceProvider ServiceProvider { get; set; }

        public App()
        {
            SetupServices();
            InitializeComponent();
        }


        void SetupServices()
        {
            var services = new ServiceCollection();
            Tizen.Log.Info("AAAAA", "sono qui");

            // TODO: Add user of services here
            services.AddTransient<MainViewModel>();
            services.AddTransient<DateTimeViewModel>();
            services.AddSingleton<IUserService, UserService>();

            // TODO: Add core services here
            services.AddSingleton<IDataService, DataBaseService>();

 

            ServiceProvider = services.BuildServiceProvider();

        }


        public static BaseViewModel GetViewModel<TViewModel>() where TViewModel : BaseViewModel
        {
            Tizen.Log.Info("AAAAA","richiesto servizio: " + typeof(TViewModel).FullName);            
            return Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetService<TViewModel>(ServiceProvider);
        }

        async void RequestPrivileges()
        {
            Tizen.Log.Info("A4G@Splash", "I'm here");

            string[] privileges = new[] {
                "http://tizen.org/privilege/power",
                "http://tizen.org/privilege/sensor",
                "http://tizen.org/privilege/mediastorage",
            "http://tizen.org/privilege/healthinfo"};

           


            //doesn't work. Will work in tizen 5.5
            CheckResult[] results = PrivacyPrivilegeManager.CheckPermissions(privileges).ToArray();

            List<string> privilegesWithAskStatus = new List<string>();
            try
            {
                for (int iterator = 0; iterator < results.Length; ++iterator)
                {
                    switch (results[iterator])
                    {
                        case CheckResult.Allow:
                            // Privilege can be used
                            break;
                        case CheckResult.Deny:
                            // Privilege can't be used
                            break;
                        case CheckResult.Ask:
                            // User permission request required
                            privilegesWithAskStatus.Add(privileges[iterator]);
                            break;
                    }
                }
                
                var request = await PrivacyPrivilegeManager.RequestPermissions(privilegesWithAskStatus);


                foreach (PermissionRequestResponse response in request.Responses)
                {
                    // PermissionRequestResponse contains Privilege name and RequestResult
                    switch (response.Result)
                    {
                        case RequestResult.AllowForever:
                            /// Update UI and start accessing protected functionality
                            break;
                        case RequestResult.DenyForever:
                            /// Show a message and terminate the application
                            break;
                        case RequestResult.DenyOnce:
                            /// Show a message with explanation
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Tizen.Log.Info("AAAAA", e.Message);
                // handle exceptions
            }


        }


        protected override void OnStart()
        {
            // Handle when your app starts
            RequestPrivileges();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
        }
    }
}
