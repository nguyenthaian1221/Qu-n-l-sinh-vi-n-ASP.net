using Project2_QLSVLDA.Content.Custom.Class;
using Project2_QLSVLDA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project2_QLSVLDA.Areas.Student.Controllers
{
    public class SV_MeetingController : Controller
    {
        // GET: Student/SV_Meeting
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

        [HttpGet]
        [Route("Book/{id}")]
        public ActionResult Book(string id, string macuochen, string magiaovien)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "HomeAdmin", new { area = "Admin" });
            }
            else
            {
                ClassID model = new ClassID();
                model.Id = id;

                return View(model);
            }
        }



        [HttpPost]
        public ActionResult DienThongTin(string lopgianday, string nhomtronglop, string macuochen, string magiaovien)
        {

            //tblFileDetail model = new tblFileDetail();
            //List<tblFileDetail> list = new List<tblFileDetail>();
            ;
            DateTime timenow = DateTime.Now;
            var mssv = Session["user"].ToString();

            string lop = lopgianday;
            string nhom = nhomtronglop;
            string mahen = macuochen;
            string magv = magiaovien;

            using (QL_PROJECTEntities db = new QL_PROJECTEntities())
            {

                var cuochen = db.CUOCHENs.FirstOrDefault(s => s.macuochen.ToString() == mahen);

                if (cuochen != null)
                {
                    // Thực hiện sửa giá trị cần thay đổi
                    // Thay thế "Ten" bằng tên cột cần sửa và "New Name" bằng giá trị mới.
                    cuochen.tinhtrang = 2;
                    // Lưu thay đổi vào cơ sở dữ liệu
                    db.SaveChanges();
                }

                db.SINHVIENCUOCHENs.Add(new SINHVIENCUOCHEN
                {
                    macuochen = int.Parse(mahen),
                    masinhvien = mssv,
                    manhom = nhom,
                    magiaovien = magv,
                    thoigiandat = timenow,
                    malop = lop

                });
                db.SaveChanges();
            }

            TempData["Message"] = "Successfully Booked";
            return RedirectToAction("Index");
        }


        public ActionResult HistoryBook()
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