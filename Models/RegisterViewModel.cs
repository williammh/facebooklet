using System.ComponentModel.DataAnnotations;

namespace facebooklet.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name can only contain letters")]
        [Display(Name = "First Name")]
        public string firstname { get; set; }
 
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last Name can only contain letters")]
        [Display(Name = "Last Name")]
        public string lastname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$", ErrorMessage = "Invalid E-Mail")]
        [Display(Name = "E-Mail Address")]
        public string email { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Password and confirmation must match.")]
        [Display(Name = "Confirm Password")]
        public string confirmpassword { get; set; }
    }
}