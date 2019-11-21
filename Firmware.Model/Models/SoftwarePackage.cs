using System;
using System.Collections.Generic;

namespace Firmware.DAL.Models
{
    public class SoftwarePackage
    {
        public Guid SwPkgUID { get; set; }
        public string SwPkgVersion { get; set; }
        public string SwColorStandardID { get; set; }
        public DateTime SwAddedDate { get; set; }
        public string SwFileName { get; set; }
        public float SwFileSize { get; set; }
        public string HelpDocFileName { get; set; }
        public List<string> CameraModels { get; set; } = new List<string>();
    }
}
