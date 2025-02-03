using CoffeeBeansApi.DbContext;
using CoffeeBeansApi.Interfaces;
using CoffeeBeansApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeBeansApi.RepositoryPatterns;

public class AllBeansRepository : IAllBeansRepository
{
    private readonly BeansDbContext _context;
    private static Beans? _previousBeanOfTheDay;

    public AllBeansRepository(BeansDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Beans>> GetAllAsync() => await _context.Beans.ToListAsync();

    public async Task<IEnumerable<Beans>> SearchAsync(string query)
    {
        return await _context.Beans
            .Where(b => b.Name.Contains(query) || b.Origin.Contains(query) || b.RoastLevel.Contains(query) || b.Colour.Contains(query))
            .ToListAsync();
    }

    public async Task<Beans> GetByIdAsync(int id) => await _context.Beans.FindAsync(id);

    public async Task AddAsync(Beans bean)
    {
        _context.Beans.Add(bean);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Beans bean)
    {
        _context.Beans.Update(bean);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var bean = await _context.Beans.FindAsync(id);
        if (bean != null)
        {
            _context.Beans.Remove(bean);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Beans> GetBeanOfTheDayAsync()
    {
        var beans = await _context.Beans.ToListAsync();
        if (!beans.Any()) return null;

        var yesterday = DateTime.UtcNow.Date.AddDays(-1);
        var previousSelection = await _context.BeanSelectionHistory
            .OrderByDescending(h => h.SelectedDate)
            .FirstOrDefaultAsync(h => h.SelectedDate == yesterday);

        var availableBeans = beans.Where(b => previousSelection == null || b.Id != previousSelection.BeanId).ToList();
        if (!availableBeans.Any()) return null;

        var random = new Random();
        var beanOfTheDay = availableBeans[random.Next(availableBeans.Count)];

        _context.BeanSelectionHistory.Add(new BeanSelectionHistory { BeanId = beanOfTheDay.Id, SelectedDate = DateTime.UtcNow.Date });
        await _context.SaveChangesAsync();

        return beanOfTheDay;
    }
}

