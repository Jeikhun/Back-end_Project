using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_Project.Models
{
    public class Person:BaseModel
    {
        public string? Image { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public string? Name { get; set; }
        public string? Position { get; set; }
        [NotMapped]
        public IFormFile? FormFile { get; set; }

    }
}
