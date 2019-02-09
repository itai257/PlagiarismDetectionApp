using System;
using System.Linq;
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
        private string _activeView = "";

        public string Text { set; get; } = "textesgsjnhgsd";
        public DelegateCommand ShowAboutViewCommand { get; set; }
        public DelegateCommand GoBackCommand { get; set; }

        public bool ShowAboutView
        {
            get => _showAbout;
            set => SetProperty(ref _showAbout, value);
        }

        public ShellViewModel(MainWindowView mainWindowView, IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            regionManager.RegisterViewWithRegion("MainRegion", typeof(MainWindowView));
            ShowAboutViewCommand = new DelegateCommand(OnShowAbout);
            GoBackCommand = new DelegateCommand(OnGoBack);
            ShowAboutView = false;

        }

        private void OnShowAbout()
        {
            var v = regionManager.Regions["MainRegion"].ActiveViews.FirstOrDefault();
            _activeView = v?.ToString();
            ShowAboutView = true;
            regionManager.RequestNavigate("MainRegion", "AboutView");
        }

        private void OnGoBack()
        {
            ShowAboutView = false;
            regionManager.RequestNavigate("MainRegion", string.IsNullOrEmpty(_activeView) ? "MainWindowView" : _activeView);
        }
    }
}
