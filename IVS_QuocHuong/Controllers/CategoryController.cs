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
            DataTable catelogydt;
            catelogy.SearchList(out catelogydt);
            ViewBag.CaterogyList = CommonMethod.DataTableToList<CategoryDTO>(catelogydt);
            return View();
        }

        [HttpPost]
        public ActionResult CategoryAdd(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                CategoryBL bl = new CategoryBL();
                category.created_by = 0;
                category.updated_by = 0;
                bl.InsertData(category);
                return RedirectToAction("CategoryAdd");
            }
            return View(category);
        }

        [HttpGet]
        public ActionResult CategoryDelete(int id)
        {
            CategoryBL bl = new CategoryBL();
            CategoryDTO categoryDTO = new CategoryDTO();
            List<CategoryDTO> dtos = new List<CategoryDTO>();
            categoryDTO.id = id;
            int equal = bl.SearchData(categoryDTO, out dtos);

            if (equal == 1)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                bl.DeleteData(id);
            }

            return RedirectToAction("CategorySearch");
        }

        [HttpGet]
        public ActionResult CategoryUpdate(int id)
        {
            CategoryBL bl = new CategoryBL();
            CategoryDTO category = new CategoryDTO();
            CategoryBL catelogy = new CategoryBL();
            DataTable catelogydt;
            catelogy.SearchList(out catelogydt);
            ViewBag.CaterogyList = CommonMethod.DataTableToList<CategoryDTO>(catelogydt);
            category.id = id;
            List<CategoryDTO> dtos = new List<CategoryDTO>();
            bl.SearchData(category, out dtos);
            CategoryDTO model=new CategoryDTO();
            model= dtos[0];
            return View(model);
        }
        [HttpPost]
        public ActionResult CategoryUpdate(CategoryDTO category)
        {
            CategoryBL bl = new CategoryBL();
            category.updated_by = 0;
            bl.UpdateData(category);
            return RedirectToAction("CategorySearch");
        }

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
                    bl.DeleteData(id);
                }
            }
            return RedirectToAction("CategorySearch");
        }

    }
}