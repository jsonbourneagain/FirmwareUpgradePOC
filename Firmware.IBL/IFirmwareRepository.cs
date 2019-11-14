using System.Collections.Generic;

namespace Firmware.IBL
{
    public interface IFirmwareRepository
    {
        IEnumerable<Model.Models.Firmware> GetAllFirmware();
        System.Guid UploadFirmware(byte[] firmwareSwPackg, string firmwareFilename, byte[] helpDoc, string helpDocFileName, string key);
        bool AddFirmware(string key, string SwPkgVersion, string SwPkgDescription, int SwColorStandardID, int SwVersion,  string SwFileFormat, string SwFileURL, string SwFileChecksum, string SwFileChecksumType, string SwCreatedBy, string BlobDescription);
    }
}
