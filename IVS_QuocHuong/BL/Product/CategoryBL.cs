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
    public class CategoryBL
    {
        public int SearchData(CategoryDTO searchDto, out DataTable dtResult)
        {
            int returnCode = CategoryDAO.SearchData(searchDto as CategoryDTO, out dtResult);
            return returnCode;
        }

        public int SearchList(out DataTable dtResult)
        {
            int returnCode = CategoryDAO.SearchList(out dtResult);
            return returnCode;
        }


    }
}
