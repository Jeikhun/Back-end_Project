using System.ComponentModel.DataAnnotations;

namespace Back_end_Project.Models
{
    public class Category:BaseModel
    {
        [Required]
        public string? Name { get; set; }
        public List<CourseCategory>? CourseCategories { get; set; }

    }
}
