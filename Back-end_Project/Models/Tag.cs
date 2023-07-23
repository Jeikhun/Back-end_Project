using System.ComponentModel.DataAnnotations;

namespace Back_end_Project.Models
{
    public class Tag:BaseModel
    {
        [Required]
        public string? Name { get; set; }
        public List<CourseTag>? CourseTag { get; set; }
    }
}