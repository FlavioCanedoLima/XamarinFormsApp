using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.ViewModels;

namespace XamarinFormsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeItemDetailPage : ContentPage
    {
        private HomeItemDetailViewModel ViewModel => BindingContext as HomeItemDetailViewModel;

        public HomeItemDetailPage()
        {
            InitializeComponent();

            listViewHomeItemDetail.ItemSelected += (sender, e) => 
            {
                ((ListView)sender).SelectedItem = null;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel == null)
                return;

            ViewModel.LoadHomeItemDetail();
        }
    }
}