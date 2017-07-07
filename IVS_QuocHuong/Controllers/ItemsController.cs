﻿using Core;

using System.Data;
using System.Web.Mvc;
using BL.Product;
using DTO.Product;
using System;
using PagedList;
using System.Collections.Generic;
using IVS_QuocHuong.Model;

namespace IVS_QuocHuong.Controllers
{
    public class ItemsController : Controller
    {
        const int pageSize = 25;

        public ActionResult ItemSearch(ItemsSearch model)
        {
            if (!string.IsNullOrEmpty(model.SearchButton) || model.Page.HasValue)
            {

                ItemBL bl = new ItemBL();
                ItemsDTO dto = new ItemsDTO();
                List<ItemsDTO> result = new List<ItemsDTO>();
                if (model.ItemName.IsNotNullOrEmpty())
                {
                    dto.name = model.ItemName;
                }
                if (model.ItemCode.IsNotNullOrEmpty())
                {
                    dto.code = model.ItemCode;
                }
                if (model.Category.HasValue)
                {
                    dto.category_id = model.Category;
                }
                bl.SearchData(dto, out result);
                model.CountResult = result.Count;
                var pageIndex = model.Page ?? 1;
                model.SearchResults = result.ToPagedList(pageIndex, pageSize);
                     
            }
            CategoryBL category = new CategoryBL();
            DataTable categorydt;
            category.SearchList(out categorydt);
            CategoryDTO cate = new CategoryDTO();
            List<CategoryDTO> List = CommonMethod.DataTableToList<CategoryDTO>(categorydt);
            List.Add(cate);
            ViewBag.CategoryList = List;
            return View(model);
        }

        [HttpGet]
        public ActionResult ItemAdd()
        {
            CategoryBL category = new CategoryBL();
            MeasureBL measure = new MeasureBL();
            DataTable categorydt;
            DataTable measuredt;
            category.SearchList(out categorydt);
            measure.SearchList(out measuredt);
            ViewBag.CategoryList = CommonMethod.DataTableToList<CategoryDTO>(categorydt);
            ViewBag.MeasureList = CommonMethod.DataTableToList<CategoryDTO>(measuredt);
            return View();
        }

        [HttpPost]
        public ActionResult ItemAdd(ItemsDTO model)
        {
         
            if (ModelState.IsValid)
            {
                if (model.discontinued_datetime.IsNotNullOrEmpty() == true)
                {
                    string datetime = DateTime.Parse(model.discontinued_datetime).ToString("yyyy-MM-dd HH:mm:ss");
                    model.discontinued_datetime = datetime;
                }
                model.created_by = 0;
                model.updated_by = 0;
                ItemBL bl = new ItemBL();
                bl.InsertData(model);
                return RedirectToAction("ItemSearch");
            }
            else
            {
             CategoryBL category = new CategoryBL();
            MeasureBL measure = new MeasureBL();
            DataTable categorydt;
            DataTable measuredt;
            category.SearchList(out categorydt);
            measure.SearchList(out measuredt);
            ViewBag.CategoryList = CommonMethod.DataTableToList<CategoryDTO>(categorydt);
            ViewBag.MeasureList = CommonMethod.DataTableToList<CategoryDTO>(measuredt);
                return View(model);
            }
        
        }

        [HttpGet]
        public ActionResult ItemUpdate(int id)
        {
            CategoryBL category = new CategoryBL();
            MeasureBL measure = new MeasureBL();
            ItemBL bl = new ItemBL();
            DataTable categorydt;
            DataTable measuredt;
            ItemsDTO dto = new ItemsDTO();      
            bl.SearchID(id, out dto);
            ItemsAdd model = new ItemsAdd();
            model.Item = dto;
            category.SearchList(out categorydt);
            measure.SearchList(out measuredt);
            ViewBag.CategoryList = CommonMethod.DataTableToList<CategoryDTO>(categorydt);
            ViewBag.MeasureList = CommonMethod.DataTableToList<CategoryDTO>(measuredt);
            return View(model);
        }

        [HttpPost]
        public ActionResult ItemUpdate(ItemsDTO model)
        {
            string datetime = DateTime.Parse(model.discontinued_datetime).ToString("yyyy-MM-dd HH:mm:ss");
            model.updated_by = 65;
            model.discontinued_datetime = datetime;
            ItemBL bl = new ItemBL();
            bl.UpdateData(model);
            return RedirectToAction("ItemSearch");
        }

        [HttpGet]
        public ActionResult ItemDetele(int? id)
        {
            
            ItemBL bl = new ItemBL();
            List<ItemsDTO> dtResult;
            ItemsDTO dto = new ItemsDTO();
            dto.id = id;
            bl.SearchData(dto, out dtResult);
            if (dtResult.Count > 0)
            {
                bl.DeleteData(id);
            }
            return RedirectToAction("ItemSearch");
        }

        [HttpPost]
        public ActionResult ItemDeleteMulti(FormCollection frm)
        {
            string[] ids = frm["ItemID"].Split(new char[] { ',' });
            ItemBL bl = new ItemBL();
            ItemsDTO dtResult;
           
            foreach(string item in ids)
            {
                ItemsDTO dto = new ItemsDTO();
                int id = item.ParseInt32();
                int equal=bl.SearchID(id, out dtResult);
                if (dtResult != null)
                {
                    bl.DeleteData(id);
                }
            }
            return RedirectToAction("ItemSearch");
        }
    }
}