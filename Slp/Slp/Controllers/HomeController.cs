using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using Application.ViewModel;
using Application.Orchestrators;

namespace Slp.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        private AppOrchestrator appOrchestrator { get; set; }

        private const string _success = "success";

        public HomeController() {
            appOrchestrator = new AppOrchestrator();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            //Guid appId = new Guid("5030623C-288F-404F-8744-E43F94BDBFB6");//use this for existing app 
            Guid appId = Guid.NewGuid();
            return View(appId);
        }

        [Route("Application/{appId}")]
        public ActionResult Application(Guid appId)
        {
            return View(appId);
        }

        [Route("Confirmation/{appId}")]
        public ActionResult Confirmation(Guid appId)
        {
            return View(appId);
        }

        [Route("GetApplication/{appId}")]
        public JsonResult GetAplication(Guid appId)
        {
            AppViewModel avm = new AppViewModel();            
            avm = appOrchestrator.GetAppViewModel(appId);
            return Json(avm, JsonRequestBehavior.AllowGet);
        }

        [Route("NewMember/{IsChild}/{appId}")]
        public JsonResult GetNewMember(string IsChild, Guid appId)
        {
            return Json(appOrchestrator.GetNewMember(appId, IsChild=="1"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MemberSave(MemberViewModel mvm)
        {
            List<MemberViewModel> cvmdb = appOrchestrator.MemberSave(mvm);
            return Json(cvmdb, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MemberDelete(MemberViewModel mvm)
        {
            List<MemberViewModel> cvmdb = appOrchestrator.MemberDelete(mvm);
            return Json(cvmdb, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveAssistanceProgram(AssistanceProgramViewModel apvm)
        {
            appOrchestrator.SaveAssistanceProgram(apvm);
            return Json(_success, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveStep3(Step3ViewModel svm)
        {
            appOrchestrator.SaveStep3(svm);
            return Json(_success, JsonRequestBehavior.AllowGet);
        }       

        [HttpPost]
        public JsonResult SaveContactInformation(ContactViewModel cvm)
        {
            appOrchestrator.SaveContactInformation(cvm);
            return Json(_success, JsonRequestBehavior.AllowGet);
        }

        [Route("MemberIncome/{memberId}")]
        public JsonResult GetNewMemberIncome(Guid memberId)
        {
            return Json(appOrchestrator.GetMemberIncome(memberId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveMemberIncome(IncomeViewModel ivm)
        {
            List<MemberViewModel> mdb = appOrchestrator.SaveMemberIncome(ivm);
            return Json(mdb, JsonRequestBehavior.AllowGet);
        }
    }
}
