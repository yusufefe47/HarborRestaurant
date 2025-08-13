using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.DataAccess.Abstract
{
    public interface IMenuCategoryRepository : IGenericRepository<MenuCategory>
    {
        // Async methods
        Task<IEnumerable<MenuCategory>> GetActiveCategoriesAsync();
        Task<IEnumerable<MenuCategory>> GetCategoriesWithMenuItemsAsync();
        Task<IEnumerable<MenuCategory>> GetActiveMenuCategoriesAsync();
        
        // Sync methods
        MenuCategory? GetById(int id);
        IEnumerable<MenuCategory> GetAll();
        IEnumerable<MenuCategory> GetActiveCategories();
        IEnumerable<MenuCategory> GetCategoriesWithMenuItems();
        IEnumerable<MenuCategory> GetActiveMenuCategories();
        void Add(MenuCategory entity);
        void Delete(MenuCategory entity);
    }
}
