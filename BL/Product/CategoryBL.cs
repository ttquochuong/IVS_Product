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
        public int SearchData(CategoryDTO searchDto, out List<CategoryDTO> dtResult)
        {
            int returnCode = CategoryDAO.SearchData(searchDto as CategoryDTO, out dtResult);
            return returnCode;
        }

        public int SearchList(out DataTable dtResult)
        {
            int returnCode = CategoryDAO.SearchList(out dtResult);
            return returnCode;
        }

        public int InsertData(CategoryDTO inputData)
        {
            int returnCode = CategoryDAO.InsertData(inputData);
            return returnCode;
        }

        public int UpdateData(CategoryDTO inputData)
        {
            int returnCode = CategoryDAO.UpdateData(inputData);
            return returnCode;
        }

        public int DeleteData(int? id)
        {
            int returnCode = CategoryDAO.DeleteData(id);
            return returnCode;
        }

        public int CheckCode(CategoryDTO dto)
        {
            int returnCode = CategoryDAO.CheckCode(dto);
            return returnCode;
        }

        public int CountPage(CategoryDTO dto)
        {
            int returnCode = CategoryDAO.CountPage(dto);
            return returnCode;
        }
    }
}
