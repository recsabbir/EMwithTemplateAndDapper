using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;
using Dapper;

namespace WebApplication1.Controllers
{
    public class EmployeesController : Controller
    {
        
        // GET: Employees
        public ActionResult Index()
        {
            return View();
        }


        // GET: Employees/GetEmployees
        public JsonResult GetEmployees()
        {

            List<Employee> employeeList = new List<Employee>();
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["EMEntity"].ConnectionString))
            {

                employeeList = db.Query<Employee>("Select * From Employees").ToList();
            }


            return Json(employeeList, JsonRequestBehavior.AllowGet);
        }


        //GET: Employees/GetEmployee/5
        public JsonResult GetEmployee(int id)
        {
            Employee employee = new Employee();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["EMEntity"].ConnectionString))
            {

                employee = db.Query<Employee>("Select * From Employees Where id = " + id).FirstOrDefault();
            }



            return Json(employee, JsonRequestBehavior.AllowGet);
        }


        //POST: Employees/CreateEmployee
        [HttpPost]
        public bool CreateEmployee(Employee employee)
        {
            int newId = 1;
            Employee emplast;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["EMEntity"].ConnectionString))
            {

                emplast = db.Query<Employee>("Select * From Employees").ToList().LastOrDefault();
            }

            if (emplast != null)
            {
                newId = emplast.id + 1;
            }

            employee.id = newId;
            string im = employee.pic;

            if (employee.ImageFile != null)
            {
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
                string extension = Path.GetExtension(employee.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                employee.pic = fileName;

                fileName = Path.Combine(Server.MapPath("~/Content/Employees/"), fileName);

                employee.ImageFile.SaveAs(fileName);
            }

            
            string sqlQueryStart = "Insert Into Employees (id,";
            string sqlQueryEnd = " Values (" + employee.id + ",";

            if (employee.name != null)
            {
                sqlQueryStart += "name";
                sqlQueryEnd += "'" + employee.name + "'";
            }
            if (employee.dob != null)
            {
                sqlQueryStart += ",dob";
                sqlQueryEnd += ",'" + employee.dob.Value.Year + "-" + employee.dob.Value.Month + "-" + employee.dob.Value.Day + "'";
            }
            if (employee.nid != null)
            {
                sqlQueryStart += ",nid";
                sqlQueryEnd += ",'" + employee.nid + "'";
            }
            if (employee.bgroup != null)
            {
                sqlQueryStart += ",bgroup";
                sqlQueryEnd += ",'" + employee.bgroup + "'";
            }
            if (employee.email != null)
            {
                sqlQueryStart += ",email";
                sqlQueryEnd += ",'" + employee.email + "'";
            }
            if (employee.phone != null)
            {
                sqlQueryStart += ",phone";
                sqlQueryEnd += ",'" + employee.phone + "'";
            }
            if (employee.address != null)
            {
                sqlQueryStart += ",address";
                sqlQueryEnd += ",'" + employee.address + "'";
            }
            if (employee.pic != null)
            {
                sqlQueryStart += ",pic";
                sqlQueryEnd += ",'" + employee.pic + "'";
            }

            sqlQueryStart += ")";
            sqlQueryEnd += ")";

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["EMEntity"].ConnectionString))
            {
                string sqlQuery = sqlQueryStart+ sqlQueryEnd;
                db.Execute(sqlQuery);

            }

            return true;

        }


        //POST: Employees/UpdateEmployee
        [HttpPost]
        public bool UpdateEmployee(Employee employee)
        {
            Employee emp = new Employee();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["EMEntity"].ConnectionString))
            {

                emp = db.Query<Employee>("Select * From Employees Where id = " + employee.id).FirstOrDefault();
            }

            if (emp.CheckEqual(employee) && employee.ImageFile == null)
            {
                return true;
            }

            string im = emp.pic;

            if (employee.ImageFile != null)
            {
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
                string extension = Path.GetExtension(employee.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                im = fileName;

                fileName = Path.Combine(Server.MapPath("~/Content/Employees/"), fileName);

                employee.ImageFile.SaveAs(fileName);


            }

            string sqlQuery = "Update Employees Set ";
            int udateCount = 0;

            if (emp.name != employee.name)
            {
                sqlQuery += "name='" + employee.name + "'";
                udateCount++;
            }
            if (emp.dob != employee.dob)
            {
                if (udateCount > 0)
                {
                    sqlQuery += ",";
                }
                sqlQuery += "dob='" + employee.dob.Value.Year +"-"+ employee.dob.Value.Month + "-" + employee.dob.Value.Day + "'";
                udateCount++;
            }
            if (emp.nid != employee.nid)
            {
                if (udateCount > 0)
                {
                    sqlQuery += ",";
                }
                sqlQuery += "nid='" + employee.nid + "'";
                udateCount++;
            }
            if (emp.bgroup != employee.bgroup)
            {
                if (udateCount > 0)
                {
                    sqlQuery += ",";
                }
                sqlQuery += "bgroup='" + employee.bgroup + "'";
                udateCount++;
            }
            if (emp.email != employee.email)
            {
                if (udateCount > 0)
                {
                    sqlQuery += ",";
                }
                sqlQuery += "email='" + employee.email + "'";
                udateCount++;
            }
            if (emp.phone != employee.phone)
            {
                if (udateCount > 0)
                {
                    sqlQuery += ",";
                }
                sqlQuery += "phone='" + employee.phone + "'";
                udateCount++;
            }
            if (emp.address != employee.address)
            {
                if (udateCount > 0)
                {
                    sqlQuery += ",";
                }
                sqlQuery += "address='" + employee.address + "'";
                udateCount++;
            }
            if (emp.pic != im)
            {
                if (udateCount > 0)
                {
                    sqlQuery += ",";
                }
                sqlQuery += "pic='" + im + "'";
                udateCount++;
            }

            sqlQuery += " Where id=" + employee.id;

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["EMEntity"].ConnectionString))
            {

                int rowsAffected = db.Execute(sqlQuery);
            }
            //sqlQuery = "Update Employees Set name='New Employee',dob='1995-9-30',nid=88183453627',bgroup='B+',email='newemployee@gmail.com',phone='01234223232',address='abc Road, Dhaka',pic='New Employee211837057.jpg' Where id=3"
            return true;
        }


        //POST: Employees/DeleteEmployee/5
        [HttpPost]
        public bool DeleteEmployee(int id)
        {
            Employee employee = new Employee();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["EMEntity"].ConnectionString))
            {

                employee = db.Query<Employee>("Select * From Employees Where id = " + id).FirstOrDefault();
            }

            if (employee == null)
            {
                return false;
            }

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


            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["EMEntity"].ConnectionString))
            {
                string sqlQuery = "Delete From Employees Where id = " + employee.id;
                int rowsAffected = db.Execute(sqlQuery);
            }

            return true;
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            
            return View("Create");
        }

        //        // POST: Employees/Create
        //        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult Create(Employee employee)
        //        {
        //            var employeeList = db.Employees.ToList();
        //            int newId = employeeList.Last().id + 1;

        //            employee.id = newId;

        //            if (employee.name == null || employee.email == null)
        //            {
        //                ViewBag.empId = newId;

        //                return View("Create", employee);
        //            }
        //            else{

        //            }

        //            db.Employees.Add(employee);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");

        //        }

        //        // GET: Employees/Edit/5
        //        public ActionResult Edit(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            Employee employee = db.Employees.Find(id);
        //            if (employee == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View("Edit", employee);
        //        }

        //        // POST: Employees/Edit/5
        //        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public void Edit([Bind(Include = "id,name,dob,nid,bgroup,email,phone,address")] Employee employee)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                db.Entry(employee).State = EntityState.Modified;
        //                db.SaveChanges();

        //                string redir = "/Employees/Edit/" + employee.id;
        //                Response.Redirect(redir);
        //            }

        //        }

        //        // POST: Employees/EditPicture/5
        //        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public void EditPicture(Employee emp)
        //        {
        //            Employee employee = db.Employees.Find(emp.id);
        //            //if (employee == null)
        //            //{
        //            //    return HttpNotFound();
        //            //}
        //            //string rootFolder = @"D:\Temp\Data\";
        //            string im = employee.pic;
        //            if (im == null)
        //            {
        //                im = "a.jpg";
        //            }
        //            if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Content/Employees/"), im)))
        //            {
        //                // If file found, delete it    
        //                System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/Employees/"), im));

        //            }


        //            //string fileName = Path.GetFileNameWithoutExtension(emp.ImageFile.FileName);
        //            string fileName = employee.name;
        //            string extension = Path.GetExtension(emp.ImageFile.FileName);
        //            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

        //            employee.pic = fileName;

        //            fileName = Path.Combine(Server.MapPath("~/Content/Employees/"), fileName);

        //            emp.ImageFile.SaveAs(fileName);

        //            db.Entry(employee).State = EntityState.Modified;
        //            db.SaveChanges();
        //            string redir = "/Employees/Edit/" + employee.id;

        //            Response.Redirect(redir);

        //        }

        //        // GET: Employees/Delete/5
        //        public ActionResult Delete(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            Employee employee = db.Employees.Find(id);
        //            if (employee == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(employee);
        //        }

        //        // POST: Employees/Delete/5
        //        [HttpPost, ActionName("Delete")]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult DeleteConfirmed(int id)
        //        {
        //            Employee employee = db.Employees.Find(id);
        //            db.Employees.Remove(employee);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

        //        // POST: Employees/Delete/5
        //        //[HttpDelete]
        //        [HttpPost]
        //        public JsonResult DeleteEmployee(int id)
        //        {
        //            string m;

        //            Employee employee = db.Employees.Find(id);
        //            if (employee == null)
        //            {
        //                m = "message : Wrong ID";
        //                return Json(m, JsonRequestBehavior.AllowGet);
        //            }
        //            db.Employees.Remove(employee);
        //            db.SaveChanges();
        //            m = "message : success";
        //            return Json(m, JsonRequestBehavior.AllowGet);
        //        }




        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
