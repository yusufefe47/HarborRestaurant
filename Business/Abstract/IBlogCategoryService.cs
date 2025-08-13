using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Abstract
{
    public interface IBlogCategoryService
    {
        Task<List<BlogCategory>> GetAllAsync();
        Task<List<BlogCategory>> GetActiveAsync();
        Task<BlogCategory?> GetByIdAsync(int id);
        Task<BlogCategory> CreateAsync(BlogCategory blogCategory);
        Task<BlogCategory> UpdateAsync(BlogCategory blogCategory);
        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleActiveAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> NameExistsAsync(string name, int? excludeId = null);
    }
}
