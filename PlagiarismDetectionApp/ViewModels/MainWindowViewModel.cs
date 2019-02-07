using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using PlagiarismDetectionApp.Models;
using Prism.Commands;
using System.Windows.Forms;
using Prism.Regions;
using PlagiarismDetectionApp.Views;

namespace PlagiarismDetectionApp.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
            private readonly MainWindowModel model;
        private readonly IRegionManager regionManager;
        private string corpusPath;

        public MainWindowViewModel(MainWindowModel model, IRegionManager regionManager)
        {
            this.model = model;
            this.regionManager = regionManager;
            TextFieldFocusedCommand = new DelegateCommand(OpenFileBrowser);
            ExecuteAlgorithm = new DelegateCommand(ExecuteAlgorithmHandler);
        }

        public string CorpusPath
        {
            get
            {
                return corpusPath;
            }
            set
            {
                corpusPath = value;
                RaisePropertyChanged(nameof(CorpusPath));
            }
        }

        public DelegateCommand TextFieldFocusedCommand { get; }

        public DelegateCommand ExecuteAlgorithm { get; }

        private void OpenFileBrowser()
        {
            OpenFileDialog();
        }

        private void ExecuteAlgorithmHandler()
        {
            regionManager.RequestNavigate("MainRegion", "ResultsWindowView");
        }

        private void OpenFileDialog()
        {
            var openFolderDialog = new FolderBrowserDialog();
            if (openFolderDialog.ShowDialog() == DialogResult.OK)
            {
                CorpusPath = openFolderDialog.SelectedPath;
            }
        }
    }
}
