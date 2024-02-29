using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Models
{
    public class Profile
    {
        [Key] public int Id { get; set; }
        [Required] public string DisplayName { get; set; }
        [Required] public string Username { get; set; }
        [Required] public string Picture { get; set; }
        [Required] public int Age { get; set; }
        [Required] public int CatsOwned { get; set; }
        [Required] public string? FavoriteCatBreed { get; set; }

        public Profile(string displayname, string picture, int age, int castowned, string favCatBreed)
        {
            this.DisplayName = displayname;
            this.Picture = picture;
            this.Age = age;
            CatsOwned = castowned;
            FavoriteCatBreed = favCatBreed;
        }
        public Profile() { }

    }
}
