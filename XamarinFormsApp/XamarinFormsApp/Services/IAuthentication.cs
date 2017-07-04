using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFormsApp.Services
{
    public interface IAuthentication
    {
        Task<MobileServiceUser> Authenticate(
            MobileServiceClient client,
            MobileServiceAuthenticationProvider provider,
            IDictionary<string, string> parameters = null);

        void Signout();
    }
}
