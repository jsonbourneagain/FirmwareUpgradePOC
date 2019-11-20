using Firmware.IBL;
using Firmware.Model.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Firmware.WebApi.Controllers
{

    public class FirmwareController : ApiController
    {
        private readonly IFirmwareRepository _repository;

        public FirmwareController(IFirmwareRepository repository)
        {
            this._repository = repository;
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost, Route("api/UploadSoftwarePackage")]
        public async Task<IHttpActionResult> UploadSoftwarePackage(string guid)
        {
            string key = guid.Trim('\"');
            var count = HttpContext.Current.Request.Files.Count;
            var beforeUpload = GC.GetTotalMemory(false);
            string id = string.Empty;
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            byte[] swPackgBuffer = null;
            byte[] helpDocBuffer = null;

            string swPackgFilename = string.Empty;
            string helpDocFilename = string.Empty;

            var swPackgfile = provider.Contents[0];
            swPackgFilename = swPackgfile.Headers.ContentDisposition.FileName.Trim('\"');
            swPackgBuffer = await swPackgfile.ReadAsByteArrayAsync();

            if (count > 1)
            {
                var helpDocFile = provider.Contents[1];
                helpDocFilename = helpDocFile.Headers.ContentDisposition.FileName.Trim('\"');
                helpDocBuffer = await helpDocFile.ReadAsByteArrayAsync();
            }

            id = _repository.UploadFirmware(swPackgBuffer, swPackgFilename, helpDocBuffer, helpDocFilename, key).ToString();
            return base.Content(HttpStatusCode.OK, id, new JsonMediaTypeFormatter(), "text/plain"); ;
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost, Route("api/AddSoftwarePackage")]
        public async Task<IHttpActionResult> AddSoftwarePackage(SoftwarePackageAdd softwarePackage)
        {
            var key = softwarePackage.Key.Trim('\"');

            var result = _repository.AddFirmware(key, softwarePackage.SwPkgVersion, softwarePackage.SwPkgDescription, softwarePackage.SwColorStandardID, softwarePackage.SwFileChecksum, softwarePackage.SwFileChecksumType, softwarePackage.SwCreatedBy, softwarePackage.BlobDescription);
            return base.Content(HttpStatusCode.OK, true, new JsonMediaTypeFormatter(), "text/plain"); ;
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost, Route("api/CancelUpload")]
        public async Task<IHttpActionResult> CancelUpload(string key)
        {
            key = key.Trim('\"');

            var result = _repository.DeleteSwPackageFromMemory(key);
            return base.Content(HttpStatusCode.OK, result, new JsonMediaTypeFormatter(), "text/plain"); ;
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet, Route("api/GetAllSoftwarePackage")]
        public async Task<IHttpActionResult> GetAllSoftwarePackage(int pageNo, int pageSize)
        {
            var result = _repository.GetAllSoftwarePackage(pageNo, pageSize);
            return base.Content(HttpStatusCode.OK, result, new JsonMediaTypeFormatter(), "text/plain"); ;
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpDelete, Route("api/DeleteSoftwarePackage")]
        public async Task<IHttpActionResult> DeleteSoftwarePackage(List<Guid> packageIds)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(packageIds.ToString()))
            {
                result = await Task.Run(() => _repository.DeleteSoftwarePackage(packageIds[0]));
                if (result)
                {
                    return base.Content(HttpStatusCode.OK, result, new JsonMediaTypeFormatter(), "text/plain");
                }
                else
                {
                    return base.Content(HttpStatusCode.InternalServerError, result, new JsonMediaTypeFormatter(), "text/plain");
                }
            }
            else
            {
                return base.Content(HttpStatusCode.BadRequest, result, new JsonMediaTypeFormatter(), "text/plain");
            }
        }
    }
}
