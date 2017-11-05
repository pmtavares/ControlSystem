using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ControlSystem.Models;
using ControlSystem.Classes;

namespace ControlSystem.Controllers
{
    public class UsersController : Controller
    {
        private ContextControl db = new ContextControl();

        // GET: Users

        //[Authorize]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        //[Authorize(Users = "pedro@pedro.com")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Users = "pedro@pedro.com")]
        public ActionResult Create(UserView userView)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(userView.User);
                try
                {
                    if(userView != null)
                    {
                        var pic = Utilities.UploadPhoto(userView.Photo);


                        if (!string.IsNullOrEmpty(pic))
                        {
                            userView.User.Photo = string.Format("~/Content/Photos/{0}", pic);
                        }
                        
                    }
                    db.SaveChanges();

                    Utilities.CreateUserASP(userView.User.UserName);

                    if(userView.User.Student)
                    {
                        Utilities.AddRoleToUser(userView.User.UserName, "Student");
                    }
                    if (userView.User.Teacher)
                    {
                        Utilities.AddRoleToUser(userView.User.UserName, "Teacher");
                    }
                    

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(userView);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var view = new UserView
            {
                User = user
            };
            return View(view);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserView userView)
        {
            if (ModelState.IsValid)
            {
                var db2 = new ContextControl();

                var oldUser = db2.Users.Find(userView.User.UserId);

                db2.Dispose();

                if (userView.Photo != null)
                {
                    var pic = Utilities.UploadPhoto(userView.Photo);
                    
                    if (!string.IsNullOrEmpty(pic))
                    {
                        userView.User.Photo = string.Format("~/Content/Photos/{0}", pic);
                    }

                }
                else
                {

                    userView.User.Photo = oldUser.Photo;

                }


                db.Entry(userView.User).State = EntityState.Modified;
                try
                {

                    if(oldUser != null && oldUser.UserName != userView.User.UserName)
                    {
                        Utilities.ChangeEmailUserASP(oldUser.UserName, userView.User.UserName);
                                              
                    }
                    db.SaveChanges();
                    
                }
                catch (Exception ex)
                {
                    
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(userView);
                }


                return RedirectToAction("Index");
            }
            return View(userView.User);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
