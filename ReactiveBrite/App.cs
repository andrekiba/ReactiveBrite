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
        public static string AppName => "Reactive Brite";
	    public static User User { get; set; }
        public static bool IsLoggedIn => !string.IsNullOrWhiteSpace(User?.Email);
	    public static bool IsConnected { get; set; }

	    private FreshNavigationContainer navContainer;

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
            navContainer = new FreshNavigationContainer(login);
            MainPage = navContainer;
        }

	    private async void HandleConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.IsConnected;

            var noNetwork = FreshPageModelResolver.ResolvePageModel<NoNetworkPageModel>();

            if (IsConnected)
            {
                if (navContainer.Navigation.ModalStack.LastOrDefault().GetType() == typeof (NoNetworkPage))
                    await navContainer.PopPage(true);
            }
	        else
            {
                if(navContainer.Navigation.ModalStack.LastOrDefault().GetType() != typeof(NoNetworkPage))
                    await navContainer.PushPage(noNetwork, noNetwork.GetModel(), true);
            }
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

