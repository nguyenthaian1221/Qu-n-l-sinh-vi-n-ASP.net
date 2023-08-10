using Project2_QLSVLDA.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project2_QLSVLDA.Areas.Student.Controllers
{
    public class SubmitAssignmentController : Controller
    {
        // GET: Student/SubmitAssignment
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
        [Route("DetailAndSubmit/{id}")]
        public ActionResult DetailAndSubmit(string id,string malop_pass)
        {

            if (Session["user"] == null)
            {
                return RedirectToAction("DangNhap", "HomeAdmin", new { area = "Admin" });
            }
            else
            {
                QL_PROJECTEntities db = new QL_PROJECTEntities();
                var noidung_baitap = (from m in db.BAITAPs
                                      where id == m.mabaitap.ToString().Trim()
                                      select m.noidung).First();
                ViewBag.noidung = noidung_baitap;

                var student = Session["user"].ToString();

                var a = db.SINHVIENBAITAPs.Where(m => m.masinhvien == student && m.mabaitap.ToString() == id && malop_pass == m.malop).FirstOrDefault();

                if (a == null)
                {
                   
                    return View(); // Trả về một View khác để hiển thị thông báo
                }
                else
                {
                    return View(a);
                }

                
            }

        }





        #region Upload file
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file, int id, string exercise_content)
        {

            tblFileDetail model = new tblFileDetail();
            List<tblFileDetail> list = new List<tblFileDetail>();
            QL_PROJECTEntities db = new QL_PROJECTEntities();
            DateTime timenow = DateTime.Now;
            var mssv = Session["user"].ToString();
            var mabt = id;
            var cmt = exercise_content;
            var _malop = (from m in db.BAITAPs
                          where mabt.ToString() == m.mabaitap.ToString().Trim()
                          select m.malop).First();


            #region Xu ly viec upload vao csdl
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

            if (file != null)
            {
                var Extension = Path.GetExtension(file.FileName);
                var Orgininame = Path.GetFileNameWithoutExtension(file.FileName);
                var fileName = Orgininame + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Extension;
                string path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                model.FILEURL = Url.Content(Path.Combine("~/UploadedFiles/", fileName));
                model.FILENAME = fileName;
                #region Luu lai toan bo form vao bang SINHVIENBAITAP


                var path_luu_csdl = Url.Content(Path.Combine("~/UploadedFiles/", fileName));
                var name_luu_csdl = fileName;



                //Xử lý lưu
                db.SINHVIENBAITAPs.Add(
                    new SINHVIENBAITAP
                    {
                        masinhvien = mssv,
                        mabaitap = mabt,
                        path = path_luu_csdl,
                        thoigiannop = timenow,
                        malop = _malop,
                        tenfile = name_luu_csdl,
                        comment = cmt
                    });

                db.SaveChanges();

                #endregion

                if (SaveFile(model))
                {
                    file.SaveAs(path);


                    TempData["AlertMessage"] = "Uploaded Successfully !!";
                    return RedirectToAction("Index");


                }
                else
                {
                    ModelState.AddModelError("", "Error In Add File. Please Try Again !!!");
                }


            }
            else
            {
                ModelState.AddModelError("", "Please Choose Correct File Type !!");
                //return View(model);
            }

            #endregion





            return RedirectToAction("Index");
        }

        #endregion



        #region SaveFile
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
        #endregion



        #region Download file
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
        #endregion



    }
}