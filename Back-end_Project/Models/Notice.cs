using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Back_end_Project.Models
{
    
    public class Notice:BaseModel
    {
        [Required]
        public string? Link { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
