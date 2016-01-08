using System.Web.Mvc;
using Application.Orchestrators;
using Application.ViewModel;
using System.Collections.Generic;
using System.IO;
using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Linq;
using System.Text;
using CsvHelper;

namespace Slp.Controllers
{
    [RoutePrefix("Admin")]
    public class AdminController : Controller
    {
        private AdminOrchestrator adminOrchestrator { get; set; }

        private const string _success = "success";

        public AdminController()
        {
            adminOrchestrator = new AdminOrchestrator();
        }


        // GET: Admin
        public ActionResult Index()
        {            
            return View(adminOrchestrator.GetLists());
        }

        public ActionResult AllApplications()
        {
            return View();
        }

        public ActionResult Extracts()
        {
            return View();
        }

        [Route("AllMembers/{appId}")]
        public JsonResult AllMembers(Guid appId)
        {
            AppViewModel avm = adminOrchestrator.CompletedAppMembers(appId);
            return Json(avm, JsonRequestBehavior.AllowGet);
        }      

        public FileStreamResult CSV()
        {
            MemoryStream ms;
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter))
            {
                csvWriter.WriteRecords(adminOrchestrator.AllAppData());
                streamWriter.Flush();
                var result = memoryStream.ToArray();
                ms = new MemoryStream(result);
            }
            return new FileStreamResult(ms, "text/csv") { FileDownloadName = "SlpApplications.csv" };
        }


        public void Excel() {
            var grid = new GridView();
            grid.DataSource = adminOrchestrator.AllAppData();
            grid.DataBind();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=SlpApplications.xls");
            Response.ContentType = "text/excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(sw);
            grid.RenderControl(htmlTextWriter);
            Response.Write(sw.ToString());
            Response.End();
        }
    
        [Route("List/{type}")]
        public ActionResult List(string type) {
            NameValueViewModel nvm = adminOrchestrator.GetLists().Where(a => a.NameWithoutBlanks.ToLower() == type.ToLower()).FirstOrDefault();
            return View(nvm);
        }

        [Route("ListViewModel/{id}")]
        public JsonResult GetListViewModel(int id)
        {
            return Json(adminOrchestrator.GetListViewModel(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveListViewModel(ListViewModel lvm)
        {
            adminOrchestrator.SaveListViewModel(lvm);
            return GetListViewModel(lvm.ListId);
        }

    } 
}