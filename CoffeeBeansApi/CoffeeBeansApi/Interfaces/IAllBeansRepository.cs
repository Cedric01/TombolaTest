using CoffeeBeansApi.Models;

namespace CoffeeBeansApi.Interfaces;

public interface IAllBeansRepository
{
    Task<IEnumerable<Beans>> GetAllAsync();
    Task<IEnumerable<Beans>> SearchAsync(string query);
    Task<Beans> GetByIdAsync(int id);
    Task AddAsync(Beans bean);
    Task UpdateAsync(Beans bean);
    Task DeleteAsync(int id);
    Task<Beans> GetBeanOfTheDayAsync();
}
