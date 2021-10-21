using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace IBTExamUbroAPI.Models
{
    public class VideoFile
    {
        [Key]
        public Int32 Id { get; set; }

        [Display(Name = "Name")]
        public String Name { get; set; }

        ////[DatalistColumn]
        //[Display(Name = "Select")]
        //public String SelectFile { get; set; }

        public Boolean? IsWorking { get; set; }

        public List<VideoFile> allfiles { get; private set; }

        public void GetAllFiles()
        {
            allfiles = new List<VideoFile>();
            string path = HttpContext.Current.Server.MapPath("~/Media/");
            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                foreach (FileInfo fi in di.GetFiles())
                {
                    VideoFile dthis = new VideoFile();
                    dthis.Name = fi.Name; ;
                    // dthis.SelectFile = HttpContext.Current.Server.MapPath("~/Media/") + fi.Name;
                    dthis.IsWorking = false;
                    allfiles.Add(dthis);
                }

            }
            //  return allfiles;
        }
        //public void CombineVideos()
        // {
        //     string firstVideoFilePath = HttpContext.Current.Server.MapPath("~/Media/1.mp4"); 
        //     string secondVideoFilePath = HttpContext.Current.Server.MapPath("~/Media/2.mp4");
        //     string outputVideoPath = HttpContext.Current.Server.MapPath("~/Media/Earth.mp4");

        //     using (ITimeline timeline = new DefaultTimeline())
        //     {
        //         IGroup group = timeline.AddVideoGroup(32, 720, 576);

        //         var firstVideoClip = group.AddTrack().AddVideo(firstVideoFilePath);
        //         var secondVideoClip = group.AddTrack().AddVideo(secondVideoFilePath, firstVideoClip.Duration);

        //         using (AviFileRenderer renderer = new AviFileRenderer(timeline, outputVideoPath))
        //         {
        //             renderer.Render();
        //         }
        //     }
        // }

        //        public void MergeFiles(string strFile)
        //        {
        //            string strParam;
        //            string Path_FFMPEG = HttpContext.Current.Server.MapPath("~/ffmpeg/bin/ffmpeg.exe");

        //            //Converting a video into mp4 format
        //            string strOrginal = Server.MapPath("~/Videos/");
        //            strOrginal = strOrginal + strFile;
        //            string strConvert = Server.MapPath("~/Videos/ConvertedFiles/");
        //            string strExtn = Path.GetExtension(strOrginal);
        //            if (strExtn != ".mp4")
        //            {
        //                strConvert = strConvert + strFile.Replace(strExtn, ".mp4");
        //                strParam = "-i " + strOrginal + " " + strConvert;
        //                //strParam = "-i " + strOrginal + " -same_quant " + strConvert;
        //                process(Path_FFMPEG, strParam);
        //            }

        //            //Merging two videos               
        //            String video1 = Server.MapPath("~/Videos/Cars1.mp4");
        //            String video2 = Server.MapPath("~/Videos/ConvertedFiles/Cars2.mp4");
        //            String strResult = Server.MapPath("~/Videos/ConvertedFiles/Merge.mp4");
        //            //strParam = "-loop_input -shortest -y -i " + video1 + " -i " + video2 + " -acodec copy -vcodec mjpeg " + strResult;

        //            strParam = " -i " + video1 + " -i " + video2 + " -acodec copy -vcodec mjpeg " + strResult;

        //            process(Path_FFMPEG, strParam);
        //        }

        //        public void process(string Path_FFMPEG, string strParam)
        //        {
        //            try
        //            {
        //                Process ffmpeg = new Process();
        //                ProcessStartInfo ffmpeg_StartInfo = new ProcessStartInfo(Path_FFMPEG, strParam);
        //                ffmpeg_StartInfo.UseShellExecute = false;
        //                ffmpeg_StartInfo.RedirectStandardError = true;
        //                ffmpeg_StartInfo.RedirectStandardOutput = true;
        //                ffmpeg.StartInfo = ffmpeg_StartInfo;
        //                ffmpeg_StartInfo.CreateNoWindow = true;
        //                ffmpeg.EnableRaisingEvents = true;
        //                ffmpeg.Start();
        //                ffmpeg.WaitForExit();
        //                ffmpeg.Close();
        //                ffmpeg.Dispose();
        //                ffmpeg = null;
        //            }
        //            catch (Exception ex)
        //            {

        //            }
        //        }

        //    }
        //}
    }
}