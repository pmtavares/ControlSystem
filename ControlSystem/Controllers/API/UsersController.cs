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
using ControlSystem.Models;
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ControlSystem.Classes;

namespace ControlSystem.Controllers.API
{

    [RoutePrefix("API/Users")]
    public class UsersController : ApiController
    {
        private ContextControl db = new ContextControl();

        //Login
        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login(JObject form)
        {
            string email = string.Empty;
            string password = string.Empty;
            dynamic jsonObject = form; // to receive the form parameter

            try
            {
                email = jsonObject.Email.Value;
                password = jsonObject.Password.Value;
            }
            catch
            {
                return this.BadRequest("Incorrect Call");
            }

            var userContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userASP = userManager.Find(email, password);

            if (userASP == null)
            {
                return this.BadRequest("User or password invalids");
            }

            var user = db.Users.Where(u => u.UserName == email).FirstOrDefault();

            if (user == null)
            {
                return this.BadRequest("User or password invalids");
            }

            return this.Ok(user);
        }


        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;
            var db2 = new ContextControl();

            var oldUser = db2.Users.Find(user.UserId);

            db2.Dispose();
            try
            {

                if (oldUser != null && oldUser.UserName != user.UserName)
                {
                    Utilities.ChangeEmailUserASP(oldUser.UserName, user.UserName);


                }
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //return StatusCode(HttpStatusCode.NoContent);
            return this.Ok(user);
        }

        // POST: api/Users
        //[ResponseType(typeof(User))]
        public IHttpActionResult PostUser(UserPassword userPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User()
            {
                Address = userPassword.Address,
                
                Surname = userPassword.Surname,
                Phone = userPassword.Phone,
                UserName = userPassword.UserName
            };

            try
            {
                                
                db.Users.Add(userPassword);
                db.SaveChanges();
                Utilities.CreateUserASP(userPassword.UserName, userPassword.Password); //add to the other table
                //Utilities.AddRoleToUser(userPassword.UserName, "Student"); //add student permission
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            userPassword.Teacher = false;
            userPassword.Student = true;
            userPassword.UserId = user.UserId; //after saved


            //return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
            return this.Ok(userPassword);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.UserId == id) > 0;
        }
    }
}