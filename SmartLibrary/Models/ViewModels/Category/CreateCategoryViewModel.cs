﻿using System.ComponentModel.DataAnnotations;

namespace SmartLibrary.Models.ViewModels.Category
{
    public class CreateCategoryViewModel
    {

        [Required]
        [Display(Name = "Tên thể loại")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Hình ảnh")]
        public string CoverImage { get; set; }
    }

}