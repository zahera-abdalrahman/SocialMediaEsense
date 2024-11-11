using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Data;
using System.Security.Claims;

namespace SocialMedia.Generic
{
    public class Generic<T> : IGeneric<T> where T : class
    {
        private readonly SocialMediaContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public Generic(SocialMediaContext _context, IHttpContextAccessor _httpContextAccessor)
        {
            context = _context;
            httpContextAccessor = _httpContextAccessor;
        }
       

        public async Task Add(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var entity = await context.Set<T>().FindAsync(Id);
            if (entity == null)
            {
                throw new Exception("Entity not found");
            }

            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
        }


        public async Task Update(T entity)
        {
            context.Set<T>().Attach(entity);

            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }




    }
}
