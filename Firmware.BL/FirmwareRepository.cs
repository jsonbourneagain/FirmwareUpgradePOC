using Firmware.DAL.DataOperations;
using Firmware.DAL.Models;
using Firmware.IBL;
using Firmware.Model.Models;
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
        public bool AddFirmware(string key, string SwPkgVersion, string SwPkgDescription, int SwColorStandardID, string SwFileChecksum, string SwFileChecksumType, string SwCreatedBy, string SwManufacturer, string SwDeviceType, List<string> SupportedModels, string BlobDescription)
        {
            PackageFile package = FirmwareCache.AddOrGetFirmware(key, new PackageFile()) as PackageFile;

            return _dataOperations.AddSoftwarePackage(package?.SoftwarePakage, package?.HelpDocument, SwPkgVersion, SwPkgDescription, SwColorStandardID, package?.SoftwarePackageFileName, "bin", package.SoftwarePakage.LongLength, null, SwFileChecksum, SwFileChecksumType, SwCreatedBy, "Honeywell", "Camera", SupportedModels, BlobDescription,
               package?.HelpDocumentFileName, "pdf", package?.HelpDocument?.Length);
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
        public IEnumerable<SoftwarePackage> GetAllSoftwarePackage(int pageNo, int pageSize, string searchText, string sortColumn, string sortDirection)
        {
            return _dataOperations.GetAllSoftwarePackage(pageNo, pageSize, searchText != null ? searchText : string.Empty, sortColumn != null ? sortColumn : "DEFAULT", sortDirection != null ? sortDirection : string.Empty);
        }

        public bool DeleteSoftwarePackage(List<Guid> packageIds, bool deleteAll)
        {
            bool result = _dataOperations.DeleteSoftwarePackage(packageIds, deleteAll);
            return result;
        }

        public byte[] GetHelpDoc(string key) => _dataOperations.GetHelpDoc(new Guid(key));

        public List<CameraMakeModel> GetCameraModels() => _dataOperations.GetCameraModels();
    }
}
