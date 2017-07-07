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

        [Required]
        public int? category_id { get; set; }

        [Required]
        [Display(Name ="Item's Code")]
        public string code { get; set; }

        [Required]
        [Display(Name = "Item's Name")]
        public string name { get; set; }

        [Display(Name = "Specification")]
        public string specification { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        [Display(Name = "Dangerous")]
        public bool dangerous { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Discontinued Datetime ")]
        public DateTime? discontinued_datetime { get; set; }

        [Display(Name = "Inventory measure")]
        public int? inventory_measure_id { get; set; }

        [Display(Name = "Inventory expired")]
        public int? inventory_expired { get; set; }

        [Display(Name = "Inventory standard cost")]
        public double? inventory_standard_cost { get; set; }

        [Display(Name = "Inventory list price")]
        public double? inventory_list_price { get; set; }

        [Display(Name = "Manufacture day")]
        public double? manufacture_day { get; set; }

        [Display(Name = "manufacture_make")]
        public int? manufacture_make { get; set; }

        [Display(Name = "Manufacture tool")]
        public int? manufacture_tool { get; set; }

        [Display(Name = "Manufacture finished goods")]
        public int? manufacture_finished_goods { get; set; }

        [Display(Name = "inventory size")]
        public string manufacture_size { get; set; }

        [Display(Name = "Manufacture size")]
        public int? manufacture_size_measure_id { get; set; }

        [Display(Name = "Manufacture weight")]
        public string manufacture_weight { get; set; }

        [Display(Name = "manufacture weight")]
        public int? manufacture_weight_measure_id { get; set; }

        [Display(Name = "Manufacture style")]
        public string manufacture_style { get; set; }

        [Display(Name = "Manufacture class")]
        public string manufacture_class { get; set; }

        [Display(Name = "Manufacture color")]
        public string manufacture_color { get; set; }

        public List<CategoryDTO> category { get; set; }

        public List<MeasureDTO> measure { get; set; }
    }
}
