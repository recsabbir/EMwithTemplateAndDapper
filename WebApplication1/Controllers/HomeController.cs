using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        //private EMEntity db = new EMEntity();

        public ActionResult Index()
        {
            //ViewBag.totalEmployee = db.Employees.ToList().Count;

            return View("NewIndex");
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public string Login( Employee employee)
        {
            return "Logged In";
        }

        
    }
}