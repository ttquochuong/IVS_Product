using DAL.Product;
using DTO.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Product
{
    public class MeasureBL
    {
        public int SearchData(MeasureDTO searchDto, out DataTable dtResult)
        {
            int returnCode = MeasureDAO.SearchData(searchDto as MeasureDTO, out dtResult);
            return returnCode;
        }

        public int SearchList(out DataTable dtResult)
        {
            int returnCode = MeasureDAO.SearchList(out dtResult);
            return returnCode;
        }

    }
}
