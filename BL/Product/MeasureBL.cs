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
        public int SearchData(MeasureDTO searchDto, out List<MeasureDTO> dtResult)
        {
            int returnCode = MeasureDAO.SearchData(searchDto as MeasureDTO, out dtResult);
            return returnCode;
        }

        public int SearchList(out List<MeasureDTO> dtResult)
        {
            int returnCode = MeasureDAO.SearchList(out dtResult);
            return returnCode;
        }

        public int InsertData(MeasureDTO inputData)
        {
            int returnCode = MeasureDAO.InsertData(inputData);
            return returnCode;
        }

        public int UpdateData(MeasureDTO inputData)
        {
            int returnCode = MeasureDAO.UpdateData(inputData);
            return returnCode;
        }

        public int DeleteData(int? id)
        {
            int returnCode = MeasureDAO.DeleteData(id);
            return returnCode;
        }

        public int CheckCode(MeasureDTO dto)
        {
            int returnCode = MeasureDAO.CheckCode(dto);
            return returnCode;
        }

        public int PageCount(MeasureDTO dto)
        {
            int returnCode = MeasureDAO.CountPage(dto);
            return returnCode;
        }


    }
}
