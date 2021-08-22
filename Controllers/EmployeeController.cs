using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task4_Hr.DB_Manage;
using Task4_Hr.Models;

namespace Task4_Hr.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        Connection con;
        Home h = new Home();
        public EmployeeController()
        {
            con = new Connection();
        }
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult AddEmployee(FormCollection form)
        {
            var cmdTrans = con.CreateTransaction();
           var b = h.SaveEmployee(form, cmdTrans);
            string json = JsonConvert.SerializeObject("", Formatting.None);
            return Json(int.Parse(b));

        }
        public JsonResult EditEmployee(FormCollection form)
        {
            var id = form["EmpID"];
            var cmdTrans = con.CreateTransaction();
            var b = h.UpdateEmployee(form, cmdTrans);
            string json = JsonConvert.SerializeObject("", Formatting.None);
            return Json(int.Parse(b));

        }
        public JsonResult DeleteEmployee(int id)
        {
            var cmdTrans = con.CreateTransaction();
            var b = h.DeleteEmployee(id, cmdTrans);
            string json = JsonConvert.SerializeObject("", Formatting.None);
            return Json(json, JsonRequestBehavior.AllowGet);

        }
    }
}