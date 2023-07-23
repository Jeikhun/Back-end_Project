using System.ComponentModel.DataAnnotations;

namespace Back_end_Project.ViewModels
{
    public class AccountLoginVM
    {
        [Required]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
