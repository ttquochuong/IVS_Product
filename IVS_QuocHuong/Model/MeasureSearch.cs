using DTO.Product;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVS_QuocHuong.Model
{
    public class MeasureSearch
    {
        public int? Page { get; set; } = 1;
        public string MeasureName { get; set; }
        public string MeasureCode { get; set; }
        public int PageCount { get; set}
        public IPagedList<MeasureDTO> SearchResults { get; set; }
        public string SearchButton { get; set; }
    }
}