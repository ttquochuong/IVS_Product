using Core;
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
        public static int SearchData(ItemsDTO inputData, out List<ItemsDTO> dt)
        {
            dt = new List<ItemsDTO>();
            DataTable tb = new DataTable();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    try
                    {
                        string sql = @"SELECT item.`id`, item.`code`, item.`name`,item.`Dangerous`, item.`specification`,item.`description`,item.`name` ,cate.`name` as category, item.`discontinued_datetime`
                      FROM `product_item` item JOIN `product_category` cate ON cate.`ID`= item.`category_id` WHERE TRUE ";
                        if (inputData.id.HasValue)
                        {
                            sql += " AND item.`id` = @ID ";
                            cmd.Parameters.AddWithValue("@ID", inputData.id);
                        }
                        if (inputData.code.IsNotNullOrEmpty())
                        {
                            sql += " AND item.`code` LIKE CONCAT('%',@code,'%') ";
                            cmd.Parameters.AddWithValue("@code", inputData.code);
                        }
                        if (inputData.name.IsNotNullOrEmpty())
                        {
                            sql += " AND item.`name` LIKE CONCAT('%',@name,'%') ";
                            cmd.Parameters.AddWithValue("@name", inputData.name);


                        }
                        if (inputData.category_id.HasValue)
                        {

                            sql += " AND item.`category_id` = @category ";
                            cmd.Parameters.AddWithValue("@category", inputData.category_id);

                        }


                        sql += " ORDER BY item.`discontinued_datetime` ";

                        sql += " LIMIT  @start, 20 ";
                        cmd.Parameters.AddWithValue("@start", 20 * (inputData.page - 1));
                        con.Open();
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
                            else
                            {
                                foreach (DataRow item in tb.Rows)
                                {
                                    ItemsDTO dto = new ItemsDTO();
                                    dto.id = int.Parse(item["id"].ToString());
                                    dto.code = item["code"].ToString();
                                    dto.name = item["name"].ToString();
                                    dto.specification = item["specification"].ToString();
                                    dto.description = item["description"].ToString();
                                    dto.discontinued_datetime = item["discontinued_datetime"].ToString();
                                    dto.dangerous = item["dangerous"].ToString().ParseBool();
                                    dto.category_name = item["category"].ToString();
                                    dt.Add(dto);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        returnCode = 1;
                    }
                }

                return returnCode;
            }
        }

        public static int SearchID(int? id, out ItemsDTO dt)
        {
            dt = new ItemsDTO();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            DataTable tb = new DataTable();
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    string sql = @"SELECT `id`,`code`, `name`, `specification`, `description`, `category_id`, `discontinued_datetime`
                    , `dangerous`, `inventory_measure_id`, `inventory_expired`, `inventory_standard_cost`
                    , `inventory_list_price`, `manufacture_day`,`manufacture_make`, `manufacture_tool`
                    , `manufacture_finished_goods`, `manufacture_size`, `manufacture_size_measure_id`
                    , `manufacture_weight`, `manufacture_weight_measure_id`, `manufacture_style`
                    , `manufacture_class`, `manufacture_color` FROM product_item WHERE TRUE";
                    if (id.HasValue)
                    {
                        sql += " AND `id` = @ID ";
                        cmd.Parameters.AddWithValue("@ID", id);
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
                        else
                        {
                            DataRow row = tb.Rows[0];
                            dt.id = row["id"].ToString().ParseInt32();
                            dt.code = row["code"].ToString();
                            dt.name = row["name"].ToString();
                            if (row["inventory_list_price"].ToString().IsNotNullOrEmpty())
                            {
                                dt.inventory_list_price = (Decimal.Parse(row["inventory_list_price"].ToString()));
                            }
                            dt.dangerous = bool.Parse(row["dangerous"].ToString());
                            dt.manufacture_style = row["manufacture_style"].ToString();
                            dt.manufacture_weight = row["manufacture_weight"].ToString();
                            if (row["manufacture_weight_measure_id"].ToString().IsNotNullOrEmpty())
                            {
                                dt.manufacture_weight_measure_id = row["manufacture_weight_measure_id"].ToString().ParseInt32();
                            }
                            dt.specification = row["specification"].ToString();
                            dt.description = row["description"].ToString();
                            dt.discontinued_datetime = row["discontinued_datetime"].ParseDateTime().ToString();
                            if (row["inventory_expired"].ToString().IsNotNullOrEmpty())
                            {
                                dt.inventory_expired = row["inventory_expired"].ToString().ParseInt32();
                            }
                            if (row["inventory_measure_id"].ToString().IsNotNullOrEmpty())
                            {
                                dt.inventory_measure_id = row["inventory_measure_id"].ToString().ParseInt32();
                            }
                            if (row["inventory_standard_cost"].ToString().IsNotNullOrEmpty())
                            {
                                dt.inventory_standard_cost = row["inventory_standard_cost"].ToString().ParseDouble();
                            }
                            dt.manufacture_class = row["manufacture_class"].ToString();
                            dt.manufacture_color = row["manufacture_color"].ToString();
                            if (row["manufacture_day"].ToString().IsNotNullOrEmpty())
                            {
                                dt.manufacture_day =Decimal.Parse(row["manufacture_day"].ToString());
                            }
                            if (row["manufacture_finished_goods"].ToString().IsNotNullOrEmpty())
                            {
                                dt.manufacture_finished_goods = bool.Parse(row["manufacture_finished_goods"].ToString());
                            }
                            if (row["manufacture_make"].ToString().IsNotNullOrEmpty())
                            {
                                dt.manufacture_make = bool.Parse(row["manufacture_make"].ToString());
                            }
                            dt.manufacture_size = row["manufacture_size"].ToString();
                            if (row["manufacture_size_measure_id"].ToString().IsNotNullOrEmpty())
                            {
                                dt.manufacture_size_measure_id = row["manufacture_size_measure_id"].ToString().ParseInt32();
                            }
                        }
                    }
                }
            }

            return returnCode;
        }

        public static int InsertData(ItemsDTO dto)
        {
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (MySqlConnection connect = new MySqlConnection(constr))
            {
                try
                {
                    using (MySqlCommand command = new MySqlCommand(
                        @"INSERT INTO product_item(
                    `code`, `name`, `specification`, `description`, `category_id`, `discontinued_datetime`
                    , `dangerous`, `inventory_measure_id`, `inventory_expired`, `inventory_standard_cost`
                    , `inventory_list_price`, `manufacture_day`,`manufacture_make`, `manufacture_tool`
                    , `manufacture_finished_goods`, `manufacture_size`, `manufacture_size_measure_id`
                    , `manufacture_weight`, `manufacture_weight_measure_id`, `manufacture_style`
                    , `manufacture_class`, `manufacture_color`
                    , `created_datetime`, `created_by`,`updated_datetime`,`updated_by`
                    ) VALUES (
                    @code, @name, @specification, @description, @category_id,@discontinued_datetime
                    , @dangerous, @inventory_measure_id, @inventory_expired, @inventory_standard_cost
                    , @inventory_list_price, @manufacture_day, @manufacture_make, @manufacture_tool
                    , @manufacture_finished_goods, @manufacture_size, @manufacture_size_measure_id
                    , @manufacture_weight, @manufacture_weight_measure_id, @manufacture_style
                    , @manufacture_class, @manufacture_color
                    , SYSDATE(), @created_by, SYSDATE(), @updated_by)", connect))
                    {
                        command.Parameters.AddWithValue("@code", dto.code);
                        command.Parameters.AddWithValue("@name", dto.name);
                        command.Parameters.AddWithValue("@specification", dto.specification);
                        command.Parameters.AddWithValue("@description", dto.description);
                        command.Parameters.AddWithValue("@category_id", dto.category_id);
                        command.Parameters.AddWithValue("@discontinued_datetime", dto.discontinued_datetime);
                        command.Parameters.AddWithValue("@dangerous", dto.dangerous);
                        command.Parameters.AddWithValue("@inventory_measure_id", dto.inventory_measure_id);
                        command.Parameters.AddWithValue("@inventory_expired", dto.inventory_expired);
                        command.Parameters.AddWithValue("@inventory_standard_cost", dto.inventory_standard_cost);
                        command.Parameters.AddWithValue("@inventory_list_price", dto.inventory_list_price);
                        command.Parameters.AddWithValue("@manufacture_day", dto.manufacture_day);
                        command.Parameters.AddWithValue("@manufacture_make", dto.manufacture_make);
                        command.Parameters.AddWithValue("@manufacture_tool", dto.manufacture_tool);
                        command.Parameters.AddWithValue("@manufacture_finished_goods", dto.manufacture_finished_goods);
                        command.Parameters.AddWithValue("@manufacture_size", dto.manufacture_size);
                        command.Parameters.AddWithValue("@manufacture_size_measure_id", dto.manufacture_size_measure_id);
                        command.Parameters.AddWithValue("@manufacture_weight", dto.manufacture_weight);
                        command.Parameters.AddWithValue("@manufacture_weight_measure_id", dto.manufacture_weight_measure_id);
                        command.Parameters.AddWithValue("@manufacture_style", dto.manufacture_style);
                        command.Parameters.AddWithValue("@manufacture_class", dto.manufacture_class);
                        command.Parameters.AddWithValue("@manufacture_color", dto.manufacture_color);
                        command.Parameters.AddWithValue("@created_by", dto.created_by);
                        command.Parameters.AddWithValue("@updated_by", dto.updated_by);
                        connect.Open();
                        command.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    returnCode = 1;
                }
            }
            return returnCode;
        }

        public static int UpdateData(ItemsDTO dto)
        {
            int returnCode = 0;

            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (MySqlConnection connect = new MySqlConnection(constr))
            {
                {
                    using (MySqlCommand command = new MySqlCommand(
                        @"Update product_item Set
                    `name` = @name, `specification` = @specification, `description` = @description,
                    `category_id` = @category_id, `discontinued_datetime` = @discontinued_datetime,
                    `dangerous` = @dangerous, `inventory_measure_id` = @inventory_measure_id, 
                    `inventory_expired` = @inventory_expired, `inventory_standard_cost` = @inventory_standard_cost,
                    `inventory_list_price` = @inventory_list_price, `manufacture_day` = @manufacture_day,
                    `manufacture_make` = @manufacture_make, `manufacture_tool` = @manufacture_tool,
                    `manufacture_finished_goods` = @manufacture_finished_goods, 
                    `manufacture_size` = @manufacture_size, `manufacture_size_measure_id` = @manufacture_size_measure_id,
                    `manufacture_weight` = @manufacture_weight, `manufacture_weight_measure_id` = @manufacture_weight_measure_id,
                    `manufacture_style` = @manufacture_style,
                    `manufacture_class` = @manufacture_class, `manufacture_color` = @manufacture_color,
                    `updated_datetime` = SYSDATE(), `updated_by` = @updated_by Where `id` = @ID", connect))
                    {


                        command.Parameters.AddWithValue("@name", dto.name);
                        command.Parameters.AddWithValue("@specification", dto.specification);
                        command.Parameters.AddWithValue("@description", dto.description);
                        command.Parameters.AddWithValue("@category_id", dto.category_id);
                        command.Parameters.AddWithValue("@discontinued_datetime", dto.discontinued_datetime);
                        command.Parameters.AddWithValue("@dangerous", dto.dangerous);
                        command.Parameters.AddWithValue("@inventory_measure_id", dto.inventory_measure_id);
                        command.Parameters.AddWithValue("@inventory_expired", dto.inventory_expired);
                        command.Parameters.AddWithValue("@inventory_standard_cost", dto.inventory_standard_cost);
                        command.Parameters.AddWithValue("@inventory_list_price", dto.inventory_list_price);
                        command.Parameters.AddWithValue("@manufacture_day", dto.manufacture_day);
                        command.Parameters.AddWithValue("@manufacture_make", dto.manufacture_make);
                        command.Parameters.AddWithValue("@manufacture_tool", dto.manufacture_tool);
                        command.Parameters.AddWithValue("@manufacture_finished_goods", dto.manufacture_finished_goods);
                        command.Parameters.AddWithValue("@manufacture_size", dto.manufacture_size);
                        command.Parameters.AddWithValue("@manufacture_size_measure_id", dto.manufacture_size_measure_id);
                        command.Parameters.AddWithValue("@manufacture_weight", dto.manufacture_weight);
                        command.Parameters.AddWithValue("@manufacture_weight_measure_id", dto.manufacture_weight_measure_id);
                        command.Parameters.AddWithValue("@manufacture_style", dto.manufacture_style);
                        command.Parameters.AddWithValue("@manufacture_class", dto.manufacture_class);
                        command.Parameters.AddWithValue("@manufacture_color", dto.manufacture_color);
                        command.Parameters.AddWithValue("@updated_by", dto.updated_by);
                        command.Parameters.AddWithValue("@ID", dto.id);
                        connect.Open();
                        command.ExecuteNonQuery();

                    }
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
                    @"DELETE FROM product_item WHERE `id`=@ID", connect))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    connect.Open();
                    command.ExecuteNonQuery();
                }


            }
            return returnCode;
        }


        public static int CountPage(ItemsDTO inputData)
        {

            DataTable tb = new DataTable();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    string sql = @"SELECT Count(*) FROM product_item WHERE TRUE";
                    if (inputData.id.HasValue)
                    {
                        sql += " AND `id` = @ID ";
                        cmd.Parameters.AddWithValue("@ID", inputData.id);
                    }
                    if (inputData.code.IsNotNullOrEmpty())
                    {
                        sql += " AND `code` LIKE CONCAT('%',@code,'%') ";
                        cmd.Parameters.AddWithValue("@code", inputData.code);
                    }
                    if (inputData.name.IsNotNullOrEmpty())
                    {
                        sql += " AND `name` LIKE CONCAT('%',@name,'%') ";
                        cmd.Parameters.AddWithValue("@name", inputData.name);


                    }
                    if (inputData.category_id.HasValue)
                    {
                        sql += " AND `category_id` = @category ";
                        cmd.Parameters.AddWithValue("@category", inputData.category_id);
                    }
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    returnCode = int.Parse(cmd.ExecuteScalar().ToString());


                }
            }
            return returnCode;

        }

        public static int CheckCode(ItemsDTO inputData)
        {

            DataTable tb = new DataTable();
            int returnCode = 0;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    string sql = @"SELECT Count(*) FROM product_item WHERE TRUE";
                    if (inputData.code.IsNotNullOrEmpty())
                    {
                        sql += " AND `code` = @code ";
                        cmd.Parameters.AddWithValue("@code", inputData.code);
                    }
                   
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    returnCode = int.Parse(cmd.ExecuteScalar().ToString());


                }
            }
            return returnCode;

        }
    }
}
