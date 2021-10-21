using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IBTExamUbroAPI.Controllers
{
    public class ImageController : ApiController
    {
        // GET api/Image
        [HttpGet]
        public DataTable Get()
        {
            DataTable dt = new DataTable();
            DataAccessManager.UbroBaseDB db = new DataAccessManager.UbroBaseDB();
            dt = db.GetDataFromTable("recimg");
            return dt;
        }

        // GET api/Image/enrollment
        [HttpGet]
        public DataTable Get(string enrollment)
        {
            DataTable dt = new DataTable();
            DataAccessManager.UbroBaseDB db = new DataAccessManager.UbroBaseDB();
            dt = db.GetDataFromTableByEnrollment(enrollment);
            return dt;
        }

        // POST api/Image
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Models.RecordedImages value)
        {
            DataAccessManager.UbroBaseDB db = new DataAccessManager.UbroBaseDB();
            Models.RecordedImages img = new Models.RecordedImages()
            {
                EnrollmentNumber = value.EnrollmentNumber,
                RecordedImage = value.RecordedImage
                
            };
            db.InsertImageWithEnrollment(img);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
       
           
        // PUT api/Image/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Image/5
        public void Delete(int id)
        {

        }
    }
}
