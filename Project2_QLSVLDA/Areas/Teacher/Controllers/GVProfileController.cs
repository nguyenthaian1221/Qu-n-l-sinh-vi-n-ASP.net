using Project2_QLSVLDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project2_QLSVLDA.Areas.Teacher.Controllers
{
    public class GVProfileController : Controller
    {
        // GET: Teacher/GVProfile
        public ActionResult Index()
        {
            QL_PROJECTEntities db = new QL_PROJECTEntities();
            //var ketqua = from sv in db.SINHVIENs
            //             where sv.massv == Session["user"].ToString() 
            //             select sv;

            var userID = Session["user"].ToString();

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