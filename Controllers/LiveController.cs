using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IBTExamUbroAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LiveController : ApiController
    {
        /// API Controller for streaming video files.

        private const string videoFilePath = "~/Media/";

        /// Gets the live video.
        [HttpGet]
        [Route("api/video/live")]
        public IHttpActionResult GetLiveVideo(string videoFileId, string fileName)
        {
            string filePath = Path.Combine(HttpContext.Current.Server.MapPath(videoFilePath), fileName);
            return new VideoFileActionResult(filePath);
        }

        /// Gets the live video using post.
        /// The request.
        [HttpPost]
        [Route("api/video/live")]
        public IHttpActionResult GetLiveVideoPost(VideoFileDownloadRequest request)
        {
            string filePath = Path.Combine(HttpContext.Current.Server.MapPath(videoFilePath), request.Filename);
            return new VideoFileActionResult(filePath);
        }


        /// Action Result for Returning Stream
        public class VideoFileActionResult : IHttpActionResult
        {
            private const long BufferLength = 65536;
            public VideoFileActionResult(string videoFilePath)
            {
                this.Filepath = videoFilePath;
            }

            public string Filepath { get; private set; }

            Task<HttpResponseMessage> IHttpActionResult.ExecuteAsync(CancellationToken cancellationToken)
            {
                HttpResponseMessage response = new HttpResponseMessage();
                FileInfo fileInfo = new FileInfo(this.Filepath);
                long totalLength = fileInfo.Length;
                response.Content = new PushStreamContent((outputStream, httpContent, transportContext) =>
                {
                    OnStreamConnected(outputStream, httpContent, transportContext);
                });

                response.Content.Headers.ContentLength = totalLength;
                return Task.FromResult(response);
            }

            private async void OnStreamConnected(Stream outputStream, HttpContent content, TransportContext context)
            {
                try
                {
                    var buffer = new byte[BufferLength];

                    using (var nypdVideo = File.Open(this.Filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        var videoLength = (int)nypdVideo.Length;
                        var videoBytesRead = 1;

                        while (videoLength > 0 && videoBytesRead > 0)
                        {
                            videoBytesRead = nypdVideo.Read(buffer, 0, Math.Min(videoLength, buffer.Length));
                            await outputStream.WriteAsync(buffer, 0, videoBytesRead);
                            videoLength -= videoBytesRead;
                        }
                    }
                }
                catch (HttpException ex)
                {
                    return;
                }
                finally
                {
                    // Close output stream as we are done
                    outputStream.Close();
                }
            }
        }
    }
    
}
