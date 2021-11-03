using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartAdminNetCore.Controllers
{
    public class CategoryController : Controller
    {
        private dynamic expObj;
        public CategoryController()
        {
            expObj = new ExpandoObject();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult _list()
        {
            return PartialView();
        }


        public IActionResult Action(string id="")
        {
            string recId = Lib.CryptoEngine.Decrypt(id);
            expObj.recId = recId;
            return View(expObj);
        }
    }
}
