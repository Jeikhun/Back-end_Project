using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_Project.Models
{
    public class Course:BaseModel
    {

        public string? Image { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        [NotMapped]
        public IFormFile? FormFile { get; set; }

    }
}
