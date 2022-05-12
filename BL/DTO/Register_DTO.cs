using System.ComponentModel.DataAnnotations;

namespace BL.DTO
{
    public class Register_DTO
    {
        [Required (ErrorMessage = "This field is required.")]
        public string User_Name { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }

    }
}