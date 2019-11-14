using Firmware.IBL;
using System;
using System.Collections.Generic;
using Firmware.DAL.DataOperations;

namespace Firmware.BL
{
    public class FirmwareRepository : IFirmwareRepository
    {
        //private FirmwarePOCEntities firmwarePOCEntities;

        //public FirmwareRepository()
        //{
        //    firmwarePOCEntities = new FirmwarePOCEntities();
        //}

        public Guid UploadFirmware(byte[] firmwareSwPackg, string firmwareFilename, byte[] helpDoc, string helpDocFileName, string key)
        {
            PackageFile packageFile = new PackageFile();

            packageFile.HelpDocument = helpDoc;
            packageFile.HelpDocumentFileName = helpDocFileName;
            packageFile.SoftwarePakage = firmwareSwPackg;
            packageFile.SoftwarePackageFileName = firmwareFilename;

            if (Guid.Empty != new Guid(key))
            {
                FirmwareCache.DeleteFromMemoryCache(key);
            }

            var tempKey = Guid.NewGuid();
            FirmwareCache.AddOrGetFirmware(tempKey.ToString(), packageFile);
            return tempKey;
        }
        public bool AddFirmware(string key)
        {
            
        }

        public IEnumerable<Model.Models.Firmware> GetAllFirmware()
        {
            throw new NotImplementedException();
        }
    }
}
