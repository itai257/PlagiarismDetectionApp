using LiveCharts;
using LiveCharts.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PlagiarismDetectionApp.ViewModels
{
    class ResultsWindowViewModel: BindableBase, INavigationAware
    {
        private IRegionManager regionManager;

        public Dictionary<string, string> Results { get; set; }

        public SeriesCollection SeriesCollections { get; set; }

        public Func<double, string> YFormatter { get; set; }

        public DelegateCommand GoToMainCommand { get; set; }

        public string[] Labels { get; set; }

        public ResultsWindowViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            Results = new Dictionary<string, string>();
            SeriesCollections = new SeriesCollection();
            GoToMainCommand = new DelegateCommand(GotoMenu);
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
            ParseResults(navParameters);
            CreateSeriesCollectionForChart();
        }

        private void CreateSeriesCollectionForChart()
        {
            SeriesCollections.Clear();
            double threshold = 0.7;
            var Thresholdvalue = new ChartValues<double>();
            var values = new ChartValues<double>(Results.Values.Select(x => double.Parse(x)));
            SeriesCollections.Add(new LineSeries { Title = "Values", Values = values, PointForeground = Brushes.Blue });
            Results.Keys.ToList().ForEach(l => Thresholdvalue.Add(threshold));
            SeriesCollections.Add(new LineSeries { Title = "Threshold", Values = Thresholdvalue, PointForeground = Brushes.Red });
            Labels = Results.Keys.ToArray();
            YFormatter = value => value.ToString();
        }

        private void ParseResults(NavigationParameters navParameters)
        {
            if (navParameters == null || navParameters.ToArray().Length == 0)
            {
                GotoMenu();
            }

            var resultsList = ((string)navParameters["Results"]).Substring(5, navParameters["Results"].ToString().Length - 4 - 3).Split('[').ToList();
            var listWithoutPunc = resultsList.Select(s => s = s.Replace('\'', ' ').Replace(']', ' ')).ToList();
            var listOfListOfResults = listWithoutPunc.Select(s => s.Split(',')).ToList();
            Results.Clear();
            listOfListOfResults.ForEach(l => Results.Add(l[0], l[1]));
        }

        private void GotoMenu()
        {
            regionManager.RequestNavigate("MainRegion", "MainWindowView");
        }
    }
}
