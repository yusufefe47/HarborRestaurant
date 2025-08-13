using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Abstract
{
    public interface IAboutService
    {
        Task<About?> GetActiveAboutAsync();
        Task<About?> GetByIdAsync(int id);
        Task<IEnumerable<About>> GetAllAsync();
        Task AddAsync(About about);
        Task<About> CreateAsync(About about);
        Task UpdateAsync(About about);
        Task DeleteAsync(int id);
    }
}
