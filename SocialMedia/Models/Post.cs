using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class Post
    {
        [Key] public int Id { get; set; }
        [Required] public string? Image { get; set; }
        [Required] public string? Caption { get; set; }
        [Required] public string? PosterId { get; set; }
        [Required] public string? PosterUsername { get; set; }
        [Required] public string? PosterDisplayName { get; set; }
        [Required] public string? ReceiverId { get; set; }
        [Required] public int MatchingId { get; set; }

        public Post(string image, string caption, string poster, string receiver, string username, string displayName, int userID)
        {
            Image = image;
            Caption = caption;
            PosterId = poster;
            ReceiverId = receiver;
            PosterUsername = displayName;
            PosterDisplayName = displayName;
            MatchingId = userID;
        }

        public Post()
        {

        }
    }
}
