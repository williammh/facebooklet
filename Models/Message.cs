using System.ComponentModel.DataAnnotations;

namespace facebooklet.Models
{
    public class Message : BaseEntity
    {
        [Required]
        [MinLength(1)]
        [Display(Name = "What's on your mind?")]
        public string text { get; set; }
    }
}