using Project2_QLSVLDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project2_QLSVLDA.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap");
            }
            else
            {
                return View();
            }
        }

        public ActionResult DangNhap()
        {
            return View();
        }


        [HttpPost]
        public ActionResult DangNhap(string user, string password)
        {
            // Check DB
            QL_PROJECTEntities db = new QL_PROJECTEntities();
            int demTaiKhoan = db.USERACCOUNTs.Count(m => m.ID.ToLower() == user.ToLower() && m.password == password);
            // Check Code 
            if (demTaiKhoan == 1)
            {
                // lưu vào session

                if (user.ToUpper() == "admin".ToUpper())
                {
                    Session["user"] = user.ToUpper();
                    ViewBag.user = user.ToUpper();

                    return RedirectToAction("Index");
                }
                else if (db.SINHVIENs.Any(m => m.massv.ToUpper() == user.ToUpper()))
                {
                    Session["user"] = user.ToUpper();
                    ViewBag.user = user.ToUpper();
                    return RedirectToAction("Index", "SinhVienHome", new { area = "Student" });
                }

                else if (db.GIANGVIENs.Any(m => m.magv.ToUpper() == user.ToUpper()))
                {
                    Session["user"] = user.ToUpper();
                    ViewBag.user = user.ToUpper();
                    return RedirectToAction("Index", "GiaoVienHome", new { area = "Teacher" });
                }

                else
                {
                    return View();
                }

            }
            else
            {
                TempData["error"] = "Tài khoản đăng nhập không đúng";
                return View();
            }

        }

        #region Dang xuat 
        public ActionResult DangXuat()
        {
            Session.Remove("user");

            //xóa session from authen
            FormsAuthentication.SignOut();

            return RedirectToAction("DangNhap");
        }
        #endregion

    }
}