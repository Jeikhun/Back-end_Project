using System.ComponentModel.DataAnnotations;


namespace Back_end_Project.ViewModels
{
    public class AccountRegisterVM
    {

        [Required, MaxLength(30), DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required,MaxLength(30)]
        public string? Username { get; set; }

        [Required, MaxLength(30), DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required, MaxLength(30),DataType(DataType.Password), Display(Name ="Confirm Password"), Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }
    }
}
