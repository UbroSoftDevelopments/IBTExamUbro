using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IBTExamUbroAPI.Models
{
    public class RecordedImages
    {
        public int ID { get; set; }
        public string EnrollmentNumber { get; set; }
        public byte[] RecordedImage { get; set; }

        public RecordedImages()
        {
            RecordedImage = new byte[500000];
        }
    }
}