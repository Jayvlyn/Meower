using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class Message
    {
        [Key] public int Id { get; set; }
        [Required] public string? Content { get; set; }
        [Required] public Profile? Sender { get; set; }
    }
}
