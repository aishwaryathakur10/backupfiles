using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Routing;
using WebApi.Models;



namespace WebApi.Controllers
{
    public class StudentController : ApiController
    {


        private StudentDBEntities db = new StudentDBEntities();
       // private DetailClass St1 = new DetailClass();

        int pageSize = 3;
        //  ApiRecordEntities entity = new ApiRecordEntities();
        DetailClass St1 = new DetailClass();

        // GET api/Student
        public IHttpActionResult Get(int pageNumber = 0)
        {


            try
            {
                if (pageNumber < 0)
                {
                    return BadRequest();
                }
                var rec = (from s in db.StudentTables orderby s.id ascending select s).Skip(3 * pageNumber).Take(pageSize).ToList();
                //  var current_url = Request.RequestUri.AbsoluteUri;
                var totalCount = db.StudentTables.ToList().Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                if (rec == null)
                    return NotFound();
                var urlHelper = new UrlHelper(Request);

                St1.studentList = rec;




                if (Request.RequestUri.AbsoluteUri.ToString().IndexOf("pageNumber") > 0)
                {
                    St1.next_url = pageNumber < totalPages + 1 ? Request.RequestUri.AbsoluteUri.ToString().Replace("pageNumber=" + pageNumber, "pageNumber=" + (pageNumber + 1)) : "";
                    St1.prev_url = pageNumber > 0 ? Request.RequestUri.AbsoluteUri.ToString().Replace("pageNumber=" + pageNumber, "pageNumber=" + (pageNumber - 1)) : "";
                }
                else
                {
                    St1.next_url = pageNumber < totalPages + 1 ? Request.RequestUri.AbsoluteUri.ToString() + "?pageNumber=" + (pageNumber + 1) : "";
                    St1.prev_url = pageNumber > 0 ? Request.RequestUri.AbsoluteUri.ToString() + "?pageNumber=" + (pageNumber - 1) : "";
                }

                return Ok(St1);

            }

            catch (Exception ex)
            {
                return InternalServerError();
            }




        // GET api/Student
        //int pageno = 0;

        //public IHttpActionResult Getstudent(int pageno = 0, int pagesize = 4)
        //{
        //    var prev_url = "";
        //    var next_url = "";

        //    // Get total number of records
        //    int total = db.StudentTables.Count();
        //    var totalPages = (int)Math.Ceiling((double)total / pagesize);




        //    var d = db.StudentTables
        //          .OrderBy(c => c.id)
        //          .Skip(pageno * pagesize)
        //          .Take(pagesize)
        //          .ToList();


        //    St1.studentList = d;
        //    var urlHelper = new UrlHelper(Request);


        //    if (Request.RequestUri.AbsoluteUri.ToString().IndexOf("pageno") > 0)
        //    {
              
        //        St1.next_url = pageno < totalPages + 1 ? Request.RequestUri.AbsoluteUri.ToString().Replace("pageno=" + pageno, "pageno=" + (pageno + 1)) : "";
        //        St1.prev_url = pageno > 0 ? Request.RequestUri.AbsoluteUri.ToString().Replace("pageno=" + pageno, "pageno=" + (pageno - 1)) : "";
        //    }
        //    else
        //    {

                
        //        St1.next_url = pageno < totalPages + 1 ? Request.RequestUri.AbsoluteUri.ToString() + "?pageno=" + (pageno + 1) : "";
        //        St1.prev_url = pageno > 0 ? Request.RequestUri.AbsoluteUri.ToString() + "?pageno=" + (pageno - 1) : "";
        //    }

        //    //var  prevLink = pageno > 0 ? urlHelper.Link("DefaultApi", new { pageno = pageno - 1 }) : "";
        //    // var nextLink = pageno <totalPages-1 ? urlHelper.Link("DefaultApi", new { pageno = pageno + 1 }) : "";


        //    /* return Ok(new
        //      {
        //         // record = d,
        //          //St1.Student =d,
        //          NextLink = nextLink,
        //          PrevLink = prevLink
        //      });*/
        //    return Ok(St1);



        //    /* {
                
        //       // prev_url=prevLink,
        //       // next_url=nextLink,
    
        //        /* Paging = new
        //         {
        //             Total = total,
        //             Limit = pagesize,
        //             Offset = pageno,
                   
        //             Returned = d.Count

        //         }
        //     });*/


        }

        // GET api/Student/5
        [ResponseType(typeof(StudentTable))]
        public IHttpActionResult Getstudent(int id)
        {
            StudentTable student = db.StudentTables.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT api/Student/5
        public IHttpActionResult Putstudent(int id, StudentTable student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.id)
            {
                return BadRequest();
            }


            db.Entry(student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!studentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Student
        [ResponseType(typeof(StudentTable))]
        public IHttpActionResult Poststudent(StudentTable student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentTables.Add(student);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = student.id }, student);
        }

        // DELETE api/Student/5
        [ResponseType(typeof(StudentTable))]
        public IHttpActionResult Deletestudent(int id)
        {
            StudentTable student = db.StudentTables.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            db.StudentTables.Remove(student);
            db.SaveChanges();

            return Ok(student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool studentExists(int id)
        {
            return db.StudentTables.Count(e => e.id == id) > 0;
        }
    }
}
