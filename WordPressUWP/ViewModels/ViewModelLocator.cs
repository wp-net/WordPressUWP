using System;

using GalaSoft.MvvmLight.Ioc;

using Microsoft.Practices.ServiceLocation;

using WordPressUWP.Services;
using WordPressUWP.Views;

namespace WordPressUWP.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register(() => new NavigationServiceEx());
            SimpleIoc.Default.Register<ShellViewModel>();
            Register<NewsViewModel, NewsPage>();
            Register<NewsDetailViewModel, NewsDetailPage>();
            Register<SettingsViewModel, SettingsPage>();
        }

        public SettingsViewModel SettingsViewModel => ServiceLocator.Current.GetInstance<SettingsViewModel>();

        public NewsDetailViewModel NewsDetailViewModel => ServiceLocator.Current.GetInstance<NewsDetailViewModel>();

        public NewsViewModel NewsViewModel => ServiceLocator.Current.GetInstance<NewsViewModel>();

        public ShellViewModel ShellViewModel => ServiceLocator.Current.GetInstance<ShellViewModel>();

        public NavigationServiceEx NavigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();

        public void Register<VM, V>()
            where VM : class
        {
            SimpleIoc.Default.Register<VM>();

            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
