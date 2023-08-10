using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Project2_QLSVLDA.Models;
using System.Data.Entity;

namespace Project2_QLSVLDA.Areas.Admin.Controllers
{
    public class FilesController : Controller
    {
        // GET: Admin/Files

        // GET: Files  
        public ActionResult Index()
        {

            using (var db = new QL_PROJECTEntities())
            {
                List<tblFileDetail> list = db.tblFileDetails.ToList();
                return View(list);
            }


        }

        //private List<tblFileDetail> GetFileDetails()
        //{
        //    using (var db = new QL_PROJECTEntities1())
        //    {
        //        return db.tblFileDetails.ToList();
        //    }

        //}

        private bool SaveFile(tblFileDetail model)
        {
            using (var dbContext = new QL_PROJECTEntities())
            {
                try
                {
                    var fileDetail = new tblFileDetail
                    {
                        FILENAME = model.FILENAME,
                        FILEURL = model.FILEURL
                    };

                    dbContext.tblFileDetails.Add(fileDetail);
                    dbContext.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase files)
        {
            tblFileDetail model = new tblFileDetail();
            List<tblFileDetail> list = new List<tblFileDetail>();

            using (var dbContext = new QL_PROJECTEntities())
            {
                var fileDetails = dbContext.tblFileDetails.ToList();
                foreach (var fileDetail in fileDetails)
                {
                    list.Add(new tblFileDetail
                    {
                        SQLID = fileDetail.SQLID,
                        FILENAME = fileDetail.FILENAME,
                        FILEURL = fileDetail.FILEURL
                    });
                }
            }

            //model.FileList = list;

            if (files != null)
            {
                var Extension = Path.GetExtension(files.FileName);
                var Orgininame = Path.GetFileNameWithoutExtension(files.FileName);
                var fileName = Orgininame + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Extension;
                string path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                model.FILEURL = Url.Content(Path.Combine("~/UploadedFiles/", fileName));
                model.FILENAME = fileName;

                if (SaveFile(model))
                {
                    files.SaveAs(path);


                    TempData["AlertMessage"] = "Uploaded Successfully !!";
                    return RedirectToAction("Index", "Files");
                }
                else
                {
                    ModelState.AddModelError("", "Error In Add File. Please Try Again !!!");
                }
            }
            else
            {
                ModelState.AddModelError("", "Please Choose Correct File Type !!");
                return View(model);
            }

            return RedirectToAction("Index", "Files");
        }


        public ActionResult DownloadFile(string filePath)
        {
            string fullName = Server.MapPath("~" + filePath);

            byte[] fileBytes = GetFile(fullName);
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filePath);
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

    }
}
