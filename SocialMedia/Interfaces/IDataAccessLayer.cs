using SocialMedia.Models;

namespace SocialMedia.Interfaces
{
    public interface IDataAccessLayer<T> where T : class
    {
        public IEnumerable<T> GetAll();

        public T Get(int? id);

        public void Add(T entity);

        public void Remove(int? id);

        public void Update(T entity);

        public IEnumerable<T> Search(string filters);
    }
}
