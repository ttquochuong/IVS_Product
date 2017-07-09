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
    public class CategoryController : Controller
    {
        
        // GET: Caterogy
        public ActionResult CategorySearch(string Page,CategorySearch model)
        {
            CategoryBL bl = new CategoryBL();
            CategoryDTO dto = new CategoryDTO();
            if (!string.IsNullOrEmpty(model.SearchButton) || Page.IsNotNullOrEmpty())
            {

                if (Page != null)
                {
                    dto.page = int.Parse(Page);
                    model.Page = dto.page;
                }
          
                List<CategoryDTO> result = new List<CategoryDTO>();
                if (model.CategoryCode.IsNotNullOrEmpty())
                {
                    dto.code = model.CategoryCode;
                }
                if (model.CategoryName.IsNotNullOrEmpty())
                {
                    dto.name = model.CategoryName;
                }
                bl.SearchData(dto, out result);
                model.PageCount = bl.CountPage(dto);     
                model.SearchResults = new StaticPagedList<CategoryDTO>(result, model.Page, 20, model.PageCount);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult CategoryAdd()
        {
            CategoryBL catelogy = new CategoryBL();
            List<CategoryDTO> catelogydt;
            catelogy.SearchList(out catelogydt);
            ViewBag.CaterogyList = catelogydt;
            return View();
        }

        [HttpPost]
        public ActionResult CategoryAdd(CategoryDTO category)
        {
            CategoryBL catelogy = new CategoryBL();
            List<CategoryDTO> catelogydt;
            catelogy.SearchList(out catelogydt);
            ViewBag.CaterogyList = catelogydt;
            if (ModelState.IsValid)
            {
                CategoryBL bl = new CategoryBL();
                int code = bl.CheckCode(category);
                if (code > 0)
                {
                    TempData["Error"] = "Code already exists";
                }
                else
                {
                    TempData["Error"] = "";
                    category.created_by = 0;
                    category.updated_by = 0;
                    int result = bl.InsertData(category);
                    if (result == 1)
                    {
                        TempData["Error"] = "Insert badly";
                        TempData["Success"] = "";
                        return View(category);
                    }
                    else
                    {
                        TempData["Error"] = "";
                        TempData["Success"] = "Insert successfully";
                        return RedirectToAction("CategoryAdd");
                    }
                        
                }
               
            }
            return View(category);
        }

        //Delete category
        [HttpGet]
        public ActionResult CategoryDelete(int id)
        {
            CategoryBL bl = new CategoryBL();
            CategoryDTO categoryDTO = new CategoryDTO();
            List<CategoryDTO> dtos = new List<CategoryDTO>();
            categoryDTO.id = id;
            bl.SearchData(categoryDTO, out dtos);
            if (dtos.Count<=0)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                int result=bl.DeleteData(id);
                if (result == 1)
                {
                    TempData["Error"] = "Delete badly";
                    TempData["Success"] = "";
                    
                }
                else
                {
                    TempData["Error"] = "";
                    TempData["Success"] = "Delete successfully";
                }
            }

            return RedirectToAction("CategorySearch");
        }

        [HttpGet]
        public ActionResult CategoryUpdate(int id)
        {
            ModelState.Clear();
            CategoryBL bl = new CategoryBL();
            CategoryDTO category = new CategoryDTO();   
            List<CategoryDTO> viewbag; 
            category.id = id;
            List<CategoryDTO> dtos = new List<CategoryDTO>();
            bl.SearchData(category, out dtos);
            CategoryDTO model=new CategoryDTO();
            model= dtos[0];
            bl.SearchListUp(model,out viewbag);
            ViewBag.CaterogyList = viewbag;
            return View(model);
        }
        [HttpPost]
        public ActionResult CategoryUpdate(CategoryDTO category)
        {
            CategoryBL bl = new CategoryBL();
            List<CategoryDTO> list = new List<CategoryDTO>();
            bl.SearchListUp(category, out list);
            ViewBag.CaterogyList = list;
            if (ModelState.IsValid)
            {    
                category.updated_by = 0;
                int result=bl.UpdateData(category);
                if (result == 1)
                {
                    TempData["Error"] = "Update badly";
                    TempData["Success"] = "";
                    return View(category);
                }
                else
                {
                    TempData["Error"] = "";
                    TempData["Success"] = "Update successfully";
                    return RedirectToAction("CategorySearch");
                }
            }
            else return View(category);

        }

        //Delete multi category when checked
        [HttpPost]
        public ActionResult CategoryDeleteMulti(FormCollection frm)
        {
            string[] ids = frm["CategoryID"].Split(new char[] { ',' });
            CategoryBL bl = new CategoryBL();
            foreach (string item in ids)
            {
                CategoryDTO categoryDTO = new CategoryDTO();
                int id = item.ParseInt32();
                categoryDTO.id = id;
                List<CategoryDTO> dtos = new List<CategoryDTO>();
                int returncode=bl.SearchData(categoryDTO, out dtos);
                if (returncode == 1)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                else
                {
                    int result = bl.DeleteData(id);
                    if (result == 1)
                    {
                        TempData["Error"] = "Delete badly";
                        TempData["Success"] = "";
                    }
                    else
                    {
                        TempData["Error"] = "";
                        TempData["Success"] = "Insert successfully";
                    }
                }
            }
            return RedirectToAction("CategorySearch");
        }

    }
}