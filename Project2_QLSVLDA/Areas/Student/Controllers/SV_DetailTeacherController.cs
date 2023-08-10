using Project2_QLSVLDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project2_QLSVLDA.Areas.Student.Controllers
{
    public class SV_DetailTeacherController : Controller
    {
        // GET: Student/SV_DetailTeacher
        [HttpGet]
        [Route("SV_DetailTeacher/Details/{id}")]
        public ActionResult Details(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "HomeAdmin", new { area = "Admin" });
            }
            else
            {

                QL_PROJECTEntities db = new QL_PROJECTEntities();
                var userID = id;

                var giaovien = db.GIANGVIENs.FirstOrDefault(gv => gv.magv == userID);

                //var sinhvien = ketqua.FirstOrDefault();
                if (giaovien != null)
                {
                    var magv = giaovien.magv;
                    var ten = giaovien.ten;
                    var sdt = giaovien.sdt;
                    var email = giaovien.email;
                    var chucdanh = giaovien.chucdanh;
                    var congtac = giaovien.congtac;

                    ViewBag.magv = magv;
                    ViewBag.ten = ten;
                    ViewBag.sdt = sdt;
                    ViewBag.email = email;
                    ViewBag.chucdanh = chucdanh;
                    ViewBag.congtac = congtac;

                }
                return View();
            }
        }
    }
}