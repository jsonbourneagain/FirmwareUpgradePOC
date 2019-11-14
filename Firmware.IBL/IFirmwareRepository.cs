using System.Collections.Generic;

namespace Firmware.IBL
{
    public interface IFirmwareRepository
    {
        IEnumerable<Model.Models.Firmware> GetAllFirmware();
        System.Guid UploadFirmware(byte[] firmwareSwPackg, string firmwareFilename,  byte[] helpDoc, string helpDocFileName, string key);
        bool AddFirmware(string key);
    }
}
