using System;
using Prism.Mvvm;
using PlagiarismDetectionApp.Views;
using Prism.Commands;
using Prism.Regions;

namespace PlagiarismDetectionApp.ViewModels
{
    class ShellViewModel : BindableBase
    {
        private IRegionManager regionManager;
        private bool _showAbout = false;
        private bool _showMainView;

        public string Text { set; get; } = "textesgsjnhgsd";
        public DelegateCommand ShowAboutViewCommand { get; set; }
        public DelegateCommand GoBackCommand { get; set; }

        public bool ShowAboutView
        {
            get => _showAbout;
            set => SetProperty(ref _showAbout, value);
        }

        public bool ShowMainView
        {
            get => _showMainView;
            set => SetProperty(ref _showMainView, value);
        }

        public ShellViewModel(MainWindowView mainWindowView, IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            regionManager.RegisterViewWithRegion("MainRegion", typeof(MainWindowView));
            ShowAboutViewCommand = new DelegateCommand(OnShowAbout);
            GoBackCommand = new DelegateCommand(OnGoBack);
            ShowMainView = true;
            ShowAboutView = false;

        }

        private void OnShowAbout()
        {
            ShowMainView = false;
            ShowAboutView = true;
            regionManager.RequestNavigate("MainRegion", "AboutView");
        }

        private void OnGoBack()
        {
            ShowAboutView = false;
            ShowMainView  = true;
            regionManager.RequestNavigate("MainRegion", "MainWindowView");
        }
    }
}
