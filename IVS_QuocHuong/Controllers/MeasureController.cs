﻿using BL.Product;
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
        public ActionResult MeasureSearch(MeasureSearch model)
        {
            if (!string.IsNullOrEmpty(model.SearchButton) || model.Page.HasValue)
            {
                MeasureDTO dto = new MeasureDTO();
                List<MeasureDTO> list = new List<MeasureDTO>();
                if (model.MeasureCode.IsNotNullOrEmpty())
                {
                    dto.code = model.MeasureCode;
                }
                if (model.MeasureName.IsNotNullOrEmpty())
                {
                    dto.name = model.MeasureName;
                }
                MeasureBL bl = new MeasureBL();  
                bl.SearchData(dto, out list);
                model.CountResult = list.Count;
                var pageIndex = model.Page ?? 1;
                model.SearchResults = list.ToPagedList(pageIndex, pageSize);
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
                measure.created_by = 0;
                measure.updated_by = 0;
                bl.InsertData(measure);
                return RedirectToAction("MeasureAdd");
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