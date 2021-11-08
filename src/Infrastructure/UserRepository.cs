using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<User> AddAsync(User entity)
        {
            await _context.Users.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity;
        }


        public async Task DeleteAsync(Guid id)
        {
            var user = _context.Users.Where(u => u.UserId == id).First();
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> FindByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);

            await _context.SaveChangesAsync();

        }
    }
}