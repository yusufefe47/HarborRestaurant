using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Abstract
{
    public interface IMenuService
    {
        // Menu Categories Async
        Task<MenuCategory?> GetCategoryByIdAsync(int id);
        Task<MenuCategory?> GetMenuCategoryByIdAsync(int id);
        Task<IEnumerable<MenuCategory>> GetAllCategoriesAsync();
        Task<IEnumerable<MenuCategory>> GetAllMenuCategoriesAsync();
        Task<IEnumerable<MenuCategory>> GetActiveCategoriesAsync();
        Task<IEnumerable<MenuCategory>> GetCategoriesWithMenuItemsAsync();
        Task AddCategoryAsync(MenuCategory category);
        Task AddMenuCategoryAsync(MenuCategory category);
        Task UpdateCategoryAsync(MenuCategory category);
        Task UpdateMenuCategoryAsync(MenuCategory category);
        Task DeleteCategoryAsync(int id);
        Task DeleteMenuCategoryAsync(int id);

        // Menu Items Async
        Task<MenuItem?> GetItemByIdAsync(int id);
        Task<MenuItem?> GetMenuItemByIdAsync(int id);
        Task<IEnumerable<MenuItem>> GetAllItemsAsync();
        Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync();
        Task<IEnumerable<MenuItem>> GetAllMenuItemsWithCategoryAsync();
        Task<IEnumerable<MenuItem>> GetActiveItemsAsync();
        Task<IEnumerable<MenuItem>> GetItemsByCategoryAsync(int categoryId);
        Task<IEnumerable<MenuItem>> GetSpecialItemsAsync();
        Task<IEnumerable<MenuItem>> GetItemsWithCategoryAsync();
        Task AddItemAsync(MenuItem item);
        Task AddMenuItemAsync(MenuItem item);
        Task UpdateItemAsync(MenuItem item);
        Task UpdateMenuItemAsync(MenuItem item);
        Task DeleteItemAsync(int id);
        Task DeleteMenuItemAsync(int id);

        // Menu Categories Sync
        MenuCategory? GetCategoryById(int id);
        IEnumerable<MenuCategory> GetAllCategories();
        IEnumerable<MenuCategory> GetActiveCategories();
        IEnumerable<MenuCategory> GetCategoriesWithMenuItems();
        void AddCategory(MenuCategory category);
        void UpdateCategory(MenuCategory category);
        void DeleteCategory(int id);

        // Menu Items Sync
        MenuItem? GetItemById(int id);
        IEnumerable<MenuItem> GetAllItems();
        IEnumerable<MenuItem> GetActiveItems();
        IEnumerable<MenuItem> GetItemsByCategory(int categoryId);
        IEnumerable<MenuItem> GetSpecialItems();
        IEnumerable<MenuItem> GetItemsWithCategory();
        void AddItem(MenuItem item);
        void UpdateItem(MenuItem item);
        void DeleteItem(int id);
    }
}
