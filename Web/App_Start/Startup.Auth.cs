using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using ShortUrl.Models;
using System.Configuration;

namespace ShortUrl
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            SetAllConfiguredAuthProviders(app);
        }

        public void SetAllConfiguredAuthProviders(IAppBuilder app)
        {
            foreach (AuthProvider provider in (ConfigurationManager.GetSection("authenticationProviders") as AuthProviders).Providers)
            {
                switch (provider.ProviderName)
                {
                    case "Twitter":
                        app.UseTwitterAuthentication(consumerKey: provider.Key, consumerSecret: provider.Secret);
                        break;
                    case "Facebook":
                        app.UseFacebookAuthentication(appId: provider.Key, appSecret: provider.Secret);
                        break;
                    case "Microsoft":
                        app.UseMicrosoftAccountAuthentication(clientId: provider.Key, clientSecret: provider.Secret);
                        break;
                    case "Google":
                        app.UseGoogleAuthentication();
                        break;
                    default:
                        break;
                }
            }
        }

    }
}