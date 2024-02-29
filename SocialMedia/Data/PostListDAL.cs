using Microsoft.CodeAnalysis.CSharp.Syntax;
using SocialMedia.Interfaces;
using SocialMedia.Models;

namespace SocialMedia.Data
{
    public class PostListDAL : IDataAccessLayer<Post>
    {
        private ApplicationDbContext db;

        public PostListDAL(ApplicationDbContext indb)
        {
            db = indb;
        }

        public void Add(Post entity)
        {
            db.Posts.Add(entity);
            db.SaveChanges();
        }

        public Post Get(int? id)
        {
            return db.Posts.Where(m => m.Id == id).FirstOrDefault();
        }

        public IEnumerable<Post> GetAll()
        {
            return db.Posts;
        }

        public void Remove(int? id)
        {
            Post? foundPost = Get(id);
            if (foundPost != null)
            {
                db.Posts.Remove(foundPost);
                db.SaveChanges();
            }
        }

        public IEnumerable<Post> Search(string filters)
        {
            throw new NotImplementedException();
        }

        public void Update(Post entity)
        {
            db.Posts.Update(entity);
            db.SaveChanges();
        }
    }
}
