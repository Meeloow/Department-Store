using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Department_Store.Models;
using PagedList;

namespace Department_Store.Controllers
{
    public class AdminController : Controller
    {
       DbStore data = new DbStore();
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var item = (from hh in data.SanPhams select hh).Where(m => m.Slgton > 0).OrderBy(m => m.MaSP); ;
            int pageSize = 10;
            int pageNum = page ?? 1;
            return View(item.ToPagedList(pageNum, pageSize));
            //return View(item);
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
                return RedirectToAction("DanhMuc");
            }
            return this.ThemDM();
        }
        public ActionResult ThemSP()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemSP(FormCollection collection, SanPham sp)
        {

            var S_TenSP = collection["TenSP"];
            var S_GiaSP = Convert.ToDecimal(collection["GiaSP"]);
            var S_Slgton = Convert.ToInt32(collection["Slgton"]);
            var S_HinhSP = collection["HinhSP"];
            var S_NgayNhap = Convert.ToDateTime(collection["NgayNhap"]);
            if (string.IsNullOrEmpty(S_TenSP))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                sp.TenSP = S_TenSP.ToString();
                sp.HinhSP = S_HinhSP.ToString();
                sp.GiaSP = (int?)S_GiaSP;
                sp.NgayNhap = S_NgayNhap;
                sp.Slgton = S_Slgton;
                data.SanPhams.Add(sp);
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            return this.ThemSP();
        }
        public ActionResult SuaSP(int id)
        {
            var S_sanpham = data.SanPhams.First(m => m.MaSP == id);
            return View(S_sanpham);
        }
        [HttpPost]
        public ActionResult SuaSP(int id, FormCollection collection)
        {
            var S_sanpham = data.SanPhams.First(m => m.MaSP == id);
            var S_TenSP = collection["TenSP"];
            var S_HinhSP = collection["HinhSP"];
            var S_GiaSP = Convert.ToDecimal(collection["GiaSP"]);
            var S_NgayNhap = Convert.ToDateTime(collection["NgayNhap"]);
            var S_Slgton = Convert.ToInt32(collection["SlgTon"]);
            S_sanpham.MaSP = id;
            if (string.IsNullOrEmpty(S_TenSP))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                S_sanpham.TenSP = S_TenSP;
                S_sanpham.HinhSP = S_HinhSP;
                S_sanpham.GiaSP = (int?)S_GiaSP;
                S_sanpham.NgayNhap = S_NgayNhap;
                S_sanpham.Slgton = S_Slgton;
                UpdateModel(S_sanpham);
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            return this.SuaSP(id);

        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
        public ActionResult XoaSP(int id)
        {
            var D_sp = data.SanPhams.First(m => m.MaSP == id);
            return View(D_sp);
        }
        [HttpPost]
        public ActionResult XoaSP(int id, FormCollection collection)
        {
            var D_sp = data.SanPhams.Where(m => m.MaSP == id).First();
            data.SanPhams.Remove(D_sp);
            data.SaveChanges();
            return RedirectToAction("Index");

        }

    }

}