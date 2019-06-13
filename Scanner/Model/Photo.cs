using Emgu.CV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanner.Model
{
    public class Photo
    {
        public Mat OriginalPhoto { get; set; }
        public Mat SharpenedPhoto { get; set; }
        public string RecognizedText { get; set; }
        
    }
}
