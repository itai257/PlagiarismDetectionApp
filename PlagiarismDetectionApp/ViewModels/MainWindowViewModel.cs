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
using MaterialDesignThemes.Wpf;
using System.IO;
using Prism.Events;

namespace PlagiarismDetectionApp.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;
        private readonly MainWindowModel model;
        private readonly IRegionManager regionManager;
        private string corpusPath;
        private string segmentChunkSize;
        private string nGramSize;
        private SnackbarMessageQueue _messageQueue;

        public MainWindowViewModel(MainWindowModel model, IRegionManager regionManager)
        {
            this.model = model;
            this.regionManager = regionManager;
            TextFieldFocusedCommand = new DelegateCommand(OpenFileBrowser);
            ExecuteAlgorithm = new DelegateCommand(ExecuteAlgorithmHandler);
            MessageQueue = new SnackbarMessageQueue();
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

        public string SegmentChunkSize
        {
            get
            {
                return segmentChunkSize;
            }
            set
            {
                segmentChunkSize = value;
                RaisePropertyChanged(nameof(segmentChunkSize));
            }
        }

        public string NGramSize
        {
            get
            {
                return nGramSize;
            }
            set
            {
                nGramSize = value;
                RaisePropertyChanged(nameof(nGramSize));
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
            var textFilesToDeliver = GetNonEmptyTxTFilesExistsInFolder();
            if (ValidateInputBoxes() && textFilesToDeliver.Any())
            {
                var ngramAsInt = int.Parse(NGramSize);
                var chunkSizeAsInt = int.Parse(SegmentChunkSize);

                var navParams = new NavigationParameters();
                navParams.Add("NGramSize", ngramAsInt);
                navParams.Add("ChunkSize", chunkSizeAsInt);
                navParams.Add("TextFiles", textFilesToDeliver);

                regionManager.RequestNavigate("MainRegion", "AlgorithmView", navParams);
            }
        }

        private List<string> GetNonEmptyTxTFilesExistsInFolder()
        {
            if (corpusPath == null)
            {
                MessageQueue.Enqueue("path cannot be empty!");
                return new List<string>();
            }
            var txtFilesPath = Directory.EnumerateFiles(CorpusPath, "*.txt").ToList();
            var txtFilesToDeliver = new List<string>();
            foreach (var filePath in txtFilesPath)
            {
                if((new FileInfo(filePath).Length) > 0)
                {
                    txtFilesToDeliver.Add(filePath);
                }
            }

            if (!txtFilesToDeliver.Any())
            {
                MessageQueue.Enqueue("At Least One File In The Folder Should Be Non Empty .txt File");
            }

            return txtFilesToDeliver;
        }

        public SnackbarMessageQueue MessageQueue
        {
            get => _messageQueue;
            set => SetProperty(ref _messageQueue, value);
        }

        private bool ValidateInputBoxes()
        {
            if (string.IsNullOrEmpty(NGramSize))
            {
                NGramSize = "4";
            }

            if (string.IsNullOrEmpty(SegmentChunkSize))
            {
                SegmentChunkSize = "4000";
            }

            if (!int.TryParse(NGramSize,out int k))
            {
                MessageQueue.Enqueue("N-Gram Size Should Be A Numbers");
                return false;
            }else if(!int.TryParse(SegmentChunkSize, out int q))
            {
                MessageQueue.Enqueue("Segment Chunk Size Should Be A Numbers");
                return false;
            }
                return true;
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
