using DTO.Product;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVS_QuocHuong.Model
{
    public class CategorySearch
    {
        public int? Page { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryParent { get; set; }
        public IPagedList<CategoryDTO> SearchResults { get; set; }
        public string SearchButton { get; set; }
        public int PageCount { get; set; }
    }
}