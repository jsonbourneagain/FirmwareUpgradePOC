using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firmware.BL
{
    public class PackageFile
    {
        public string SoftwarePackageFileName { get; set; }
        public string HelpDocumentFileName{ get; set; }
        public byte[] SoftwarePakage { get; set; }
        public byte[] HelpDocument { get; set; }
    }
}
