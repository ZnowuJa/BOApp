using Domain.Entities.ITWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.ITWarehouse.Repositories;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class CategoryTypeRepository : ICustomRepository<CategoryType>
{
    private readonly IAppDbContext _context;
    public CategoryTypeRepository(IAppDbContext appDbContext)
    {
        _context = appDbContext;

    }

    public async Task<CategoryType> CreateAsync(CategoryType t)
    {
        _context.CategoryTypes.Add(t);
        await _context.SaveChangesAsync();
        return t;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var t = _context.CategoryTypes.FirstOrDefault(t => t.Id == id);
        if (t != null)
        {
            _context.CategoryTypes.Remove(t);
            await _context.SaveChangesAsync();
        }
        else
        {
            return 0;
        }

        return id;
        
    }

    public async Task<ICollection<CategoryType>> GetAllAsync()
    {
        return await _context.CategoryTypes.ToListAsync();
    }

    public async Task<CategoryType> GetByIdAsync(int id)
    {
        return await _context.CategoryTypes.FirstOrDefaultAsync(t => t.Id == id);
    }

    public Task<CategoryType> UpdateAsync(int id, CategoryType t)
    {
        throw new NotImplementedException();
    }
}
