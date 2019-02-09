using PlagiarismDetectionApp.Models;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismDetectionApp.ViewModels
{
    class ConfigurationsWindowViewModel
    {
        private SharedConfigurations sharedConfigurations;
        private IRegionManager regionManager;

        public string PathToPythonExe { get; set; }

        public string PathToProjectMain { get; set; }

        public DelegateCommand SaveConfigurations { get; }

        public DelegateCommand GoToMenu { get; }

        public ConfigurationsWindowViewModel(SharedConfigurations sharedConfigurations, IRegionManager regionManager)
        {
            this.sharedConfigurations = sharedConfigurations;
            this.regionManager = regionManager;
            PathToPythonExe = sharedConfigurations.PathToPythonExe;
            PathToProjectMain = sharedConfigurations.PathToProjectMain;
            SaveConfigurations = new DelegateCommand(SaveConfigurationsHandler);
            GoToMenu = new DelegateCommand(GoToMenuHandler);
        }

        private void GoToMenuHandler()
        {
            regionManager.RequestNavigate("MainRegion", "MainWindowView");
        }

        private void SaveConfigurationsHandler()
        {
            if (!string.IsNullOrEmpty(PathToProjectMain) && !string.IsNullOrEmpty(PathToPythonExe))
            {
                sharedConfigurations.PathToProjectMain = PathToProjectMain;
                sharedConfigurations.PathToPythonExe = PathToPythonExe;
            }

            GoToMenuHandler();
        }
    }
}
