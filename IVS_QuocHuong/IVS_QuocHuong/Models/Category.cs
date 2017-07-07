using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IVS_QuocHuong.Models
{
    public class Category
    {
        [Key]
        public int id { get; set; }

        [Required]
        public int parent_id { get; set; }

        [Required]
        public string code { get; set; }

        [Required]
        public string name { get; set; }

        public string decription { get; set; }
    }
}