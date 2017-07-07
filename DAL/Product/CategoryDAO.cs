using Core;
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
        public static int SearchData(CategoryDTO inputData, out List<CategoryDTO> dt)
        {
            dt = new List<CategoryDTO>();
            DataTable tb = new DataTable();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    string sql = "SELECT child.`id`, child.`code`, child.`name`,child.`parent_id`, child.`description`, parent.`name` AS parent_name FROM product_category child JOIN product_category parent ON child.`parent_id`=parent.`id` WHERE TRUE ";
                    if (inputData.id.HasValue)
                    {
                        sql += " AND child.`id` = @ID ";
                        cmd.Parameters.AddWithValue("@ID", inputData.id);
                    }
                    if (inputData.code.IsNotNullOrEmpty())
                    {
                        sql += " AND child.`code` = @code ";
                        cmd.Parameters.AddWithValue("@code", inputData.code);
                    }
                    if (inputData.name.IsNotNullOrEmpty())
                    {
                        sql += " AND child.`name` = @name ";
                        cmd.Parameters.AddWithValue("@name", inputData.name);
                    }

                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        sda.Fill(tb);
                        if (tb.Rows.Count == 0)
                        {
                            returnCode = 1;
                        }
                        if(tb.Rows.Count > 0) { 
                            foreach (DataRow item in tb.Rows)
                            {
                                CategoryDTO dto = new CategoryDTO();
                                dto.id = int.Parse(item["id"].ToString());
                                dto.code = item["code"].ToString();
                                dto.name = item["name"].ToString();       
                                if (item["description"].ToString().IsNotNullOrEmpty())
                                {
                                    dto.description = item["description"].ToString();
                                }
                                if (item["parent_name"].ToString().IsNotNullOrEmpty())
                                {
                                    dto.parent_name = item["parent_name"].ToString();
                                }
                                if (item["parent_id"].ToString().IsNotNullOrEmpty())
                                {
                                    dto.parent_id = item["parent_id"].ToString().ParseInt32();
                                }
                                dt.Add(dto);
                            }
                        }
                    }
                }
            }

            return returnCode;
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

        public static int InsertData(CategoryDTO dto)
        {
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection connect = new MySqlConnection(constr))
            {
                using (MySqlCommand command = new MySqlCommand(
                    @"INSERT INTO product_category(`parent_id`,`code`, `name`, `description`
                    , `created_datetime`, `created_by`,`updated_datetime`,`updated_by`
                    ) VALUES (@parent_id, @code, @name, @description
                    , SYSDATE(), @created_by, SYSDATE(), @updated_by)", connect))
                {
                    command.Parameters.AddWithValue("@parent_id", dto.parent_id);
                    command.Parameters.AddWithValue("@code", dto.code);
                    command.Parameters.AddWithValue("@name", dto.name);
                    command.Parameters.AddWithValue("@description", dto.description);
                    command.Parameters.AddWithValue("@created_by", dto.created_by);
                    command.Parameters.AddWithValue("@updated_by", dto.updated_by);
                    connect.Open();
                    command.ExecuteNonQuery();
                }


            }
            return returnCode;
        }

        public static int UpdateData(CategoryDTO dto)
        {
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection connect = new MySqlConnection(constr))
            {
                using (MySqlCommand command = new MySqlCommand(
                    @"UPDATE product_category SET `parent_id`=@parent_id,`name`=@name, `description`=@description
                    ,`updated_datetime`=SYSDATE(),`updated_by`=@updated_by WHERE `id`=@id", connect))
                {
                    command.Parameters.AddWithValue("@id", dto.id);
                    command.Parameters.AddWithValue("@parent_id", dto.parent_id);
                    command.Parameters.AddWithValue("@name", dto.name);
                    command.Parameters.AddWithValue("@description", dto.description);
                    command.Parameters.AddWithValue("@updated_by", dto.updated_by);
                    connect.Open();
                    command.ExecuteNonQuery();
                }


            }
            return returnCode;
        }

        public static int DeleteData(int? id)
        {
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            try
            {
                using (MySqlConnection connect = new MySqlConnection(constr))
                {
                    using (MySqlCommand command = new MySqlCommand(
                        @"DELETE FROM  `product_category` WHERE `id`=@ID", connect))
                    {
                        command.Parameters.AddWithValue("@ID", id);
                        connect.Open();
                        command.ExecuteNonQuery();

                    }


                }
               
            }
            catch (Exception ex)
            {
                throw ex;
                returnCode= 1;
            }
            return returnCode;
        }
    }
}
