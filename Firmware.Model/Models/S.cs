using System;

namespace Firmware.Model.Models
{
    public class Firmware
    {
        public string FirmwareUID { get; set; }
        public string FirmwareDescription { get; set; }
        public string FirmwareVersion { get; set; }
        public int DeviceType { get; set; }
        public DateTime FWBuildDate { get; set; }
        public string HelpDocumentName { get; set; }
        public Byte[] HelpDocument { get; set; }
        public bool IsObsolete { get; set; }
        public bool IsDeleted { get; set; }
        public string Version { get; set; }
    }
}
