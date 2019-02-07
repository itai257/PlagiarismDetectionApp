using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;
using System.Windows.Forms;
using Prism.Mvvm;

namespace PlagiarismDetectionApp.ViewModels
{
    class ShellViewModel : BindableBase
    {
        public string Text { set; get; } = "textesgsjnhgsd";

        private string corpusPath;

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

        public ShellViewModel()
        {
            TextFieldFocusedCommand = new DelegateCommand(OpenFileBrowser);
        }

        private void OpenFileBrowser()
        {
                OpenFileDialog();
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
