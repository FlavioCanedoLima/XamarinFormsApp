using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Models;

namespace XamarinFormsApp.Services
{
    public class AzureService
    {
        readonly string AzureAppUrl = "{your azure connection}/.auth/login/facebook/callback";
        public const string AzureAppUrlLogout = "{you azure connection}/.auth/logout";
        readonly string FacebookGraphUrl = "https://graph.facebook.com/v2.9/me/";
        readonly string ParametersFacebookGraph = "?fields=id,name,cover,picture.height(700)&access_token=";


        public MobileServiceClient Client { get; set; } = null;
        public static bool UserAuth { get; set; } = false;
        
        Settings settings;

        public AzureService()
        {
            settings = new Settings();
        }

        public void Initialize()
        {
            Client = new MobileServiceClient(AzureAppUrl);

            if (!string.IsNullOrWhiteSpace(settings.AuthToken) && !string.IsNullOrWhiteSpace(settings.UserId))
            {
                Client.CurrentUser = new MobileServiceUser(settings.UserId)
                {
                    MobileServiceAuthenticationToken = settings.AuthToken
                };
            }
        }

        public async Task<bool> LoginAsync()
        {
            if (Client == null)
                Initialize();

            var auth = DependencyService.Get<IAuthentication>();
            var user = await auth.Authenticate(Client, MobileServiceAuthenticationProvider.Facebook);

            if (user == null)
            {
                ClearDataFromSettings(settings);

                return false;
            }
            else
            {
                await InfoUserAsync(Client);

                settings.AuthToken = user.MobileServiceAuthenticationToken;
                settings.UserId = user.UserId;

                return true;
            }
        }

        public async Task LogoutAsync()
        {
            if (Client == null)
                Initialize();

            if (Client.CurrentUser?.MobileServiceAuthenticationToken == null)
                return;
       
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-ZUMO-AUTH", Client.CurrentUser.MobileServiceAuthenticationToken);
                await httpClient.GetAsync(new Uri(AzureAppUrlLogout));
            }

            // Remove the token from the cache
            Client.CurrentUser = new MobileServiceUser(string.Empty)
            {
                MobileServiceAuthenticationToken = string.Empty
            };

            //Remove the token from the MobileServiceClient
            await Client.LogoutAsync();

            DependencyService.Get<IAuthentication>().Signout();

            ClearDataFromSettings(settings);
        }

        public async Task InfoUserAsync(MobileServiceClient client)
        {
            var userInfo = await client.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me", System.Net.Http.HttpMethod.Get, null);
            var userToken = userInfo[0].AcessToken;
            var requestUrl = FacebookGraphUrl + ParametersFacebookGraph + userToken;
            var userJson = await new HttpClient().GetStringAsync(requestUrl);
            var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

            settings.FacebookId = facebookProfile.Id;
            settings.FacebookName = facebookProfile.Name;
            settings.FacebookImageCover = facebookProfile.Cover.Source;
            settings.FacebookImageProfile = facebookProfile?.Picture?.Data?.Url;
        }

        public void ClearDataFromSettings(Settings settings)
        {
            if (settings == null)
                return;

            settings.AuthToken = string.Empty;
            settings.UserId = string.Empty;
            settings.FacebookId = string.Empty;
            settings.FacebookName = string.Empty;
            settings.FacebookImageCover = string.Empty;
            settings.FacebookImageProfile = string.Empty;
        }
    }
}
