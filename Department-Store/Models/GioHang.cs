using Department_Store.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Department_Store.Models
{
    public class GioHang
    {
        DbStore data = new DbStore();
        public int masp { get; set; }
        [Display(Name = "Tên Sản Phẩm")]
        public string tensp { get; set; }
        [Display(Name = "Ảnh bìa")]
        public string hinh { get; set; }
        [Display(Name = "Giá bán")]
        public Double giaban { get; set; }
        [Display(Name = "số lượng")]
        public int isoluong { get; set; }
        [Display(Name = "Thành tiền")]
        public Double dThanhtien
        {
            get { return isoluong * giaban; }
        }
        public GioHang(int id)
        {
            masp = id;
            SanPham sp = data.SanPhams.Single(n => n.MaSP == masp);
            tensp = sp.TenSP;
            hinh = sp.HinhSP;
            giaban = double.Parse(sp.GiaSP.ToString());
            isoluong = 1;
        }

    }
}
