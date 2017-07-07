using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Product
{
    public class MeasureDTO
    {
        [Key]
        public int? id { get; set; }

        [Display(Name = "Code")]
    
        public string code { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Description")]
        public string description { get; set; }

        public int? created_by { get; set; }

        public int? updated_by { get; set; }

        public int? page { get; set; }
    }
}
