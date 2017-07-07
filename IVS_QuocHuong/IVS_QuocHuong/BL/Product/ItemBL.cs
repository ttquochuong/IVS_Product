
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
    public class ItemBL
    {
        public int SearchData(ItemsDTO searchDto, out DataTable dtResult)
        {
            int returnCode = ItemsDAO.SearchData(searchDto as ItemsDTO, out dtResult);
            return returnCode;
        }

    }
}
