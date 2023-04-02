using Department_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Department_Store.Controllers
{
    public class DanhMucController : Controller
    {

        DbStore data = new DbStore();
        // GET: Danhmuc
        public ActionResult DanhMuc()
        {
            var listdm = from dm in data.DanhMucs select dm;
            return PartialView(listdm);
        }

        public ActionResult DanhMucSP(int id)
        {
            var list = data.SanPhams.Where(m => m.MaDM == id).ToList();
            return View(list);
        }
    }
}