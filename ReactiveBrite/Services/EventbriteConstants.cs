using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveBrite.Services
{
    public static class EventbriteConstants
    {
        //Eventbrite OAuth
        public static string ClientId = "4BA73NAT6EF4QSWLWY";
        public static string PersonalToken = "QYRL2UNIVBXT3Y5YFZDW";
        public static string AnonymousToken = "UEVFY4TFC62MNF4OQDQD";
        public static string ClientSecret = "O3BVBQAJDS57D7GGX66BJ6DLSXCRS3K3UYUDDKJKMHFJDPJZBB";

        // These values do not need changing
        public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
        public static string AuthorizeUrl = "https://www.eventbrite.com/oauth/authorize";
        public static string AccessTokenUrl = "https://www.eventbrite.com/oauth/token";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Set this property to the location the user will be redirected too after successfully authenticating
        public static string EventbriteRootUrl = "https://www.eventbrite.com/v3";
        public static string RedirectUrl = "http://blank.org";
    }
}
