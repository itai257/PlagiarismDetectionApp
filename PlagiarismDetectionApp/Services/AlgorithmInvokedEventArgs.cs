using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlagiarismDetectionApp.Services
{
    class AlgorithmInvokedEventArgs
    {
        public List<string> TextFiles { get; set; }

        public int NGramSize { get; set; }

        public int SegmentChunkSize { get; set; }
    }
}
