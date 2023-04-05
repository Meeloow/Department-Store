using Department_Store.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace Department_Store.Controllers
{
    public class GioHangController : Controller
    {
        DbStore data = new DbStore();
        // GET: GioHang
        public List<GioHang> Laygiohang()
        {
            List<GioHang> lstGiohang = Session["GioHang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<GioHang>();
                Session["GioHang"] = lstGiohang;
            }
            return lstGiohang;
        }
        public ActionResult ThemGioHang(int id, string strURL)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.Find(n => n.masp == id);
            if (sanpham == null)
            {
                sanpham = new GioHang(id);
                lstGiohang.Add(sanpham);
            }
            else
            {
                sanpham.isoluong++;
            }
            return RedirectToAction("Home", "Store");
        }
        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Sum(n => n.isoluong);
            }
            return tsl;
        }
        private int TongSoLuongSanPham()
        {
            int tsl = 0;
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }
            return tsl;
        }
        private double TongTien()
        {
            double tt = 0;
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            if (lstGiohang != null)
            {
                tt = lstGiohang.Sum(n => n.dThanhtien);
            }
            return tt;
        }
        //[Authorize]
        public ActionResult GioHang()
        {
            if (Session["Giohang"] != null)
            {
                ViewBag.ThongBao = "Không có sản phẩm nào";
            }
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            Session["Tongslsp"] = TongSoLuong();
            return View(lstGiohang);
        }
        public ActionResult GioHangPartial()
        {
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return PartialView();
        }
        public ActionResult XoaGiohang(int id)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.masp == id);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.masp == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapnhatGiohang(int id, FormCollection collection)
        {
            List<GioHang> lstGiohang = Laygiohang();
            GioHang sanpham = lstGiohang.SingleOrDefault(n => n.masp == id);
            if (sanpham != null)
            {
                sanpham.isoluong = int.Parse(collection["txtSoLg"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("GioHang");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == null)
            {
                return RedirectToAction("DangNhap", "User");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Store");
            }
            List<GioHang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGiohang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            DonHang dh = new DonHang();
            KhachHang kh = (KhachHang)Session["Taikhoan"];
            SanPham s = new SanPham();
            HoaDon hd = new HoaDon();
            List<GioHang> gh = Laygiohang();
            dh.makh = kh.makh;
            dh.ngaydat = DateTime.Now;
            dh.ngaygiao = DateTime.Now.AddDays(3);
            data.DonHangs.Add(dh);
            data.SaveChanges();
            hd.NgayLapHD = DateTime.Now;
            hd.MaDH = dh.madon;
            int tt = 0;
            foreach (var item in gh)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.soluong = item.isoluong;
                ctdh.madon = dh.madon;
                ctdh.MaSP = item.masp;
                ctdh.gia = (int?)item.giaban;
                s = data.SanPhams.Single(n => n.MaSP == item.masp);
                s.Slgton -= ctdh.soluong;
                tt += Convert.ToInt32(item.giaban);
                data.ChiTietDonHangs.Add(ctdh);
                data.SaveChanges();
            }
            hd.ThanhTien = tt;
            data.HoaDons.Add(hd);
            data.SaveChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Home", "Store");
        }

        public ActionResult XacnhanDonhang()
        {
            return View();
        }
    }
}