using Version.Plugin;

namespace XamarinFormsApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public string Versao => CrossVersion.Current.Version;

        public AboutViewModel()
        {
            Title = "Sobre";
        }
    }
}
