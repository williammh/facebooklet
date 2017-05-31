using System.ComponentModel.DataAnnotations;

namespace facebooklet.Models
{
    public abstract class BaseEntity 
    {
    }
    public class User : BaseEntity
    {
        [Required]
        [MinLength(2)]
        public string firstname { get; set; }
        [Required]
        [MinLength(2)]
        public string lastname { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [MinLength(8)]
        public string password { get; set; }
        [Compare("password")]
        public string confirmpassword { get; set; }
    }
}