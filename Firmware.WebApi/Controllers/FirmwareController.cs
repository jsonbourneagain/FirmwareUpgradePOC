using Firmware.IBL;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Firmware.WebApi.Controllers
{

    public class FirmwareController : ApiController
    {
        private readonly IFirmwareRepository _repository;

        public FirmwareController(IFirmwareRepository repository)
        {
            this._repository = repository;
        }

        [HttpPost, Route("api/UploadSoftwarePackage")]
        public async Task<IHttpActionResult> UploadSoftwarePackage(string guid)
        {
            string key = guid.Trim('\"');
            var context = HttpContext.Current.Request.Files.Count;
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

            var helpDocFile = provider.Contents[1];
            helpDocFilename = helpDocFile.Headers.ContentDisposition.FileName.Trim('\"');
            helpDocBuffer = await helpDocFile.ReadAsByteArrayAsync();

            id = _repository.UploadFirmware(swPackgBuffer, swPackgFilename, helpDocBuffer, helpDocFilename, key).ToString();
            return base.Content(HttpStatusCode.OK, id, new JsonMediaTypeFormatter(), "text/plain"); ;
        }
        [HttpPost, Route("api/AddSoftwarePackage")]
        public async Task<IHttpActionResult> AddSoftwarePackage(string key, string SwPkgVersion, string SwPkgDescription, int SwColorStandardID, int SwVersion, string SwFileChecksum, string SwFileChecksumType, string SwCreatedBy, string BlobDescription)
        {
            key = key.Trim('\"');

            var result = _repository.AddFirmware(key, SwPkgVersion, SwPkgDescription, SwColorStandardID, SwVersion, SwFileChecksum, SwFileChecksumType, SwCreatedBy, BlobDescription);
            return base.Content(HttpStatusCode.OK, true, new JsonMediaTypeFormatter(), "text/plain"); ;
        }

        [HttpPost, Route("api/CancelUpload")]
        public async Task<IHttpActionResult> CancelUpload(string key)
        {
            key = key.Trim('\"');

            var result = _repository.DeleteSwPackageFromMemory(key);
            return base.Content(HttpStatusCode.OK, result, new JsonMediaTypeFormatter(), "text/plain"); ;
        }
        [HttpGet, Route("api/GetAllSoftwarePackage")]
        public async Task<IHttpActionResult> GetAllSoftwarePackage()
        {
            var result = _repository.GetAllSoftwarePackage();
            return base.Content(HttpStatusCode.OK, result, new JsonMediaTypeFormatter(), "text/plain"); ;
        }
    }
}
