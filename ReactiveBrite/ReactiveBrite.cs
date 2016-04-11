using System;
using System.Linq;
using FreshMvvm;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using ReactiveBrite.Models;
using ReactiveBrite.PageModels;
using ReactiveBrite.Pages;
using Xamarin.Forms;

namespace ReactiveBrite
{
	public class App : Application
	{
        public static string AppName => "ReactiveBrite";
	    public static User User { get; set; }
        public static bool IsLoggedIn => !string.IsNullOrWhiteSpace(User?.Email);
	    public static bool IsConnected { get; set; }

        public App ()
		{
            // The root page of your application
            User = new User();
            IsConnected = CrossConnectivity.Current.IsConnected;
            LoadBasicNav();
        }

        protected override void OnStart ()
		{
            // Handle when your app starts
            CrossConnectivity.Current.ConnectivityChanged += HandleConnectivityChanged;
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        private void LoadBasicNav()
        {
            var login = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
            var connected = FreshPageModelResolver.ResolvePageModel<MainPageModel>();
            var notConnected = FreshPageModelResolver.ResolvePageModel<NoNetworkPageModel>();
      
            if (IsConnected)
            {
                MainPage = IsLoggedIn ? new FreshNavigationContainer(connected) : new FreshNavigationContainer(login);
            }
            else
            {
                MainPage = new FreshNavigationContainer(notConnected);
            }    
        }

	    private void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            //var currentPage = this.MainPage.GetType();
            //if (e.IsConnected && currentPage != typeof(NetworkViewPage))
            //    MainPage = new NetworkViewPage();
            //else if (!e.IsConnected && currentPage != typeof(NoNetworkPage))
            //    MainPage = new NoNetworkPage();
            IsConnected = e.IsConnected;
            LoadBasicNav();
        }

        public Action SuccessfulLoginAction
        {
            get
            {
                return () => {
                    MainPage.Navigation.PopModalAsync();

                    if(IsLoggedIn)
                    {
                        MainPage.Navigation.InsertPageBefore(new MainPage(), MainPage.Navigation.NavigationStack.First());
                        MainPage.Navigation.PopToRootAsync();
                    }
                };
            }
        }
    }
}

