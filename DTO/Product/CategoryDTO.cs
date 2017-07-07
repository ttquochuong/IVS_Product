using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Product
{
    public class CategoryDTO
    {
        [Key]
        public int? id { get; set; }

        [Required]
        [Display(Name = "Parent")]
        public int parent_id { get; set; }

        [Required(ErrorMessage = "Code must be fill in")]
        [StringLength(16, ErrorMessage = "Max length is 16")]
        [Display(Name = "Code")]
        public string code { get; set; }

        [Required(ErrorMessage = "Name must be fill in")]
        [StringLength(64, ErrorMessage = "Max length is 64")]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Parent Name")]
        public string parent_name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        public int? created_by { get; set; }

        public int? updated_by { get; set; }

        public int page { get; set; } = 1;

    }
}
