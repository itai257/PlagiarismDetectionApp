using PlagiarismDetectionApp.Services;
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

namespace PlagiarismDetectionApp.ViewModels
{
    class AlgorithmViewModel: BindableBase, INavigationAware
    {
        private string argus;
        private IEventAggregator eventAggregator;

        public string Argus { get { return argus; } set { argus = value; RaisePropertyChanged(nameof(Argus)); } }


        public AlgorithmViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            
            eventAggregator.GetEvent<AlgorithmInvokedEvent>().Subscribe(obj => {
                Task.Run(() => InovkeAlgorithm(obj));
                });
        }

        private void InovkeAlgorithm(AlgorithmInvokedEventArgs obj)
        {
            var path = "C:\\Users\\imalka\\Desktop\\PlagiCheck-Alg\\main.py"; // neet to change to real script path
            var parameters = "";
            var textList = obj.TextFiles as List<string>;
            parameters += (obj.NGramSize + " ");
            parameters += (obj.SegmentChunkSize + " ");
            textList.ForEach(t => parameters += ("\"" + t + "\" "));
            run_cmd(path, parameters);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
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
            start.FileName = "C:\\Users\\imalka\\Documents\\pycharm\\PlagiCheckFinal\\venv\\Scripts\\python.exe"; // need to change to python.exe path
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
                    Argus += result;
                }
            }
        }
    }
}
