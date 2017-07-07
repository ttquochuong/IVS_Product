using BL.Product;
using Core;
using DTO.Product;
using IVS_QuocHuong.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IVS_QuocHuong.Controllers
{
    public class MeasureController : Controller
    {
        // GET: Measure
        public ActionResult MeasureSearch(MeasureDTO measure)
        {
            MeasureBL bl = new MeasureBL();
            MeasureList model = new MeasureList();
            DataTable dt;
            bl.SearchData(measure, out dt);
            model.Measures = CommonMethod.DataTableToList<MeasureDTO>(dt);
            return View(model);
        }
    }
}