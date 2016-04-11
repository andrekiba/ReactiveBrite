using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using ReactiveBrite.Pages;
using Xamarin.Forms;

namespace ReactiveBrite.PageModels
{
    public class AuthenticationPageModel : FreshBasePageModel
    {
        public AuthenticationPageModel()
        {
            //MessagingCenter.Subscribe<AuthenticationPage, string>(this, "UserAuthenticated", (sender, arg) => {
            //    LoggedCommand.Execute(arg);
            //});
        }

        public Command LoggedCommand
        {
            get
            {
                return new Command<string>(async (email) =>
                {
                    App.User.Email = email;
                    await CoreMethods.PopPageModel();

                    if (App.IsLoggedIn)
                    {
                        await CoreMethods.PushPageModel<MainPageModel>();
                        
                        //correggere con rootNavigation
                        CurrentPage.Navigation.InsertPageBefore(FreshPageModelResolver.ResolvePageModel<MainPageModel>(), Application.Current.MainPage.Navigation.NavigationStack.First());
                        await CoreMethods.PopToRoot(true);
                    }
                });
            }
        }
    }
}
