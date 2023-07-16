using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_Project.Models
{
    public class Slide:BaseModel
    {
        public string? Image { get; set; }
        [NotMapped]
        public IFormFile? formFile { get; set; }

    }
}
