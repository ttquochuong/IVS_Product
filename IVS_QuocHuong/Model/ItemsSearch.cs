using DTO.Product;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVS_QuocHuong.Model
{
    public class ItemsSearch
    {
        public int Page { get; set; } = 1;
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public int? Category { get; set; }
        public int CountResult { get; set; }
        public int PageCount { get; set; }
        public StaticPagedList<ItemsDTO> SearchResults { get; set; }
        public string SearchButton { get; set; }
    }

}