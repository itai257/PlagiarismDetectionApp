using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismDetectionApp.ViewModels
{
    class ResultsWindowViewModel: BindableBase, INavigationAware
    {
        public ResultsWindowViewModel()
        {

        }

        public void GoBack()
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var navParameters = navigationContext.Parameters;
        }
    }
}
