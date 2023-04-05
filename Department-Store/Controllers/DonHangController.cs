using Department_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Department_Store.Controllers
{
    public class DonHangController : Controller
    {
        DbStore data = new DbStore();
        // GET: DonHang

        public ActionResult XemDonHang(int? id)
        {
            var donhang = (from dh in data.DonHangs select dh);
            return View(donhang);
        }
        public ActionResult XemChiTietDonHang(int? id)
        {
            var ctdonhang = (from dh in data.ChiTietDonHangs select dh).Where(m => m.madon == id).First(); 
            return View(ctdonhang);
        }
        //public ActionResult HuyDonHang(int id, FormCollection collection)
        //{
        //    var D_ctdh = data.ChiTietDonHangs.Where(m => m.madon == id).First();
        //    data.ChiTietDonHangs.Remove(D_ctdh);
        //    data.SaveChanges();
        //    return RedirectToAction("Index");

        //}
    }
}