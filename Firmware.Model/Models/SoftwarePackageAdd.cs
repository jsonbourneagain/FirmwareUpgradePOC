using System;
using System.Collections.Generic;

namespace Firmware.Model.Models
{
    public class SoftwarePackageAdd
    {
        public string Key { get; set; }
        public Guid SwPkgUID { get; set; }

        public int SwColorStandardID { get; set; }

        public DateTime SwAddedDate { get; set; }

        public string SwFileName { get; set; }

        public float SwFileSize { get; set; }

        public string BlobDescription { get; set; }

        public string SwCreatedBy { get; set; }

        public string SwFileChecksum { get; set; }

        public string SwFileChecksumType { get; set; }

        public string SwPkgVersion { get; set; }
        public string SwPkgDescription { get; set; }
        public List<string> SupportedModels { get; set; }
        public string DeviceType { get; set; }
        public string Manufacturer { get; set; }
    }
    public class DeleteSwPackageModel
    {
        public List<Guid> PackageIds { get; set; }
        public bool DeleteAll { get; set; }
    }
}
