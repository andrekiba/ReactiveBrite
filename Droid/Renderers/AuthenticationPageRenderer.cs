using System;
using System.Linq;
using Android.App;
using FreshMvvm;
using Newtonsoft.Json;
using ReactiveBrite.Droid.Renderers;
using ReactiveBrite.Models;
using ReactiveBrite.PageModels;
using ReactiveBrite.Pages;
using ReactiveBrite.Services;
using Xamarin.Forms;
using Xamarin.Auth;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AuthenticationPage), typeof(AuthenticationPageRenderer))]

namespace ReactiveBrite.Droid.Renderers
{
    public class AuthenticationPageRenderer : PageRenderer
    {
        bool isShown;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            // Retrieve any stored account information
            var accounts = AccountStore.Create(Context).FindAccountsForService(App.AppName);
            var account = accounts.FirstOrDefault();

            if (account == null)
            {
                if (!isShown)
                {
                    isShown = true;

                    // Initialize the object that communicates with the OAuth service
                    var auth = new OAuth2Authenticator(
                                   ConnectionConstants.ClientId,
                                   ConnectionConstants.ClientSecret,
                                   ConnectionConstants.Scope,
                                   new Uri(ConnectionConstants.AuthorizeUrl),
                                   new Uri(ConnectionConstants.RedirectUrl),
                                   new Uri(ConnectionConstants.AccessTokenUrl));

                    // Register an event handler for when the authentication process completes
                    auth.Completed += OnAuthenticationCompleted;

                    // Display the UI
                    var activity = Context as Activity;
                    activity.StartActivity(auth.GetUI(activity));
                }
            }
            else {
                if (!isShown)
                {
                    ((AuthenticationPageModel)((AuthenticationPage)Element).GetModel()).LoggedCommand.Execute(account.Username);
                    //App.User.Email = account.Username;
                    //App.SuccessfulLoginAction.Invoke();
                }
            }
        }

        async void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                // If the user is authenticated, request their basic user data from Google
                // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                var request = new OAuth2Request("GET", new Uri(ConnectionConstants.UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    // Deserialize the data and store it in the account store
                    // The users email address will be used to identify data in SimpleDB
                    string userJson = response.GetResponseText();
                    App.User = JsonConvert.DeserializeObject<User>(userJson);
                    e.Account.Username = App.User.Email;
                    AccountStore.Create(Context).Save(e.Account, App.AppName);
                }
            }
            // If the user is logged in navigate to the TodoList page.
            // Otherwise allow another login attempt.
            ((AuthenticationPageModel)((AuthenticationPage)Element).GetModel()).LoggedCommand.Execute(string.Empty);
            //App.SuccessfulLoginAction.Invoke();
        }
    }
}