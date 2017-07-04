using System.Collections.ObjectModel;
using Xamarin.Forms;
using XamarinFormsApp.Models;

namespace XamarinFormsApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public string GridImageSource { get; }
        public ObservableCollection<HomeDetail> HomeDetails { get; }
        public Command<HomeDetail> ShowHomeItemDetail { get; }

        public HomeViewModel()
        {            
            Title = "Tela Principal";
            GridImageSource = "ColoredBackground";
            HomeDetails = new ObservableCollection<HomeDetail>();
            ShowHomeItemDetail = new Command<HomeDetail>(ExecuteShowHomeItemDetail);
        }

        private async void ExecuteShowHomeItemDetail(HomeDetail homeDetail)
        {
            await PushAsync<HomeItemDetailViewModel>(homeDetail);
        }

        public void LoadHome()
        {            
            HomeDetails.Clear();
            HomeDetails
                .Add(
                new HomeDetail()
                {
                    Id = 1,
                    Title = "PlayStation E3 Experience no Brasil",
                    Description = "A PlayStation E3 Experience deste ano promete, leia para ver os detalhes!",
                    ImageSource = "PlaystationE3"

                });
            
        }
    }
}
