using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using WebApi.Models;
namespace WebAPiii.Controllers
{
    public class DetailController: Controller
    {
        //
        // GET: /Detail/
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult create()
        {
            return View();
        }



        [HttpPost]
        public ActionResult create(StudentTable stu)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49336/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<StudentTable>("Student", stu);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");

                }
            }

            // ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(stu);


        }


        //GET: /Detail/Edit/
        public ActionResult Edit(int? id, StudentTable student)
        {
            //student student = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49336/api/");
                var val = "Student/" + id;
                HttpResponseMessage response = client.GetAsync(val).Result;


                if (response.IsSuccessStatusCode)
                {
                    
                    var v = response.Content.ReadAsStringAsync();
                    //readTask.Wait();

             
                    student = JsonConvert.DeserializeObject<StudentTable>(v.Result);
                }
            }

            return View(student);
        }





        [HttpPost]
        public ActionResult Edit(StudentTable student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49336/api/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PutAsJsonAsync<StudentTable>("student/" + student.id, student).Result;
                if (response.IsSuccessStatusCode)
                {

                    // return RedirectToAction("Index");
                    Console.WriteLine("Success");


                }
            }
            return View("Index");
        }




        //GET: /Detail/Delete/
        public ActionResult Delete(int? id, StudentTable student)
        {
            //student student = null;
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49336/api/");
                var val = "Student/" + id;
                HttpResponseMessage response = client.DeleteAsync(val).Result;



                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("success");
                }
            }

            return View("Index");
        }










    }
}






