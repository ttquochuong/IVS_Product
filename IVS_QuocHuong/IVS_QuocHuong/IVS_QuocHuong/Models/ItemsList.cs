using DTO.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IVS_QuocHuong.Models
{
    public class ItemsList
    {
       public List<ItemsDTO> Items { get; set; }
    }
}