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

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Category must be fill in")]
        public int? category_id { get; set; }

   
        public string category_name { get; set; }

        [Required(ErrorMessage = "Code must be fill in")]
        [Display(Name = "Code")]
        [StringLength(16, MinimumLength = 0, ErrorMessage = "Code cannot be longer than 16 characters")]
        public string code { get; set; }

        [Display(Name = "Name")]
        [StringLength(16, MinimumLength = 0, ErrorMessage = "Name cannot be longer than 16 characters")]
        public string name { get; set; }

        [Display(Name = "Specification")]
        [StringLength(256, MinimumLength = 0, ErrorMessage = "Specification cannot be longer than 256 characters")]
        public string specification { get; set; } 

        [Display(Name = "Description")]
        [StringLength(256, MinimumLength = 0, ErrorMessage = "Description cannot be longer than 256 characters")]
        public string description { get; set; }

        [Display(Name = "Dangerous")]
        public bool dangerous { get; set; }

        [Display(Name = "Discontinued datetime")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string discontinued_datetime { get; set; }


        [Display(Name = "Inventory measure")]
        public int? inventory_measure_id { get; set; }

        [Display(Name = "Inventory expired")]
        [RegularExpression(@"^\d{0,11}", ErrorMessage = "Inventory expired must be number")]
        public int? inventory_expired { get; set; }

        [Display(Name = "inventory standard cost")]
        [RegularExpression(@"^\d{0,14}(\.{1})?\d{1,4}", ErrorMessage = "Invalid format. Ex:4444.333, 3333, 333.2222,....")]
        public decimal? inventory_standard_cost { get; set; }

        [Display(Name = "Inventory list price")]
        [RegularExpression(@"^\d{0,14}(\.{1})?\d{1,4}", ErrorMessage = "Invalid format. Ex:4444.333, 3333, 333.2222,....")]
        public decimal? inventory_list_price { get; set; }

        [Display(Name = "Manufacture day")]
        [RegularExpression(@"^\d{0,9}((\.{1})?\d{1,2})", ErrorMessage = "Invalid format. Ex:4444.3, 3333, 333.22,....")]
        [Range(-999999999.9999, 999999999.9999, ErrorMessage = "Valid too range")]
        public decimal? manufacture_day { get; set; }

        [Display(Name = "Manufacture make")]
        public bool manufacture_make { get; set; }

        [Display(Name = "Manufacture tool")]
        public bool manufacture_tool { get; set; }

        [Display(Name = "Manufacture finished goods")]
        public bool manufacture_finished_goods { get; set; }

        [Display(Name = "Manufacture size")]
        [StringLength(16, MinimumLength = 0, ErrorMessage = "Manufacture size cannot be longer than 16 characters")]
        public string manufacture_size { get; set; }

        [Display(Name = "Manufacture size")]
        public int? manufacture_size_measure_id { get; set; }

        [Display(Name = "Manufacture weight")]
        [StringLength(16, MinimumLength = 0, ErrorMessage = "Manufacture weight cannot be longer than 16 characters")]
        public string manufacture_weight { get; set; }

        [Display(Name = "Manufacture weight")]
        public int? manufacture_weight_measure_id { get; set; }

        [Display(Name = "Manufacture style")]
        [StringLength(16, MinimumLength = 0, ErrorMessage = "Manufacture style cannot be longer than 16 characters")]
        public string manufacture_style { get; set; }

        [Display(Name = "Manufacture class")]
        [StringLength(16, MinimumLength = 0, ErrorMessage = "Manufacture class cannot be longer than 16 characters")]
        public string manufacture_class { get; set; }

        [Display(Name = "Manufacture color")]
        [StringLength(16, MinimumLength = 0, ErrorMessage = "Manufacture color cannot be longer than 16 characters")]
        public string manufacture_color { get; set; }

        public int? created_by { get; set; }

        public int? updated_by { get; set; }

        public int page { get; set; } = 1;
    }
}
