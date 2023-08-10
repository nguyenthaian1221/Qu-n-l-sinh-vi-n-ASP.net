using Project2_QLSVLDA.Content.Custom.Class;
using Project2_QLSVLDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project2_QLSVLDA.Areas.Teacher.Controllers
{
    public class DetailStudentController : Controller
    {
        // GET: Teacher/DetailStudent

        [HttpGet]
        [Route("DetailStudent/Details/{id}")]
        public ActionResult Details(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "HomeAdmin", new { area = "Admin" });
            }
            else
            {
                //ClassID model = new ClassID();
                //model.Id = id;

                QL_PROJECTEntities db = new QL_PROJECTEntities();
                //var ketqua = from sv in db.SINHVIENs
                //             where sv.massv == Session["user"].ToString() 
                //             select sv;

                var userID = id;

                var sinhvien = db.SINHVIENs.FirstOrDefault(sv => sv.massv == userID);

                //var sinhvien = ketqua.FirstOrDefault();
                if (sinhvien != null)
                {
                    var mssv = sinhvien.massv;
                    var ten = sinhvien.ten;
                    var namsinh = sinhvien.namsinh;
                    var sdt = sinhvien.sdt;
                    var lop = sinhvien.lop;
                    var nienkhoa = sinhvien.nien_khoa;
                    var email = sinhvien.email;

                    ViewBag.mssv = mssv;
                    ViewBag.ten = ten;
                    ViewBag.namsinh = namsinh;
                    ViewBag.sdt = sdt;
                    ViewBag.lop = lop;
                    ViewBag.nienkhoa = nienkhoa;
                    ViewBag.email = email;

                }







                return View();
            }
        }
    }
}