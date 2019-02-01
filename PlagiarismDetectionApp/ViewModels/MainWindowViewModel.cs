using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using PlagiarismDetectionApp.Models;

namespace PlagiarismDetectionApp.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
            private readonly MainWindowModel model;

            public MainWindowViewModel(MainWindowModel model)
            {
                this.model = model;
            }
    }
}
