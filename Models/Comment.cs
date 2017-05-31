using System.ComponentModel.DataAnnotations;

namespace facebooklet.Models
{
    public class Comment : BaseEntity
    {
        [Required]
        public int id { get; set; }
        [Required]
        public int user_id { get; set; }
        [Required]
        public int message_id { get; set; }
        [Required]
        [MinLength(1)]
        public string text { get; set; }
    }
}