using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBTExamUbroAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ViewBag.Title = "Home Page";
            return View();
        }
        public ActionResult VideoView()
        {
            Models.VideoFile vfile = new Models.VideoFile();
            //List<Models.VideoFile> videolist = vfile.GetAllFiles();
            //int totalrecords = videolist.Count;
            vfile.GetAllFiles();
            ViewBag.Title = "Manish";
            
            return View(vfile.allfiles);
        }
        public ActionResult Screen()
        {
            return View();
        }

        //login 
        

    }
}
