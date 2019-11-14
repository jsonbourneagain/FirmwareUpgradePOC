using System;

namespace Firmware.DAL.Models
{
    public class SoftwarePackage
    {
        public string SwPkgUID { get; set; }
        public string SwPkgDescription { get; set; }
        public string SwPkgVersion { get; set; }
        public int SwColorStandardID { get; set; }
        public DateTime AddedDate { get; set; }
        public string SwVersion { get; set; }

    }
}
