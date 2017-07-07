using Core;
using IVS_QuocHuong.Models;
using System.Data;
using System.Web.Mvc;
using BL.Product;
using DTO.Product;

namespace IVS_QuocHuong.Controllers
{
    public class ItemsController : Controller
    {
        // GET: Items
        [HttpGet]
        public ActionResult ItemSearch(ItemsDTO item)
        {
            ItemBL bl = new ItemBL();
            ItemsList model = new ItemsList();
            DataTable dt;
            bl.SearchData(item, out dt);
            model.Items = CommonMethod.DataTableToList<ItemsDTO>(dt);   
            return View(model);
        }

        [HttpGet]
        public ActionResult ItemAdd()
        {
            CategoryBL catelogy = new CategoryBL();
            MeasureBL measure = new MeasureBL();
            DataTable catelogydt;
            DataTable measuredt;
            catelogy.SearchList(out catelogydt);
            measure.SearchList(out measuredt);
            ViewBag.CatelogyList = CommonMethod.DataTableToList<CategoryDTO>(catelogydt);
            ViewBag.MeasureList= CommonMethod.DataTableToList<MeasureDTO>(measuredt);
            ItemsDTO model = new ItemsDTO();
            return View(model);
        }

        [HttpPost]
        public ActionResult ItemAdd(ItemsDTO input)
        {
            ItemAdd model = new ItemAdd();
            return View(model);
        }
    }
}