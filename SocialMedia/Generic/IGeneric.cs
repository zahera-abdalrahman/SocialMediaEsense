using SocialMedia.Data;

namespace SocialMedia.Generic
{
    public interface IGeneric<T> where T : class
    {
        Task Add(T entity);
        //Task Delete(int Id);

        //Task Update(T entity);

        //Task<List<T>> LoadById(string Id);
    }
}