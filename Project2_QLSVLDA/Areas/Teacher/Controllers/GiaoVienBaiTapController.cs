using Project2_QLSVLDA.Content.Custom.Class;
using Project2_QLSVLDA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Collections.Specialized.BitVector32;


namespace Project2_QLSVLDA.Areas.Teacher.Controllers
{
    public class GiaoVienBaiTapController : Controller
    {
        // GET: Teacher/GiaoVienBaiTap

        public ActionResult Index()
        {

            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "HomeAdmin", new { area = "Admin" });
            }
            else
            {

                return View();
            }
        }




        [HttpPost]
        public ActionResult ActionName(string ngaybatdau, string ngayketthuc, string giobatdau, string gioketthuc, string lopgianday, string nhomtronglop, string exercise_content)
        {

            QL_PROJECTEntities db = new QL_PROJECTEntities();

            string combinedDateTimeString = ngaybatdau + " " + giobatdau;
            DateTime start_time = DateTime.ParseExact(combinedDateTimeString, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            combinedDateTimeString = ngayketthuc + " " + gioketthuc;
            DateTime end_time = DateTime.ParseExact(combinedDateTimeString, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            string lop = lopgianday;
            string nhom = nhomtronglop;
            string nd = exercise_content.Trim();


            db.BAITAPs.Add(
                new BAITAP
                {
                    malop = lop,
                    manhom = nhom,
                    noidung = nd,
                    ngaybatdau = start_time,
                    ngayketthuc = end_time,
                }

                );
            db.SaveChanges();
            return RedirectToAction("Index", "GiaoVienBaiTap", new { area = "Teacher" });
        }



        public ActionResult XemDanhSachBaiTapSinhVienNop()
        {

            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "HomeAdmin", new { area = "Admin" });
            }
            else
            {

                return View();
            }
        }

        public ActionResult DanhSachBaiTapDaTao()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "HomeAdmin", new { area = "Admin" });
            }
            else
            {

                return View();
            }
        }


    }
}
