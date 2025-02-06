using System;
using System.ComponentModel.DataAnnotations;

namespace SmartLibrary.Models.ViewModels.Author
{
    public class CreateAuthorViewModel
    {
        [Required]
        [Display(Name = "Tên tác giả")]
        public string AuthorName { get; set; }

        [Required]
        [Display(Name = "Ngày sinh")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Tiểu sử")]
        public string Biography { get; set; }

        [Display(Name = "Hình ảnh")]
        public string AvatarImage { get; set; }
    }
}