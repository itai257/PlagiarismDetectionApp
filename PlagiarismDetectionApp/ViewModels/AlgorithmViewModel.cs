using PlagiarismDetectionApp.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace PlagiarismDetectionApp.ViewModels
{
    class AlgorithmViewModel: BindableBase, INavigationAware
    {
        private IEventAggregator eventAggregator;
        private IRegionManager regionManager;
        private NavigationParameters Results;
        private SharedConfigurations sharedConfigurations;

        public AlgorithmViewModel(IRegionManager regionManager, SharedConfigurations sharedConfigurations)
        {
            this.regionManager = regionManager;
            this.sharedConfigurations = sharedConfigurations;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var path = sharedConfigurations.PathToProjectMain; // main script path
            var parameters = "";
            var textList = navigationContext.Parameters["TextFiles"] as List<string>;
            parameters += (navigationContext.Parameters["NGramSize"] + " ");
            parameters += (navigationContext.Parameters["ChunkSize"] + " ");
            textList.ForEach(t => parameters += ("\"" + t + "\" "));

            Task.Run(() => run_cmd(path, parameters)).ContinueWith(
                (o) => Application.Current.Dispatcher.Invoke(
                    () => regionManager.RequestNavigate("MainRegion", "ResultsWindowView",Results)
                    ));
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private void run_cmd(string cmd, string args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = sharedConfigurations.PathToPythonExe; // python.exe path
            start.Arguments = string.Format("{0} {1}", cmd, args);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.CreateNoWindow = true;

            using (Process process = Process.Start(start))
            {
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.EnableRaisingEvents = true;

                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                    Results = new NavigationParameters();
                    Results.Add("Results", result);
                }
            }

        }
    }
}
