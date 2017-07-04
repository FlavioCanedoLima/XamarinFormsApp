using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.ViewModels;

namespace XamarinFormsApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        private MasterViewModel ViewModel => BindingContext as MasterViewModel;

        public MasterPage()
        {
            InitializeComponent();
            
            BindingContext = new MasterViewModel();

            menuList.ItemSelected += (sender, e) => 
            {                
                if (e?.SelectedItem == null)
                    return;

                ViewModel.MenuSelectedItemCommand.Execute(e.SelectedItem);

                menuList.SelectedItem = null;
            };
        }
    }
}