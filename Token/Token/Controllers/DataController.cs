using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Token.Controllers
{
    public class DataController : ApiController
    {
        TokenDbEntities db = new TokenDbEntities();
        //[AllowAnonymous]
        //[HttpPost]
        
        //[Route("api/data/Register")]


        
        //public IHttpActionResult Register(TokenTable user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    else
        //    {
        //        db.TokenTables.Add(user);
        //        db.SaveChanges();
        //    }

           
        //}

        //[AllowAnonymous]
        //[HttpGet]
        //[Route("api/data/forall")]
        //public IHttpActionResult Get()
        //{
        //    return Ok("Now server time is: " + DateTime.Now.ToString());
        //}





        [AllowAnonymous]
        [HttpGet]
        [Route("api/data/role")]
        public IHttpActionResult GetRole()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                         .Where(c => c.Type == ClaimTypes.Role)
                         .Select(c => c.Value);
            return Ok(roles.ToList());

        }




        [Authorize]
        [HttpGet]
        [Route("api/data/authenticate")]
        public IHttpActionResult GetForAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello " + identity.Name);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("api/data/authorize/admin")]
        public IHttpActionResult GetForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);
            return Ok("Hello " + identity.Name + " Role: " + string.Join(",", roles.ToList()));
        }


        [Authorize(Roles = "staff")]
        [HttpGet]
        [Route("api/data/authorize/staff")]
        public IHttpActionResult GetForStaff()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);
            return Ok("Hello " + identity.Name + " Role: " + string.Join(",", roles.ToList()));
        }

    }
}
