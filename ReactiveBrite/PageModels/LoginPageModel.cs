using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using Xamarin.Forms;

namespace ReactiveBrite.PageModels
{
    public class LoginPageModel : FreshBasePageModel
    {
        public Command LoginCommand
        {
            get
            {
                return new Command(async () => {
                    await CoreMethods.PushPageModel<AuthenticationPageModel>();
                });
            }
        }
    }
}
