using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Models;
using XamarinFormsApp.Services;
using XamarinFormsApp.Views;
using System.Linq;

namespace XamarinFormsApp.ViewModels
{
    public class MasterViewModel : BaseViewModel
    {
        Settings settings;
        readonly AzureService azureService = new AzureService();
        public string BackgroundColor { get; set; }

        private string _coverImage;
        public string CoverImage
        {
            get { return _coverImage; }
            set { SetProperty(ref _coverImage, value); }
        }

        private string _profileImage;
        public string ProfileImage
        {
            get { return _profileImage; }
            set { SetProperty(ref _profileImage, value); }
        }

        private string _profileName;
        public string ProfileName
        {
            get { return _profileName; }
            set { SetProperty(ref _profileName, value); }
        }

        private string _toggleSocialButton;
        public string ToggleSocialButton
        {
            get { return _toggleSocialButton; }
            set { SetProperty(ref _toggleSocialButton, value); }
        }

        private ObservableCollection<MenuMasterModel> _menuMasterDataSource;
        public ObservableCollection<MenuMasterModel> MenuMasterDataSource
        {
            get { return _menuMasterDataSource; }
            set { SetProperty(ref _menuMasterDataSource, value); }
        }

        public Command<MenuMasterModel> MenuSelectedItemCommand { get; }

        public MasterViewModel()
        {
            settings = new Settings();
            Title = "Master";
            BackgroundColor = "White";
            CoverImage = string.IsNullOrEmpty(settings.FacebookImageCover) ? "CoverImageDefault" : settings.FacebookImageCover;
            ProfileImage = string.IsNullOrEmpty(settings.FacebookImageProfile) ? "ProfileImageDefault" : settings.FacebookImageProfile;
            ProfileName = string.IsNullOrEmpty(settings.FacebookName) ? "Perfil" : settings.FacebookName;
            ToggleSocialButton = string.IsNullOrEmpty(settings.ToggleSocialButton) ? settings.ToggleSocialButton = "Logar com Facebook" : settings.ToggleSocialButton;
            MenuSelectedItemCommand = new Command<MenuMasterModel>(ExecuteMenuSelectedItemCommandAsync);
            MenuMasterDataSource = LoadMenuMaster(new ObservableCollection<MenuMasterModel>());
            SettupMasterProfile();
        }

        private ObservableCollection<MenuMasterModel> LoadMenuMaster(ObservableCollection<MenuMasterModel> menuMasterDataSource)
        {
            menuMasterDataSource
                .Add(new MenuMasterModel()
                {
                    Icon = "Home",
                    Title = "Tela Principal",
                    TargetType = typeof(HomePage)
                });
            menuMasterDataSource
                .Add(new MenuMasterModel()
                {
                    Icon = string.IsNullOrEmpty(settings.FacebookId) ? "FacebookLogin" : "FacebookLogout",
                    Title = string.IsNullOrEmpty(settings.FacebookId) ? "Logar com Facebook" : "Logout",
                    TargetType = null
                });


            return menuMasterDataSource;
        }

        private async void ExecuteMenuSelectedItemCommandAsync(MenuMasterModel menu)
        {
            if (menu.TargetType == null)
            {
                if (string.IsNullOrEmpty(settings.FacebookId))
                {
                    await LoginAsync();
                }
                else
                {
                    await azureService.LogoutAsync();
                }

                SettupMasterProfile();
                return;
            }
                

            var page = (Page)Activator.CreateInstance(menu.TargetType);

            await App.PushAsync(page);
        }


        public Task<bool> LoginAsync()
        {
            if (!string.IsNullOrWhiteSpace(settings.UserId))
                return Task.FromResult(true);

            return azureService.LoginAsync();
        }

        private void SettupMasterProfile()
        {
            if (settings == null)
                settings = new Settings();

            CoverImage = string.IsNullOrEmpty(settings.FacebookImageCover) ? "CoverImageDefault" : settings.FacebookImageCover;
            ProfileImage = string.IsNullOrEmpty(settings.FacebookImageProfile) ? "ProfileImageDefault" : settings.FacebookImageProfile;
            ProfileName = string.IsNullOrEmpty(settings.FacebookName) ? "Perfil" : settings.FacebookName;

            var itemList = MenuMasterDataSource.Where(w => new string[] { "Logar com Facebook", "Logout" }.Contains(w.Title)).SingleOrDefault();
            itemList.Title = string.IsNullOrEmpty(settings.FacebookId) ? "Logar com Facebook": "Logout";
            itemList.Icon = string.IsNullOrEmpty(settings.FacebookId) ? "FacebookLogin" : "FacebookLogout";

        }

    }
}
