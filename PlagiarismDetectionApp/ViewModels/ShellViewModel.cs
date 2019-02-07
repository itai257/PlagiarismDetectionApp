using Prism.Mvvm;
using PlagiarismDetectionApp.Views;
using Prism.Regions;

namespace PlagiarismDetectionApp.ViewModels
{
    class ShellViewModel : BindableBase
    {
        public string Text { set; get; } = "textesgsjnhgsd";
        private IRegionManager regionManager;

        public ShellViewModel(MainWindowView mainWindowView, IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            regionManager.RegisterViewWithRegion("MainRegion", typeof(MainWindowView));
        }
    }
}
