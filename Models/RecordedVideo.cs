using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBTExamUbroAPI.Models
{
    public class RecordedVideo
    {
        public int ID { get; set; }
        public string Enrollment { get; set; }
        public byte[] Videolog { get; set; }

        public RecordedVideo()
        {
            Videolog = new byte[20480000];
        }
    }
}