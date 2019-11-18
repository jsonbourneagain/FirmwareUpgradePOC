using Firmware.DAL.Models;
using System.Collections.Generic;

namespace Firmware.IBL
{
    public interface IFirmwareRepository
    {
        IEnumerable<SoftwarePackage> GetAllSoftwarePackage();
        System.Guid UploadFirmware(byte[] firmwareSwPackg, string firmwareFilename, byte[] helpDoc, string helpDocFileName, string key);
        bool AddFirmware(string key, string SwPkgVersion, string SwPkgDescription, int SwColorStandardID, int SwVersion, string SwFileChecksum, string SwFileChecksumType, string SwCreatedBy, string BlobDescription);
        bool DeleteSwPackageFromMemory(string key);
    }
}
