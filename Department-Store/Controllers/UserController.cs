using Department_Store.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Department_Store.Controllers
{
    public class UserController : Controller
    {
        DbStore data = new DbStore();
        // GET: NguoiDung
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KhachHang kh)
        {
            var regexPassword = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            var hoten = collection["hoten"];
            var tendangnhap = collection["tendangnhap"];
            var matkhau = collection["matkhau"];
            var MatKhauXacNhan = collection["MatKhauXacNhan"];
            var email = collection["email"];
            var diachi = collection["diachi"];
            var dienthoai = collection["dienthoai"];
            var ngaysinh = string.Format("{0:MM/dd/yyyy}", collection["ngaysinh"]);
            if (tendangnhap == "Admin@gmail.com")
            {
                //ViewData["Admin"] = "Không thể tạo tài khoản admin!";
                return RedirectToAction("DangKy", "User");
            }
            else
            {
                if (!regexPassword.IsMatch(matkhau))
                {
                    ViewData["matkhau"] = "Mật khẩu không hợp lệ!";
                }
                else {
                    if (String.IsNullOrEmpty(MatKhauXacNhan))
                    {
                        ViewData["NhapMKXN"] = "Phải nhập mật khẩu xác nhận!";
                    }
                    else
                    {
                        if (!matkhau.Equals(MatKhauXacNhan))
                        {
                            ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khầu xác nhận phài giống nhau";
                        }
                        else
                        {
                            kh.hoten = hoten;
                            kh.tendangnhap = tendangnhap;
                            kh.matkhau = matkhau;
                            kh.email = email;
                            kh.diachi = diachi;
                            kh.dienthoai = dienthoai;
                            kh.ngaysinh = DateTime.Parse(ngaysinh);
                            data.KhachHangs.Add(kh);
                            data.SaveChanges();
                            return RedirectToAction("DangNhap");
                        }
                    }
                    
                }
                return this.DangKy();
            }            
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var tendangnhap = collection["tendangnhap"];
            var matkhau = collection["matkhau"];
            KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.tendangnhap == tendangnhap && n.matkhau == matkhau);
            if (kh != null)
            {
                if (tendangnhap == "Admin@gmail.com")
                {
                    Session["TaiKhoan"] = kh;                  
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    Session["TaiKhoan"] = kh;
                    return RedirectToAction("Home", "Store");
                }

            }
            else
            {
                ViewData["login"] = "Tên Đăng Nhập hoặc mật khẩu sai!";
                return RedirectToAction("DangNhap", "User");
            }
            
        }
        public ActionResult LogOff()
        {
            Session["TaiKhoan"] = null;
            Session.Abandon();
            return RedirectToAction("Home", "Store");
        }
    }
}
