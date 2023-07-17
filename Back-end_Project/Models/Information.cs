using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_Project.Models
{
    public class Information:BaseModel
    {
        public string? Title { get; set; }
        public string? Image { get; set; }
        public string? Text { get; set; }
        public string? Link { get; set; }

        [NotMapped]
        public IFormFile? FormFile { get; set; }
    }
}
