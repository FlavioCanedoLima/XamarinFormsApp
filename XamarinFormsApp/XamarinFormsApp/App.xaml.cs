using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsApp.Views;

namespace XamarinFormsApp
{
    public partial class App : Application
    {
        public static MasterDetailPage MasterDetailPage { get; set; }

        public App()
        {
            InitializeComponent();

            var navigationPage = new NavigationPage(new HomePage())
            {
                BarBackgroundColor = Color.Black,
                BarTextColor = Color.White
            };

            var mainPage = new MainPage()
            {
                Master = new MasterPage(),
                Detail = navigationPage
            };

            MainPage = mainPage;
            MasterDetailPage = mainPage;
        }

        public async static Task PushAsync(Page page)
        {
            MasterDetailPage.IsPresented = false;
            await MasterDetailPage.Detail.Navigation.PushAsync(page);
        }

        

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
