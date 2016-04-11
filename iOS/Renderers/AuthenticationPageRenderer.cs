﻿using System;
using System.Linq;
using FreshMvvm;
using Newtonsoft.Json;
using ReactiveBrite.iOS.Renderers;
using ReactiveBrite.Models;
using ReactiveBrite.PageModels;
using ReactiveBrite.Pages;
using ReactiveBrite.Services;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AuthenticationPage), typeof(AuthenticationPageRenderer))]

namespace ReactiveBrite.iOS.Renderers
{
    public class AuthenticationPageRenderer : PageRenderer
    {
        private bool isShown;

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            // Retrieve any stored account information
            var accounts = AccountStore.Create().FindAccountsForService(App.AppName);
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
                    PresentViewController(auth.GetUI(), true, null);
                }
            }
            else {
                if (!isShown)
                {

                    //MessagingCenter.Send(this, "UserAuthenticated", account.Username);
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
                    AccountStore.Create().Save(e.Account, App.AppName);
                }
            }
            // If the user is logged in navigate to the TodoList page.
            // Otherwise allow another login attempt.

            //App.SuccessfulLoginAction.Invoke();
            //MessagingCenter.Send((AuthenticationPage)Element, "UserAuthenticated", string.Empty);
            ((AuthenticationPageModel)((AuthenticationPage)Element).GetModel()).LoggedCommand.Execute(string.Empty);
        }
    }
}
