using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.DataAccess.Abstract
{
    public interface IMenuItemRepository : IGenericRepository<MenuItem>
    {
        // Async methods
        Task<IEnumerable<MenuItem>> GetActiveItemsAsync();
        Task<IEnumerable<MenuItem>> GetItemsByCategoryAsync(int categoryId);
        Task<IEnumerable<MenuItem>> GetSpecialItemsAsync();
        Task<IEnumerable<MenuItem>> GetItemsWithCategoryAsync();
        Task<IEnumerable<MenuItem>> GetMenuItemsByCategoryAsync(int categoryId);
        Task<IEnumerable<MenuItem>> GetAvailableMenuItemsAsync();
        Task<IEnumerable<MenuItem>> GetAvailableMenuItemsByCategoryAsync(int categoryId);
        Task<IEnumerable<MenuItem>> GetAllWithCategoryAsync();
        
        // Sync methods
        MenuItem? GetById(int id);
        IEnumerable<MenuItem> GetAll();
        IEnumerable<MenuItem> GetActiveItems();
        IEnumerable<MenuItem> GetItemsByCategory(int categoryId);
        IEnumerable<MenuItem> GetSpecialItems();
        IEnumerable<MenuItem> GetItemsWithCategory();
        IEnumerable<MenuItem> GetMenuItemsByCategory(int categoryId);
        IEnumerable<MenuItem> GetAvailableMenuItems();
        IEnumerable<MenuItem> GetAvailableMenuItemsByCategory(int categoryId);
        IEnumerable<MenuItem> GetAllWithCategory();
        void Add(MenuItem entity);
        void Delete(MenuItem entity);
    }
}
