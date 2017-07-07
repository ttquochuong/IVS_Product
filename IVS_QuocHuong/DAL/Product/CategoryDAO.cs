using DTO.Product;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Product
{
    public class CategoryDAO
    {
        public static int SearchData(CategoryDTO inputData, out DataTable dt)
        {
            dt = new DataTable();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM product_category WHERE TRUE ", con))
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
                return returnCode;
            }
        }
            public static int SearchList(out DataTable dt)
        {
            dt = new DataTable();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT ct.ID, ct.Name, ct.Code FROM product_category ct WHERE TRUE ", con))
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
