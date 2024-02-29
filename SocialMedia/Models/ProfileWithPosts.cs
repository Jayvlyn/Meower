namespace SocialMedia.Models
{
    public class ProfileWithPosts
    {
        public ProfileWithPosts(Profile _pro, IEnumerable<Post> _posts)
        {
            profile = _pro;
            posts = _posts;
        }
        public Profile profile { get; set; }
        public IEnumerable<Post> posts { get; set; }
    }
}
