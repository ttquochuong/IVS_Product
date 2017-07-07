using DTO.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Product
{
    public class ItemsDTO
    {
        [Key]
        public int? id { get; set; }

        [Required(ErrorMessage = "Category must be fill in")]
        [Display(Name = "Category")]
        public int? category_id { get; set; }

        public string category_name { get; set; }

        [Required(ErrorMessage = "Code must be fill in")]
        [Display(Name = "Code")]
        public string code { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Specification")]
        public string specification { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Dangerous")]
        public bool dangerous { get; set; }

        [Display(Name = "Discontinued datetime")]
        public string discontinued_datetime { get; set; }


        [Display(Name = "Inventory measure")]
        public int? inventory_measure_id { get; set; }

        [Display(Name = "Inventory expired")]
        public int? inventory_expired { get; set; }

        public double? inventory_standard_cost { get; set; }

        [Display(Name = "Inventory list price")]
        [Range(-99999999999999.9999,9999999999999.9999, ErrorMessage = "Price must be between $1 and $100")]
        public decimal inventory_list_price { get; set; }

        [Display(Name = "Manufacture day")]
        public double? manufacture_day { get; set; }

        [Display(Name = "Manufacture make")]
        public bool manufacture_make { get; set; }

        [Display(Name = "Manufacture tool")]
        public int? manufacture_tool { get; set; }

        [Display(Name = "Manufacture finished goods")]
        public bool manufacture_finished_goods { get; set; }

        [Display(Name = "Manufacture size")]
        public string manufacture_size { get; set; }

        [Display(Name = "Manufacture size")]
        public int? manufacture_size_measure_id { get; set; }

        [Display(Name = "Manufacture weight")]
        public string manufacture_weight { get; set; }

        [Display(Name = "Manufacture weight")]
        public int? manufacture_weight_measure_id { get; set; }
 
        public string manufacture_style { get; set; }
  
        public string manufacture_class { get; set; }
  
        public string manufacture_color { get; set; }

        public int? created_by { get; set; }

        public int? updated_by { get; set; }

        public int page { get; set; } = 1;
    }
}
