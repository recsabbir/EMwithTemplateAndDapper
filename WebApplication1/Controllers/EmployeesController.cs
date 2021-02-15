using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;

namespace WebApplication1.Controllers
{
    public class EmployeesController : Controller
    {
        private EMEntity db = new EMEntity();

        // GET: Employees
        public ActionResult Index()
        {
            var employeeList = db.Employees.ToList();
            return View(employeeList);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            var employeeList = db.Employees.ToList();
            int newId = employeeList.Last().id + 1;
            ViewBag.empId = newId;
            return View("Create");
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            var employeeList = db.Employees.ToList();
            int newId = employeeList.Last().id + 1;

            employee.id = newId;

            if (employee.name == null || employee.email == null)
            {
                ViewBag.empId = newId;

                return View("Create", employee);
            }
            else{

            }

            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
            
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View("Edit", employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void Edit([Bind(Include = "id,name,dob,nid,bgroup,email,phone,address")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();

                string redir = "/Employees/Edit/" + employee.id;
                Response.Redirect(redir);
            }
            
        }

        // POST: Employees/EditPicture/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void EditPicture(Employee emp)
        {
            Employee employee = db.Employees.Find(emp.id);
            //if (employee == null)
            //{
            //    return HttpNotFound();
            //}
            //string rootFolder = @"D:\Temp\Data\";
            string im = employee.pic;
            if (im == null)
            {
                im = "a.jpg";
            }
            if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Content/Employees/"), im)))
            {
                // If file found, delete it    
                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/Employees/"), im));
                
            }


            //string fileName = Path.GetFileNameWithoutExtension(emp.ImageFile.FileName);
            string fileName = employee.name;
            string extension = Path.GetExtension(emp.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            employee.pic = fileName;

            fileName = Path.Combine(Server.MapPath("~/Content/Employees/"), fileName);

            emp.ImageFile.SaveAs(fileName);

            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
            string redir = "/Employees/Edit/" + employee.id;
            
            Response.Redirect(redir);

        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Employees/Delete/5
        //[HttpDelete]
        [HttpPost]
        public JsonResult DeleteEmployee(int id)
        {
            string m;
            
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                m = "message : Wrong ID";
                return Json(m, JsonRequestBehavior.AllowGet);
            }
            db.Employees.Remove(employee);
            db.SaveChanges();
            m = "message : success";
            return Json(m, JsonRequestBehavior.AllowGet);
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
