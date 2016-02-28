using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CMR.Models;
using Microsoft.AspNet.Identity;
using CMR.Custom;
using CMR.ViewModels;
using System.Collections.Generic;
using System;

namespace CMR.Controllers
{
    
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Courses
        [AccessDeniedAuthorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        // GET: Courses/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        [AccessDeniedAuthorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AccessDeniedAuthorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "Id,code,name")] Course course)
        {
            if (ModelState.IsValid)
            {
                var currentUser = db.Users.Find(User.Identity.GetUserId());
                //course.Creator = currentUser;
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        [AccessDeniedAuthorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AccessDeniedAuthorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "Id,code,name")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        [AccessDeniedAuthorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AccessDeniedAuthorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AccessDeniedAuthorize(Roles = "Administrator")]
        public ActionResult Assign(int? id)
        {
            AssignModel am = new AssignModel();
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            am.Course = course;
            var roleId = db.Roles.Single(r => r.Name == "Course Leader").Id;
            List<ApplicationUser> courseLeaders = db.Users.Where(u => u.Roles.Any(r => r.RoleId == roleId)).ToList<ApplicationUser>();
            am.CourseLeaders = courseLeaders;
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(am);
        }

        [HttpPost, ActionName("Assign")]
        [ValidateAntiForgeryToken]
        [AccessDeniedAuthorize(Roles = "Administrator")]
        public ActionResult AssignConfirmed(int id, string CourseLeaders)
        {
            Course course = db.Courses.Find(id);
            ApplicationUser user = db.Users.Find(CourseLeaders);
            Assignment assignment = new Assignment();
            assignment.Course = course;
            assignment.Manager = user;
            assignment.AssignDate = DateTime.Now;
            db.Assignment.Add(assignment);
            db.SaveChanges();
            TempData["message"] = "Assign Completed";
            return RedirectToAction("Assign", new { id = id });
        }

        [AccessDeniedAuthorize(Roles = "Course Leader")]
        public ActionResult Assigned()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
            return View(currentUser);
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
