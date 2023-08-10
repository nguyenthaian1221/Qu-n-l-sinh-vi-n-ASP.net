using Project2_QLSVLDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project2_QLSVLDA.Areas.Teacher.Controllers
{
    public class GiaoVienHomeController : Controller
    {
        // GET: Teacher/GiaoVienHome
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
            var cuochen_allsystem = (from ch in db.CUOCHENs
                                     join gv in db.GIANGVIENs on ch.magiaovien equals gv.magv
                                     join svch in db.SINHVIENCUOCHENs on ch.macuochen equals svch.macuochen
                                     join sv in db.SINHVIENs on svch.masinhvien equals sv.massv
                                     select new
                                     {
                                         sv.ten,
                                         ch.macuochen,
                                         ch.magiaovien,
                                         ch.thoigianbatdau,
                                         ch.thoigianketthuc,
                                         ch.diadiem,
                                         svch.thoigiandat,
                                         ch.ghichu,
                                         svch.masinhvien,
                                         svch.manhom,
                                         svch.malop

                                     }).ToList();


            var list_cuoc_hen = cuochen_allsystem.Where(p => p.magiaovien.ToString().Trim() == str).ToList();


            return Json(list_cuoc_hen.AsEnumerable().Select(e => new
            {
                title = e.diadiem,
                start = e.thoigianbatdau.Value.ToString("yyyy/MM/dd HH:mm"),
                end = e.thoigianketthuc.Value.ToString("yyyy/MM/dd HH:mm")

            }).ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}