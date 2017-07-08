using BL.Product;
using Core;
using DTO.Product;
using IVS_QuocHuong.Model;
using PagedList;
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
        const int pageSize = 25;
        // GET: Measure
        [HttpGet]
        public ActionResult MeasureSearch(string Page,MeasureSearch model)
        {
            MeasureDTO dto = new MeasureDTO();
            ModelState.Clear();
            if (!string.IsNullOrEmpty(model.SearchButton) || Page.IsNotNullOrEmpty())
            {

                if (Page != null)
                {
                    dto.page = int.Parse(Page);
                    model.Page = dto.page;
                }
                MeasureBL bl = new MeasureBL();
                List<MeasureDTO> result = new List<MeasureDTO>();
                if (model.MeasureCode.IsNotNullOrEmpty())
                {
                    dto.code = model.MeasureCode;
                }
                if (model.MeasureName.IsNotNullOrEmpty())
                {
                    dto.name = model.MeasureName;
                }
                bl.SearchData(dto, out result);
                model.PageCount = bl.PageCount(dto);
                model.SearchResults = new StaticPagedList<MeasureDTO>(result, model.Page, 20, model.PageCount);
            }
   
            
            return View(model);
        }

        [HttpGet]
        public ActionResult MeasureAdd()
        {

            return View();
        }
        [HttpPost]
        public ActionResult MeasureAdd(MeasureDTO measure)
        { 
            if (ModelState.IsValid)
            {
                MeasureBL bl = new MeasureBL();
                if (bl.CheckCode(measure) > 0)
                {
                    TempData["Error"] = "Insert fail";
                    TempData["Success"] = "";
                }
                else
                {
                    measure.created_by = 0;
                    measure.updated_by = 0;
                    bl.InsertData(measure);
                    TempData["Error"] = "";
                    TempData["Success"] = "Insert successfully";
                    return RedirectToAction("MeasureAdd");
                }
           
            }
            return View(measure);
        }

        [HttpGet]
        public ActionResult MeasureDelete(int id)
        {
            MeasureBL bl = new MeasureBL();
            List<MeasureDTO> dtResult = new List<MeasureDTO>();
            MeasureDTO measureDTO = new MeasureDTO();
            measureDTO.id = id;
            int count=bl.SearchData(measureDTO, out dtResult);
            
            if (count == 1)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                bl.DeleteData(id);
            }    
            return RedirectToAction("MeasureSearch");
        }

        [HttpGet]
        public ActionResult MeasureUpdate(int id)
        {
            MeasureBL bl = new MeasureBL();
            List<MeasureDTO> dtResult = new List<MeasureDTO>();
            MeasureDTO measure = new MeasureDTO();
            measure.id = id;
            bl.SearchData(measure, out dtResult);
            measure = dtResult[0];
            return View(measure);

        }

        [HttpPost]
        public ActionResult MeasureUpdate(MeasureDTO measure)
        {
            MeasureBL bl = new MeasureBL();
            measure.created_by = 0;
            measure.updated_by = 0;
            bl.UpdateData(measure);
            return RedirectToAction("MeasureSearch");
        }


        [HttpPost]
        public ActionResult MeasureDeleteMulti(FormCollection frm)
        {
            string[] ids = frm["MeasureID"].Split(new char[] { ',' });
            MeasureBL bl = new MeasureBL();
            foreach (string item in ids)
            {
                MeasureDTO measureDTO = new MeasureDTO();
                int id = item.ParseInt32();
                measureDTO.id = id;
                List<MeasureDTO> dtos = new List<MeasureDTO>();
                int returncode=bl.SearchData(measureDTO, out dtos);
                if (returncode ==1)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                else
                {
                    bl.DeleteData(id);
                }
            }
            return RedirectToAction("MeasureSearch");
        }


    }
}