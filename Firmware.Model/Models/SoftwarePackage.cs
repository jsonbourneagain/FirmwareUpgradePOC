using System;

namespace Firmware.DAL.Models
{
    public class SoftwarePackage
    {
        public Guid SwPkgUID { get; set; }
        public string SwPkgVersion { get; set; }
        public int SwColorStandardID { get; set; }
        public DateTime SwAddedDate { get; set; }
        public string SwFileName { get; set; }
        public float SwFileSize { get; set; }
        public string HelpDocFileName { get; set }
    }
}
