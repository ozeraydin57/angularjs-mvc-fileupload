using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvcangular.Controllers
{
    public class FileUploadController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        const string FILE_UPLOAD_DIRECTORY = "~/Uploads/";

        [HttpPost]
        public ActionResult FileUploadMethod(string requestID, string usertype)
        {
            if (Request.Files.Count == 0)
            {
                return Json(new { success = true, message = "Success" }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                //Request.file
                string filepath = FILE_UPLOAD_DIRECTORY + requestID + "/" + usertype + "/";
                foreach (string s in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[s];
                    string filename = file.FileName;
                    string fileext = file.ContentType;

                    if (!string.IsNullOrEmpty(filename))
                    {
                        string fileExtension = Path.GetExtension(filename);

                        if (!Directory.Exists(Server.MapPath(filepath)))
                            Directory.CreateDirectory(Server.MapPath(filepath));

                        string pathToSave_100 = Server.MapPath(filepath) + filename;
                        file.SaveAs(pathToSave_100);
                    }

                }

                return Json(new { success = true, message = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error" }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult FileList(string requestID, string usertype)
        {

            List<string> fileList = new List<string>();
            try
            {
                DirectoryInfo list2 = new DirectoryInfo(Server.MapPath(string.Format("~/Uploads/{0}/{1}/", requestID, usertype)));
                var asd = list2.GetFiles();
                //var list = Directory.GetFiles(Server.MapPath(string.Format("~/Uploads/{0}/{1}/", requestID, usertype)));
                foreach (var item in asd)
                {
                    fileList.Add(item.Name);
                }
            }
            catch
            {
            }

            return Json(fileList, JsonRequestBehavior.AllowGet);
        }
    }
}