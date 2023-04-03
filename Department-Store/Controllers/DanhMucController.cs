using Department_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Department_Store.Models;

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
        public ActionResult DanhMucAdmin()
        {
            var listdm = from dm in data.DanhMucs select dm;
            return View(listdm);
        }
        public ActionResult DanhMucSP(int id)
        {
            var list = data.SanPhams.Where(m => m.MaDM == id).ToList();
            return View(list);
        }
        public ActionResult ThemDM()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemDM(FormCollection collection, DanhMuc dm)
        {

            var S_TenDM = collection["TenDM"];
            if (string.IsNullOrEmpty(S_TenDM))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                dm.TenDM = S_TenDM.ToString();
                data.DanhMucs.Add(dm);
                data.SaveChanges();
                return RedirectToAction("DanhMucAdmin");
            }
            return this.ThemDM();
        }
        public ActionResult XoaDM(int id, FormCollection collection)
        {
            var D_dm = data.DanhMucs.Where(m => m.MaDM == id).First();
            data.DanhMucs.Remove(D_dm);
            data.SaveChanges();
            return RedirectToAction("DanhMucAdmin");

        }
    }
}