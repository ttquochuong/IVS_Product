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
    public class CategoryController : Controller
    {
        // GET: Caterogy
        public ActionResult CategorySearch(CategoryDTO caterogy)
        {
            CategoryBL bl = new CategoryBL();
            CategoryList model = new CategoryList();
            DataTable dt;
            bl.SearchData(caterogy, out dt);
            model.Caterogy = CommonMethod.DataTableToList<CategoryDTO>(dt);
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
            CategoryBL bl = new CategoryBL();
            category.created_by = 0;
            category.updated_by = 0;
            bl.InsertData(category);
            return RedirectToAction("CategoryAdd");
        }

        [HttpGet]
        public ActionResult CategoryDelete(int id)
        {
            CategoryBL bl = new CategoryBL();
            DataTable dtResult;
            CategoryDTO categoryDTO = new CategoryDTO();
            categoryDTO.id = id;
            bl.SearchData(categoryDTO, out dtResult);
            int count = dtResult.Rows.Count;
            if (count == 0)
            {
                Response.StatusCode = 404;
                return null;
            }
            bl.DeleteData(id);
            return RedirectToAction("CategorySearch");
        }

        [HttpGet]
        public ActionResult CategoryUpdate(int id)
        {
            CategoryBL bl = new CategoryBL();
            DataTable dt;
            CategoryDTO category = new CategoryDTO();

            CategoryBL catelogy = new CategoryBL();
            DataTable catelogydt;
            catelogy.SearchList(out catelogydt);
            ViewBag.CaterogyList = CommonMethod.DataTableToList<CategoryDTO>(catelogydt);
            category.id = id;
            bl.SearchData(category, out dt);
            DataRow row = dt.Rows[0];
            category.parent_id = int.Parse(row["parent_id"].ToString());
            category.code = row["code"].ToString();
            category.name = row["name"].ToString();
            category.description = row["description"].ToString();
            return View(category);

        }
        [HttpPost]
        public ActionResult CategoryUpdate(CategoryDTO category)
        {
            CategoryBL bl = new CategoryBL();
            category.created_by = 0;
            category.updated_by = 0;
            bl.UpdateData(category);
            return RedirectToAction("CategorySearch");
        }

    }
}