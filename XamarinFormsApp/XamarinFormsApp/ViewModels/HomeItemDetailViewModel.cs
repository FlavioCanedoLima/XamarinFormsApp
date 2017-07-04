using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinFormsApp.Models;

namespace XamarinFormsApp.ViewModels
{
    public class HomeItemDetailViewModel : BaseViewModel
    {
        private readonly HomeDetail _homeDetail;
        public ObservableCollection<HomeItemDetail> HomeItemDetails { get; }
        public string GridImageSource { get; }

        public HomeItemDetailViewModel(HomeDetail homeDetail = null)
        {
            Title = "Notícia";
            GridImageSource = "ColoredBackground";
            this._homeDetail = homeDetail;
            HomeItemDetails = new ObservableCollection<HomeItemDetail>();
        }

        public void LoadHomeItemDetail()
        {
            HomeItemDetails.Clear();
            HomeItemDetails
                .Add(
                new HomeItemDetail()
                {
                    Id = 1,
                    Title = "E3 2017: ASSISTA À CONFERÊNCIA DA SONY NO CINEMA COM O IGN BRASIL",
                    Description = "Em São Paulo, em 12 de junho",
                    Text = 
                    new StringBuilder()
                    .Append("Quem acompanha a indústria dos videogames sabe: poucos momentos são tão empolgantes quanto as conferências das grandes publishers durante a E3. E na medida em que os dias passam, nossa empolgação sobre o evento desse ano não para de crescer.")
                    .AppendLine()
                    .Append("Pensando nisso, aqui vai uma proposta especial: que tal assistir à coletiva de imprensa da Sony PlayStation durante a E3 2017 em uma sala de cinema, com transmissão ao vivo, ao lado de um grupo seleto de leitores do IGN Brasil? Então aqui está a sua chance: estamos distribuindo ingressos para esse evento exclusivo, que vai ocorrer na segunda - feira, 12 de junho, a partir das 20h45, em São Paulo.")
                    .ToString(),
                    ImageSource = "PlaystationE3"
                });
        }
    }
}
