using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismDetectionApp.Models
{
    class SharedConfigurations
    {
        public string PathToProjectMain { get; set; } = "C:\\Users\\imalka\\Desktop\\PlagiCheck-Alg\\main.py";
        public string PathToPythonExe { get; set; } = "C:\\Users\\imalka\\Documents\\pycharm\\PlagiCheckFinal\\venv\\Scripts\\python.exe";

        public SharedConfigurations()
        {

        }
    }
}
