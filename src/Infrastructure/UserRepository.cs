using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;


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
            return await _context.Users
            .Include(u => u.Invitations)
                .ThenInclude(i => i.InvitedUser)
            .Include(u => u.Invitations)
                .ThenInclude(i => i.InvitedTo)
            .Include(u => u.Boards)
            .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> FindUserByUsername(string username)
        {
            return await _context.Users.Include(u => u.Invitations).Include(u => u.Boards).
            FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task UpdateAsync(User entity)
        {
            _context.Users.Update(entity);

            await _context.SaveChangesAsync();

        }


    }
}