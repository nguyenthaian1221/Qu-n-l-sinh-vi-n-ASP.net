using Project2_QLSVLDA.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project2_QLSVLDA.Areas.Teacher.Controllers
{
    public class GiaoVienCuocHenController : Controller
    {
        // GET: Teacher/GiaoVienCuocHen
        QL_PROJECTEntities db = new QL_PROJECTEntities();
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "HomeAdmin", new { area = "Admin" });
            }
            else
            {
                var session = Session["user"].ToString();




                List<CUOCHEN> ketqua = (from cuochen in db.CUOCHENs
                                        where cuochen.magiaovien.Trim() == session
                                        select cuochen
                              ).ToList();



                return View(ketqua);
            }

        }

        [HttpPost]
        public ActionResult ActionName(string ngay, string giobatdau, string gioketthuc, string diadiem)
        {

            string combinedDateTimeString = ngay + " " + giobatdau;
            DateTime start_time = DateTime.ParseExact(combinedDateTimeString, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            combinedDateTimeString = ngay + " " + gioketthuc;
            DateTime end_time = DateTime.ParseExact(combinedDateTimeString, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            string location = diadiem.Trim();
            string mgv = Session["user"].ToString();

            db.CUOCHENs.Add(
                new CUOCHEN
                {
                    thoigianbatdau = start_time,
                    thoigianketthuc = end_time,
                    diadiem = location,
                    magiaovien = mgv,
                    tinhtrang = 1, // mặc định set bằng 1 vì mới tạo

                });

            db.SaveChanges();
            return RedirectToAction("Index", "GiaoVienCuocHen", new { area = "Teacher" });
        }

        public ActionResult DanhSachHen()
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
        public ActionResult ActionName_Nhanxet(string id_meeting, string gv_comment)
        {
            var ses = Session["user"].ToString();
            using (QL_PROJECTEntities db = new QL_PROJECTEntities())
            {

                var cuochen = db.CUOCHENs.FirstOrDefault(s => s.macuochen.ToString() == id_meeting.Trim() && ses == s.magiaovien.Trim() && s.tinhtrang != 1);

                if (cuochen != null)
                {
                    // Thực hiện sửa giá trị cần thay đổi
                    // Thay thế "Ten" bằng tên cột cần sửa và "New Name" bằng giá trị mới.
                    cuochen.ghichu = gv_comment;
                    // Lưu thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();
                    TempData["Message"] = "Successfully save";
                }
                else { TempData["Message"] = "Failed! Check ID!"; }

            }


            return Redirect("~/Teacher/GiaoVienCuocHen/DanhSachHen");

        }
      
        [HttpPost]
        public ActionResult CapNhatCuocHen(int macuochen, DateTime thoigianbatdau, DateTime thoigianketthuc, string diadiem)
        {
            // Kết nối với cơ sở dữ liệu
            //string connectionString = "metadata=res://*/Models.Model1.csdl|res://*/Models.Model1.ssdl|res://*/Models.Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=LAPTOP-RVBD3PGM\MSSQLSERVER04;initial catalog=QL_PROJECT;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;";
            using (QL_PROJECTEntities db = new QL_PROJECTEntities())
            {
                // Tìm bản ghi hẹn theo ID
                var cuochen = db.CUOCHENs.FirstOrDefault(m => m.macuochen == macuochen);

                if (cuochen != null)
                {
                    // Cập nhật thông tin
                    cuochen.thoigianbatdau= thoigianbatdau;
                    cuochen.thoigianketthuc = thoigianketthuc;
                    cuochen.diadiem = diadiem;

                    // Lưu thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();

                    ViewBag.Message = "Thông tin đã được cập nhật thành công!";
                }
                else
                {
                    ViewBag.Message = "Không tìm thấy bản ghi cần cập nhật!";
                }
            }

            // Chuyển hướng về trang gốc hoặc trang khác (tùy vào yêu cầu của bạn)
            return RedirectToAction("Index", "GiaoVienCuocHen", new { area = "Teacher" });
        }
    

        public ActionResult XoaCuocHen(int idmacuochen)
        {
            var updateModel = db.CUOCHENs.SingleOrDefault(m => m.macuochen == idmacuochen);
            db.CUOCHENs.Remove(updateModel);
            db.SaveChanges();
            return RedirectToAction("Index","GiaoVienCuocHen", new { area = "Teacher" });


        }

    }

}

