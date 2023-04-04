
using Department_Store.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Web.UI;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace Department_Store.Controllers
{
    public class StoreController : Controller
    {
        DbStore data = new DbStore();
        // GET: Store
        public ActionResult Home(int? page)
        {
            if (page == null) page = 1;
            var item = (from hh in data.SanPhams select hh).Where(m => m.Slgton > 0).OrderBy(m => m.MaSP); ;
            int pageSize = 8;
            int pageNum = page ?? 1;
            return View(item.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Search(int? page, string Search)
        {
            ViewBag.KeyWord = Search;
            if (page == null) page = 1;
            var item = (from hh in data.SanPhams select hh).Where(m => m.Slgton > 0 && m.TenSP.Contains(Search)).OrderBy(m => m.MaSP); ;
            int pageSize = 8;
            int pageNum = page ?? 1;
            return View(item.ToPagedList(pageNum, pageSize));
        }
        public ActionResult ThongTinSP(int? id)
        {
            var item = data.SanPhams.Where(m => m.MaSP == id).First();
            return View(item);
        }
    }
}