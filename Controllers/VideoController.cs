using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IBTExamUbroAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VideoController : ApiController
    {
        string filenames;
        public async void WriteContentToStream(Stream outputStream, HttpContent content, TransportContext transportContext)
        {
            if (filenames != null)
            {
                try
                {
                    var filePath = HttpContext.Current.Server.MapPath("~/Media/" + filenames);
                    //here set the size of buffer, you can set any size  
                    int bufferSize = 1024;
                    byte[] buffer = new byte[bufferSize];
                    //here we re using FileStream to read file from server//  
                    using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        int totalSize = (int)fileStream.Length;
                        /*here we are saying read bytes from file as long as total size of file 

                        is greater then 0*/
                        while (totalSize > 0)
                        {
                            int count = totalSize > bufferSize ? bufferSize : totalSize;
                            //here we are reading the buffer from orginal file  
                            int sizeOfReadedBuffer = fileStream.Read(buffer, 0, count);
                            //here we are writing the readed buffer to output//  
                            await outputStream.WriteAsync(buffer, 0, sizeOfReadedBuffer);
                            //and finally after writing to output stream decrementing it to total size of file.  
                            totalSize -= sizeOfReadedBuffer;
                        }


                    }

                }
                catch
                {

                }
            }
            else
            {
                return;
            }
        }     
       
        [HttpGet]
        //GET api/video/name
        public HttpResponseMessage Get(string name)
        {
            filenames = name;
            var httpResponce = Request.CreateResponse();
            httpResponce.Content = new PushStreamContent((Action<Stream, HttpContent, TransportContext>)WriteContentToStream);
            return httpResponce;
        }
    }

}

