using Microsoft.CodeAnalysis.CSharp.Syntax;
using SocialMedia.Interfaces;
using SocialMedia.Models;

namespace SocialMedia.Data
{
    public class ProfileListDAL : IDataAccessLayer<Profile>
    {
        private ApplicationDbContext db;

        public ProfileListDAL(ApplicationDbContext indb)
        {
            db = indb;
        }

        public void Add(Profile entity)
        {
            db.Profiles.Add(entity);
            db.SaveChanges();
        }

        public Profile Get(int? id)
        {
            Profile? prof = db.Profiles.Where(m => m.Id == id).FirstOrDefault();
            return prof;
        }

        public IEnumerable<Profile> GetAll()
        {
            return db.Profiles;
        }

        public void Remove(int? id)
        {
            Profile? foundProfile = Get(id);
            if (foundProfile != null)
            {
                db.Profiles.Remove(foundProfile);
                db.SaveChanges();
            }
        }

		public IEnumerable<Profile> Search(string filters)
		{
            if (filters == null) return GetAll();
            return GetAll().Where(x => x.DisplayName.ToLower().Contains(filters.ToLower()));
		}

		public void Update(Profile entity)
        {
            db.Profiles.Update(entity);
            db.SaveChanges();
        }
    }
}
