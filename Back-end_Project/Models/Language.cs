using System.ComponentModel.DataAnnotations;

namespace Back_end_Project.Models
{
    public class Language:BaseModel
    {
        [Required]
        public string? LanguageName { get; set; }
        public List<Course>? Courses { get; set; }



    }
}