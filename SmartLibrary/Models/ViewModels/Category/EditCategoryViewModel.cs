using System.ComponentModel.DataAnnotations;

namespace SmartLibrary.Models.ViewModels.Category
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tên thể loại")]
        public string Name { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }
    }

}