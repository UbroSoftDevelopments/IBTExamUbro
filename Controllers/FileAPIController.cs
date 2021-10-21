using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IBTExamUbroAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FileAPIController : ApiController
    {
        [Route("api/FileAPI/UploadFiles")]
        [HttpPost]

        public async Task<bool> Upload()
        {
            try
            {
                var fileuploadPath = HttpContext.Current.Server.MapPath("~/Media/");
                var provider = new MultipartFormDataStreamProvider(fileuploadPath);
                var content = new StreamContent(HttpContext.Current.Request.GetBufferlessInputStream(true));
                foreach (var header in Request.Content.Headers)
                {
                    content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
                await content.ReadAsMultipartAsync(provider);
                string uploadingFileName = provider.FileData.Select(x => x.LocalFileName).FirstOrDefault();
                string originalFileName = String.Concat(fileuploadPath, "\\" + (provider.Contents[1].Headers.ContentDisposition.FileName).Trim(new Char[] { '"' }));
                if (File.Exists(originalFileName))
                {
                    File.Delete(originalFileName);
                }

                File.Move(uploadingFileName, originalFileName);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

       // [Route("api/FileAPI/FileInfo")]
       // [HttpGet]
       //public string infomedia(string inputPath)
       // {
           
       // }

    }
}
