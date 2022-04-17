using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class CardRepository : ICardRepository
{


    private ApplicationDbContext _context;

    public CardRepository(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<Card> AddAsync(Card entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Card> FindByIdAsync(Guid id)
    {
        return await _context.Cards
                            .Include(c => c.CheckListComponents.Where(c => !c.IsDeleted))
                            .ThenInclude(c => c.CheckListItems.Where(i => !i.IsDeleted).OrderBy(i => i.Position))
                            .FirstAsync(c => c.CardId == id);
    }

    public async Task UpdateAsync(Card entity)
    {
        _context.Cards.Update(entity);

        await _context.SaveChangesAsync();
    }
}