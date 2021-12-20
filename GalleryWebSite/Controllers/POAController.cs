using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GalleryWebSite.Models;
using GalleryWebSite.Models.BO;
using GalleryWebSite.Models.BLL;


namespace GalleryWebSite.Controllers
{
    public class POAController : Controller
    {
        private POABLL bll = new POABLL();
        public ActionResult MosaicsPOA()
        {
            ViewBag.vbData = "mos";
            return View("POA"); 
        }

        public ActionResult StainGlassPOA()
        {
            ViewBag.vbData = "sg";
            return View("POA");
        }

        public ActionResult FusingPOA()
        {
            ViewBag.vbData = "fs";
            return View("POA");
        }

        public PartialViewResult GetAllByType(int poaType1, int poaType2)
        {
            List<PieceOfArt> model = bll.GetAllByType(poaType1, poaType2);
            return PartialView("_ListOfPOA", model);
        }

        public JsonResult GetAllPersByCN(int cn)
        {
            List<Perspective> model = bll.GetAllPOAPerspectivesByCN(cn);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPOAByCN(int cn)
        {
            PieceOfArt poa = bll.GetPOAByCN(cn);
            return Json(poa, JsonRequestBehavior.AllowGet);
        }

    }
}
