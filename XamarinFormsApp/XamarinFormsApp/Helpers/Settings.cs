using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using XamarinFormsApp.Models;
using XamarinFormsApp.ViewModels;

namespace XamarinFormsApp.Helpers
{  
  public class Settings
  {     
        private ISettings AppSettings => CrossSettings.Current;
        public event PropertyChangedEventHandler PropertyChanged;
                
        public string UserId
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        public string AuthToken
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        public string FacebookId
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        public string FacebookName
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        public string FacebookImageCover
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        public string FacebookImageProfile
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        public string ToggleSocialButton
        {
            get { return GetProperty<string>(); }
            set { SetProperty(value); }
        }

        public Settings()
        {            
        }

        public void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        bool SetProperty<T>(T value, T defaultValue = default(T), [CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return false;

            if (EqualityComparer<T>.Default.Equals(AppSettings.GetValueOrDefault<T>(propertyName, defaultValue), value))
                return false;

            AppSettings.AddOrUpdateValue(propertyName, value);
            RaisePropertyChanged(propertyName);

            return true;
        }

        T GetProperty<T>(T defaultValue = default(T), [CallerMemberName]string propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return defaultValue;

            return AppSettings.GetValueOrDefault(propertyName, defaultValue);
        }
    }
}