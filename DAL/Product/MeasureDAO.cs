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
    public class MeasureDAO
    {
        public static int SearchData(MeasureDTO inputData, out List<MeasureDTO> dt)
        {
            dt = new List<MeasureDTO>();
            DataTable tb = new DataTable();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    string sql = "SELECT `id`,`code`, `name`,`description` FROM product_measure WHERE TRUE ";
                    if (inputData.id.HasValue)
                    {
                        sql += " AND `id` = @ID ";
                        cmd.Parameters.AddWithValue("@ID", inputData.id);
                    }
                    if (inputData.name.IsNotNullOrEmpty())
                    {
                        sql += " AND `name` LIKE CONCAT('%',@name,'%')";
                        cmd.Parameters.AddWithValue("@name", inputData.name);
                    }
                    if (inputData.code.IsNotNullOrEmpty())
                    {
                       
                        sql += " AND `code` LIKE CONCAT('%',@code,'%')";
                        cmd.Parameters.AddWithValue("@code", inputData.code);
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
                        if (tb.Rows.Count > 0)
                        {
                            foreach (DataRow item in tb.Rows)
                            {
                                MeasureDTO dto = new MeasureDTO();
                                dto.id = int.Parse(item["id"].ToString());
                                dto.code = item["code"].ToString();
                                dto.name = item["name"].ToString();
                                if (item["description"].ToString().IsNotNullOrEmpty())
                                {
                                    dto.description = item["description"].ToString();
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
                using (MySqlCommand cmd = new MySqlCommand("SELECT ct.ID, ct.Name, ct.Code FROM product_measure ct WHERE TRUE ", con))
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
        public static int InsertData(MeasureDTO dto)
        {
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection connect = new MySqlConnection(constr))
            {
                using (MySqlCommand command = new MySqlCommand(
                    @"INSERT INTO product_measure(`code`, `name`, `description`
                    , `created_datetime`, `created_by`,`updated_datetime`,`updated_by`
                    ) VALUES ( @code, @name, @description
                    , SYSDATE(), @created_by, SYSDATE(), @updated_by)", connect))
                {
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

        public static int UpdateData(MeasureDTO dto)
        {
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection connect = new MySqlConnection(constr))
            {
                using (MySqlCommand command = new MySqlCommand(
                    @"UPDATE product_measure SET `name`=@name, `description`=@description
                    ,`updated_datetime`=SYSDATE(),`updated_by`=@updated_by WHERE `id`=@id", connect))
                {
                    command.Parameters.AddWithValue("@id", dto.id);
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
            using (MySqlConnection connect = new MySqlConnection(constr))
            {
                using (MySqlCommand command = new MySqlCommand(
                    @"DELETE FROM  `product_measure` WHERE `id`=@ID", connect))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    connect.Open();
                    command.ExecuteNonQuery();
                }


            }
            return returnCode;
        }


    }
}
