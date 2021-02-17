using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.IO;
using Dapper;

namespace WebApplication1.Controllers
{
    public class EmployeeWithJQController : Controller
    {
        //private EMEntity db = new EMEntity();

        // GET: EmployeeWithJQ
        public ActionResult Index()
        {
            return View();
        }

        // GET: EmployeeWithJQ/GetEmployee
        //public JsonResult GetEmployees()
        //{

        //    List<Employee> employeeList = new List<Employee>();
        //    using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["EMEntity"].ConnectionString))
        //    {

        //        employeeList = db.Query<Employee>("Select * From Employees").ToList();
        //    }


        //    return Json(employeeList, JsonRequestBehavior.AllowGet);
        //}

        //GET: EmployeeWithJQ/GetEmployee/5
        //public JsonResult GetEmployee(int id)
        //{
        //    Employee employee = db.Employees.Find(id);


        //    return Json(employee, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetLastEmployee()
        //{
        //    //return Json(employeeList);
        //    return Json(db.Employees.ToList().Last(), JsonRequestBehavior.AllowGet);
        //}


        /*public string GetEmployee()

        {
            var employeeList = db.Employees.ToList();
            //return Json(employeeList);
            return JsonConvert.SerializeObject(employeeList);
        }*/

        // GET: Employees/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(id);
        //}

        //public JsonResult EmployeeDetail(int? id)
        //{
        //    if (id == null)
        //    {
        //        return null;
        //    }
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return null;
        //    }
        //    return Json(employee, JsonRequestBehavior.AllowGet);
        //}

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id,name,dob,nid,bgroup,email,phone,address")] Employee employee)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(employee).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(employee);
        //}


        //POST: EmployeesWithJQ/CreateEmployee
        //[HttpPost]
        //public bool CreateEmployee(Employee employee)
        //{
        //    var employeeList = db.Employees.ToList();
        //    int newId = 1;

        //    if (employeeList.Count > 0)
        //    {
        //        newId =  employeeList.Last().id + 1;
        //    }

        //    employee.id = newId;
        //    string im = employee.pic;

        //    if (employee.ImageFile != null)
        //    { 
        //        if (im == null)
        //        {
        //            im = "a.jpg";
        //        }
        //        if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Content/Employees/"), im)))
        //        {
        //            // If file found, delete it
        //            System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/Employees/"), im));
        //        }


        //        //string fileName = Path.GetFileNameWithoutExtension(emp.ImageFile.FileName);
        //        string fileName = employee.name;
        //        string extension = Path.GetExtension(employee.ImageFile.FileName);
        //        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

        //        employee.pic = fileName;

        //        fileName = Path.Combine(Server.MapPath("~/Content/Employees/"), fileName);

        //        employee.ImageFile.SaveAs(fileName);
        //    }

        //    db.Employees.Add(employee);
        //    db.SaveChanges();

        //    //need to send message
        //    //return Json(employee, JsonRequestBehavior.AllowGet);
        //    return true;

        //}


        //POST: EmployeeWithJQ/UpdateEmployee
        //[HttpPost]
        //public JsonResult UpdateEmployee(Employee employee)
        //{
        //    Employee emp = db.Employees.Find(employee.id);

        //    if(emp.CheckEqual(employee) && employee.ImageFile == null)
        //    {
        //        return Json(employee, JsonRequestBehavior.AllowGet);
        //    }


        //    string im = employee.pic;

        //    if (employee.ImageFile != null)
        //    {
        //        if (im == null)
        //        {
        //            im = "a.jpg";
        //        }
        //        if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Content/Employees/"), im)))
        //        {
        //            // If file found, delete it

        //            System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/Employees/"), im));

        //        }


        //        //string fileName = Path.GetFileNameWithoutExtension(emp.ImageFile.FileName);
        //        string fileName = employee.name;
        //        string extension = Path.GetExtension(employee.ImageFile.FileName);
        //        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

        //        emp.pic = fileName;

        //        fileName = Path.Combine(Server.MapPath("~/Content/Employees/"), fileName);

        //        employee.ImageFile.SaveAs(fileName);


        //    }

        //    if(emp.name != employee.name)
        //    {
        //        emp.name = employee.name;
        //    }
        //    if (emp.dob != employee.dob)
        //    {
        //        emp.dob = employee.dob;
        //    }
        //    if (emp.nid != employee.nid)
        //    {
        //        emp.nid = employee.nid;
        //    }
        //    if (emp.bgroup != employee.bgroup)
        //    {
        //        emp.bgroup = employee.bgroup;
        //    }
        //    if (emp.email != employee.email)
        //    {
        //        emp.email = employee.email;
        //    }
        //    if (emp.phone != employee.phone)
        //    {
        //        emp.phone = employee.phone;
        //    }
        //    if (emp.address != employee.address)
        //    {
        //        emp.address = employee.address;
        //    }


        //    db.Entry(emp).State = EntityState.Modified;
        //    db.SaveChanges();

        //    //need to send message
        //    return Json(employee, JsonRequestBehavior.AllowGet);

        //}


        // POST: EmployeeWithJQ/Delete/5

        //[HttpPost]
        //public JsonResult DeleteEmployee(int id)
        //{
        //    string m;

        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        m = "message : Wrong ID";
        //        return Json(m, JsonRequestBehavior.AllowGet);
        //    }

        //    string im = employee.pic;

        //    if (im == null)
        //    {
        //        im = "a.jpg";
        //    }
        //    if (System.IO.File.Exists(Path.Combine(Server.MapPath("~/Content/Employees/"), im)))
        //    {
        //        // If file found, delete it

        //        System.IO.File.Delete(Path.Combine(Server.MapPath("~/Content/Employees/"), im));

        //    }

        //    db.Employees.Remove(employee);
        //    db.SaveChanges();
        //    m = "message : success";
        //    return Json(m, JsonRequestBehavior.AllowGet);
        //}


    }
}