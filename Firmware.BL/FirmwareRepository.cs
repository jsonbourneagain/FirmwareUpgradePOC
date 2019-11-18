using Firmware.DAL.DataOperations;
using Firmware.IBL;
using System;
using System.Collections.Generic;

namespace Firmware.BL
{
    public class FirmwareRepository : IFirmwareRepository
    {
        private DataOperations _dataOperations;

        public FirmwareRepository()
        {
            _dataOperations = new DataOperations();
        }

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
        public bool AddFirmware(string key, string SwPkgVersion, string SwPkgDescription, int SwColorStandardID, int SwVersion, string SwFileChecksum, string SwFileChecksumType, string SwCreatedBy, string BlobDescription)
        {
            PackageFile package = FirmwareCache.AddOrGetFirmware(key, new PackageFile()) as PackageFile;

            return _dataOperations.AddSoftwarePackage(package.SoftwarePakage, package.HelpDocument, SwPkgVersion, SwPkgDescription, SwColorStandardID, SwVersion, package.SoftwarePackageFileName, "bin", package.SoftwarePakage.LongLength, null, SwFileChecksum, SwFileChecksumType, SwCreatedBy, BlobDescription);
        }

        public bool DeleteSwPackageFromMemory(string key)
        {
            try
            {
                FirmwareCache.DeleteFromMemoryCache(key);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public IEnumerable<Model.Models.Firmware> GetAllFirmware()
        {
            throw new NotImplementedException();
        }
    }
}
