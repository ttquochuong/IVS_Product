using DTO.Product;

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Product
{
    public class ItemsDAO
    {
        public static int SearchData(ItemsDTO inputData, out DataTable dt)
        {
            dt = new DataTable();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM product_item WHERE TRUE ", con))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                        if (dt.Rows.Count == 0)
                        {
                            returnCode = 1;
                        }
                    }
                }
            }

            return returnCode;
        }
    }
}
