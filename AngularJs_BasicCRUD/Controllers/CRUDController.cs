using AngularJs_BasicCRUD.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularJs_BasicCRUD.Controllers
{
    public class CRUDController : Controller
    {
        //
        // GET: /CRUD/

        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CRUD()
        {
            return View();
        }
        [HttpGet()]
        public ActionResult CRUDAjax()
        {
            return View();
        }

    }

}

