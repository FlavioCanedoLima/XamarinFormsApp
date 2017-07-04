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
    public partial class HomePage : ContentPage
    {
        private HomeViewModel ViewModel => BindingContext as HomeViewModel;

        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel();

            listViewHome.ItemSelected += (sender, e) =>
            {
                if (e?.SelectedItem == null)
                    return;
                
                ViewModel.ShowHomeItemDetail.Execute(e.SelectedItem);
                ((ListView)sender).SelectedItem = null;
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel == null)
                return;

            ViewModel.LoadHome();
        }
    }
}