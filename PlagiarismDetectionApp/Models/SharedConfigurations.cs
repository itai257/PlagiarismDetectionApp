using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismDetectionApp.Models
{
    class SharedConfigurations
    {
        public string PathToProjectMain { get; set; }
        public string PathToPythonExe { get; set; }

        public SharedConfigurations()
        {
            PathToProjectMain = Directory.GetCurrentDirectory()+ "\\pythonFiles\\main.py";
            PathToPythonExe = "C:\\python\\python.exe";
        }
    }
}
