using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.WebPages;
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
                //string[] empname = employee.name.Trim().Split(' ');

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
            if (employee.joining_date != null)
            {
                sqlQueryStart += ",joining_date";
                sqlQueryEnd += ",'" + employee.joining_date.Value.Year + "-" + employee.joining_date.Value.Month + "-" + employee.joining_date.Value.Day + "'";
            }
            else
            {
                DateTime jDate = DateTime.Now;
                sqlQueryStart += ",joining_date";
                sqlQueryEnd += ",'" + jDate.Year + "-" + jDate.Month + "-" + jDate.Day + "'";

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
            int updateCount = 0;

            if (emp.name != employee.name)
            {
                sqlQuery += "name='" + employee.name + "'";
                updateCount++;
            }
            if (emp.dob != employee.dob && employee.dob != null)
            {
                if (updateCount > 0)
                {
                    sqlQuery += ",";
                }
                sqlQuery += "dob='" + employee.dob.Value.Year +"-"+ employee.dob.Value.Month + "-" + employee.dob.Value.Day + "'";
                updateCount++;
            }
            if (emp.nid != employee.nid)
            {
                if (updateCount > 0)
                {
                    sqlQuery += ",";
                }
                sqlQuery += "nid='" + employee.nid + "'";
                updateCount++;
            }
            if (emp.bgroup != employee.bgroup)
            {
                if (updateCount > 0)
                {
                    sqlQuery += ",";
                }
                sqlQuery += "bgroup='" + employee.bgroup + "'";
                updateCount++;
            }
            if (emp.email != employee.email)
            {
                if (updateCount > 0)
                {
                    sqlQuery += ",";
                }
                sqlQuery += "email='" + employee.email + "'";
                updateCount++;
            }
            if (emp.phone != employee.phone)
            {
                if (updateCount > 0)
                {
                    sqlQuery += ",";
                }
                sqlQuery += "phone='" + employee.phone + "'";
                updateCount++;
            }
            if (emp.address != employee.address)
            {
                if (updateCount > 0)
                {
                    sqlQuery += ",";
                }
                sqlQuery += "address='" + employee.address + "'";
                updateCount++;
            }
            if (emp.pic != im)
            {
                if (updateCount > 0)
                {
                    sqlQuery += ",";
                }
                sqlQuery += "pic='" + im + "'";
                updateCount++;
            }
            if (emp.joining_date != employee.joining_date && employee.joining_date != null)
            {

                sqlQuery += "joining_date='" + employee.joining_date.Value.Year + "-" + employee.joining_date.Value.Month + "-" + employee.joining_date.Value.Day + "'";
                updateCount++;
                
            }
            

            sqlQuery += " Where id=" + employee.id;
            if(updateCount > 0)
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["EMEntity"].ConnectionString))
                {

                    int rowsAffected = db.Execute(sqlQuery);
                }
            }
            

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

        // GET: Employees/UpdateSalaryInfo
        public ActionResult UpdateSalaryInfo()
        {
            
            return PartialView("_AddSalaryInfo");
        }


        // POST: Employees/UpdateSalaryInfo/"id:year-month-salary"
        [HttpPost]
        public bool UpdateSalaryInfo(string data)
        {
            if (data.Split(':').Length != 2)
            {
                return false;
            }
            else
            {
                if (data.Split(':')[1].Split('-').Length != 3 && !(data.Split(':')[1].Split('-')[2].IsInt()))
                {
                    return false;
                }
            }
            //sqlQuery = "Insert Into SalaryInfos (id,employee_id,year,month,salary) Values (1,2,2020,:,2)"


            var _empid = data.Split(':')[0];
            string[] _data = data.Split(':')[1].Split('-');
            var _year = _data[0];
            var _month = _data[1];
            var _salary = _data[2];


            int newId = 1;
            SalaryInfo sinfo;
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["EMEntity"].ConnectionString))
            {

                sinfo = db.Query<SalaryInfo>("Select * From SalaryInfos").ToList().LastOrDefault();
            }

            if (sinfo != null)
            {
                newId = sinfo.id + 1;
            }

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["EMEntity"].ConnectionString))
            {

                sinfo = db.Query<SalaryInfo>("Select * From SalaryInfos where employee_id=" + _empid + " AND year=" + _year + " AND month=" + _month ).LastOrDefault();
            }
            string sqlQuery;

            if (sinfo != null)
            {
                sqlQuery = "Update SalaryInfos Set salary=" + _salary + " WHERE id=" + sinfo.id;
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["EMEntity"].ConnectionString))
                {

                    int rowsAffected = db.Execute(sqlQuery);
                }
            }
            else
            {
                sqlQuery = "Insert Into SalaryInfos (id,employee_id,year,month,salary) Values (" + newId + "," + _empid + "," + _year + "," + _month + "," + _salary + ")";
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["EMEntity"].ConnectionString))
                {
                    db.Execute(sqlQuery);
                }
            }

            

            return true;
        }


        // GET: Employees/UpdateSalaryInfoEmployees
        public JsonResult GetSalaryInfoEmployees()
        {
            List<Employee> employeeList = new List<Employee>();
            
            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["EMEntity"].ConnectionString))
            {

                employeeList = db.Query<Employee>("Select id, name From Employees").ToList();
            }

            List<string> ret = new List<string>();
            for( int i=0; i< employeeList.Count; i++)
            {
                ret.Add(employeeList[i].id + " : " + employeeList[i].name);
            }

            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        

        // GET: Employees/GetJoiningDate/5
        public JsonResult GetJoiningDate(int id)
        {
            Employee emp = new Employee();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["EMEntity"].ConnectionString))
            {

                emp = db.Query<Employee>("Select joining_date From Employees Where id=" + id).FirstOrDefault();
            }

            
            return Json(emp, JsonRequestBehavior.AllowGet);
        }


        // GET: Employees/GetJoiningDate/5
        public JsonResult GetSalaryInfoSalary(string data)
        {
            var emp_id = data.Split(':')[0];
            var year = data.Split(':')[1].Split('-')[0];
            var month = data.Split(':')[1].Split('-')[1];

            SalaryInfo sinfo = new SalaryInfo();

            using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["EMEntity"].ConnectionString))
            {

               sinfo = db.Query<SalaryInfo>("Select salary From SalaryInfos Where employee_id=" + emp_id + " AND year=" + year + " AND month=" + month  ).FirstOrDefault();
            }

            if (sinfo == null)
            {
                sinfo = new SalaryInfo();
                sinfo.salary = 0;
            }
            return Json(sinfo, JsonRequestBehavior.AllowGet);
        }

        //POST: Employees/UpdateSalaryInfos
        //[HttpPost]


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
