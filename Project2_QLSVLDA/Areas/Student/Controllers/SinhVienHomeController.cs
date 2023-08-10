using Project2_QLSVLDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project2_QLSVLDA.Areas.Student.Controllers
{
    public class SinhVienHomeController : Controller
    {
        // GET: Student/SinhVienHome
        QL_PROJECTEntities db = new QL_PROJECTEntities();
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

        public JsonResult GetEvents()
        {

            string str = Session["user"].ToString();
            var list_cuoc_hen_sinh_vien = (from ch in db.CUOCHENs
                                          join svch in db.SINHVIENCUOCHENs on ch.macuochen equals svch.macuochen
                                          where svch.masinhvien.Trim() == str
                                          select ch).ToList();



            return Json(list_cuoc_hen_sinh_vien.AsEnumerable().Select(e => new
            {
                title = e.diadiem,
                start = e.thoigianbatdau.Value.ToString("yyyy/MM/dd HH:mm"),
                end = e.thoigianketthuc.Value.ToString("yyyy/MM/dd HH:mm")

            }).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}
