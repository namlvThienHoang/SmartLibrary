using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.ViewModels.Author
{
    public class AuthorViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tên tác giả")]
        public string AuthorName { get; set; }

        [Required]
        [Display(Name = "Ngày sinh")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Tiểu sử")]
        public string Biography { get; set; }
    }
}