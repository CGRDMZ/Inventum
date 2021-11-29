using System;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class BoardRepository : IBoardRepository
    {

        private ApplicationDbContext _context;

        public BoardRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Board> AddAsync(Board entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var board = await _context.Boards.FirstAsync(b => b.BoardId == id);

            _context.Boards.Remove(board);

            await _context.SaveChangesAsync();
        }

        public async Task<Board> FindByIdAsync(Guid id)
        {
            return await _context.Boards.Include(b => b.Owner).Include(b => b.CardGroups).ThenInclude(cg => cg.Cards).FirstAsync(b => b.BoardId == id);
        }

        public async Task UpdateAsync(Board entity)
        {
            _context.Boards.Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}