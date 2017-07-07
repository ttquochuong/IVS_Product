
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
        public int SearchData(ItemsDTO searchDto, out List<ItemsDTO> dtResult)
        {
            int returnCode = ItemsDAO.SearchData(searchDto as ItemsDTO, out dtResult);
            return returnCode;
        }

        public int SearchID(int? id, out ItemsDTO dtResult)
        {
            int returnCode = ItemsDAO.SearchID(id, out dtResult);
            return returnCode;
        }

        public int InsertData(ItemsDTO inputData)
        {
            int returnCode = ItemsDAO.InsertData(inputData);
            return returnCode;
        }

        public int UpdateData(ItemsDTO inputData)
        {
            int returnCode = ItemsDAO.UpdateData(inputData);
            return returnCode;
        }

        public int DeleteData(int? id)
        {
            int returnCode = ItemsDAO.DeleteData(id);
            return returnCode;
        }

        public int CountPage(ItemsDTO searchDto)
        {
            int returnCode = ItemsDAO.CountPage(searchDto as ItemsDTO);
            return returnCode;
        }

        public int CheckCode(ItemsDTO searchDto)
        {
            int returnCode = ItemsDAO.CheckCode(searchDto as ItemsDTO);
            return returnCode;
        }

    }
}
