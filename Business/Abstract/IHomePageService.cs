using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Abstract
{
    public interface IHomePageService
    {
        Task<HomePage?> GetActiveHomePageAsync();
        Task<HomePage?> GetByIdAsync(int id);
        Task<IEnumerable<HomePage>> GetAllAsync();
        Task AddAsync(HomePage homePage);
        Task<HomePage> CreateAsync(HomePage homePage);
        Task UpdateAsync(HomePage homePage);
        Task DeleteAsync(int id);
    }
}
