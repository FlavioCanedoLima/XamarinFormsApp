using Android.Webkit;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsApp.Droid.Services;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Services;

[assembly: Xamarin.Forms.Dependency(typeof(Authentication))]
namespace XamarinFormsApp.Droid.Services
{
    public class Authentication : IAuthentication
    {
        Settings settings;

        public Authentication()
        {
            settings = new Settings();
        }

        public async Task<MobileServiceUser> Authenticate(
            MobileServiceClient client,
            MobileServiceAuthenticationProvider provider,
            IDictionary<string, string> parameters = null)
        {
            try
            {
                var user = await client?.LoginAsync(Forms.Context, provider);

                settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                settings.UserId = user?.UserId;

                return user;
            }
            catch (Exception ex)
            {   
                return null;              
            }
            
        }

        

        public void Signout()
        {
            CookieManager.Instance.RemoveAllCookie();
        }
    }
}