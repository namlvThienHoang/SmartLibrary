using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.ViewModels.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tên thể loại")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }
    }

}