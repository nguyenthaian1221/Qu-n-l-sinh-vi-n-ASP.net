using Project2_QLSVLDA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Project2_QLSVLDA.Content.Custom.Class;
namespace Project2_QLSVLDA.Areas.Teacher.Controllers
{


    public class GVClassController : Controller
    {
        // GET: Teacher/GVClass
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "HomeAdmin", new { area = "Admin" });
            }
            else
            {
                QL_PROJECTEntities db = new QL_PROJECTEntities();
                var session = Session["user"].ToString();
                List<LOPMONHOC> lop = db.LOPMONHOCs.Where(m => m.magv == session).ToList();



                return View(lop);
            }
        }

        [HttpGet]
        [Route("Detail/{id}")]
        public ActionResult Detail(string id)
        {

            ClassID model = new ClassID();
            model.Id = id;
            // Gán giá trị cho các thuộc tính khác của model


            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "HomeAdmin", new { area = "Admin" });
            }
            else
            {

                return View(model);
            }
        }

    }
}