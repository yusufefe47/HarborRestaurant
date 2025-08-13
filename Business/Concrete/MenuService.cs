using HarborRestaurant.Business.Abstract;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Concrete
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Menu Categories Async

        public async Task<MenuCategory?> GetCategoryByIdAsync(int id)
        {
            return await _unitOfWork.MenuCategories.GetByIdAsync(id);
        }

        public async Task<MenuCategory?> GetMenuCategoryByIdAsync(int id)
        {
            return await _unitOfWork.MenuCategories.GetByIdAsync(id);
        }

        public async Task<IEnumerable<MenuCategory>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.MenuCategories.GetAllAsync();
            return categories.OrderBy(c => c.SortOrder);
        }

        public async Task<IEnumerable<MenuCategory>> GetAllMenuCategoriesAsync()
        {
            var categories = await _unitOfWork.MenuCategories.GetAllAsync();
            return categories.OrderBy(c => c.SortOrder);
        }

        public async Task<IEnumerable<MenuCategory>> GetActiveCategoriesAsync()
        {
            return await _unitOfWork.MenuCategories.GetActiveCategoriesAsync();
        }

        public async Task<IEnumerable<MenuCategory>> GetCategoriesWithMenuItemsAsync()
        {
            return await _unitOfWork.MenuCategories.GetCategoriesWithMenuItemsAsync();
        }

        public async Task AddCategoryAsync(MenuCategory category)
        {
            category.CreatedDate = DateTime.Now;
            await _unitOfWork.MenuCategories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddMenuCategoryAsync(MenuCategory category)
        {
            category.CreatedDate = DateTime.Now;
            await _unitOfWork.MenuCategories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(MenuCategory category)
        {
            category.UpdatedDate = DateTime.Now;
            _unitOfWork.MenuCategories.Update(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateMenuCategoryAsync(MenuCategory category)
        {
            category.UpdatedDate = DateTime.Now;
            _unitOfWork.MenuCategories.Update(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.MenuCategories.GetByIdAsync(id);
            if (category != null)
            {
                _unitOfWork.MenuCategories.Delete(category);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteMenuCategoryAsync(int id)
        {
            var category = await _unitOfWork.MenuCategories.GetByIdAsync(id);
            if (category != null)
            {
                _unitOfWork.MenuCategories.Delete(category);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        #endregion

        #region Menu Items Async

        public async Task<MenuItem?> GetItemByIdAsync(int id)
        {
            return await _unitOfWork.MenuItems.GetByIdAsync(id);
        }

        public async Task<MenuItem?> GetMenuItemByIdAsync(int id)
        {
            return await _unitOfWork.MenuItems.GetByIdAsync(id);
        }

        public async Task<IEnumerable<MenuItem>> GetAllItemsAsync()
        {
            var items = await _unitOfWork.MenuItems.GetAllAsync();
            return items.OrderBy(i => i.SortOrder);
        }

        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsAsync()
        {
            var items = await _unitOfWork.MenuItems.GetAllAsync();
            return items.OrderBy(i => i.SortOrder);
        }

        public async Task<IEnumerable<MenuItem>> GetAllMenuItemsWithCategoryAsync()
        {
            return await _unitOfWork.MenuItems.GetItemsWithCategoryAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetActiveItemsAsync()
        {
            return await _unitOfWork.MenuItems.GetActiveItemsAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetItemsByCategoryAsync(int categoryId)
        {
            return await _unitOfWork.MenuItems.GetItemsByCategoryAsync(categoryId);
        }

        public async Task<IEnumerable<MenuItem>> GetSpecialItemsAsync()
        {
            return await _unitOfWork.MenuItems.GetSpecialItemsAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetItemsWithCategoryAsync()
        {
            return await _unitOfWork.MenuItems.GetItemsWithCategoryAsync();
        }

        public async Task AddItemAsync(MenuItem item)
        {
            item.CreatedDate = DateTime.Now;
            await _unitOfWork.MenuItems.AddAsync(item);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddMenuItemAsync(MenuItem item)
        {
            item.CreatedDate = DateTime.Now;
            await _unitOfWork.MenuItems.AddAsync(item);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(MenuItem item)
        {
            item.UpdatedDate = DateTime.Now;
            _unitOfWork.MenuItems.Update(item);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateMenuItemAsync(MenuItem item)
        {
            item.UpdatedDate = DateTime.Now;
            _unitOfWork.MenuItems.Update(item);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            var item = await _unitOfWork.MenuItems.GetByIdAsync(id);
            if (item != null)
            {
                _unitOfWork.MenuItems.Delete(item);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteMenuItemAsync(int id)
        {
            var item = await _unitOfWork.MenuItems.GetByIdAsync(id);
            if (item != null)
            {
                _unitOfWork.MenuItems.Delete(item);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        #endregion

        #region Menu Categories Sync

        public MenuCategory? GetCategoryById(int id)
        {
            return _unitOfWork.MenuCategories.GetById(id);
        }

        public IEnumerable<MenuCategory> GetAllCategories()
        {
            var categories = _unitOfWork.MenuCategories.GetAll();
            return categories.OrderBy(c => c.SortOrder);
        }

        public IEnumerable<MenuCategory> GetActiveCategories()
        {
            return _unitOfWork.MenuCategories.GetActiveCategories();
        }

        public IEnumerable<MenuCategory> GetCategoriesWithMenuItems()
        {
            return _unitOfWork.MenuCategories.GetCategoriesWithMenuItems();
        }

        public void AddCategory(MenuCategory category)
        {
            category.CreatedDate = DateTime.Now;
            _unitOfWork.MenuCategories.Add(category);
            _unitOfWork.SaveChanges();
        }

        public void UpdateCategory(MenuCategory category)
        {
            category.UpdatedDate = DateTime.Now;
            _unitOfWork.MenuCategories.Update(category);
            _unitOfWork.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = _unitOfWork.MenuCategories.GetById(id);
            if (category != null)
            {
                _unitOfWork.MenuCategories.Delete(category);
                _unitOfWork.SaveChanges();
            }
        }

        #endregion

        #region Menu Items Sync

        public MenuItem? GetItemById(int id)
        {
            return _unitOfWork.MenuItems.GetById(id);
        }

        public IEnumerable<MenuItem> GetAllItems()
        {
            var items = _unitOfWork.MenuItems.GetAll();
            return items.OrderBy(i => i.SortOrder);
        }

        public IEnumerable<MenuItem> GetActiveItems()
        {
            return _unitOfWork.MenuItems.GetActiveItems();
        }

        public IEnumerable<MenuItem> GetItemsByCategory(int categoryId)
        {
            return _unitOfWork.MenuItems.GetItemsByCategory(categoryId);
        }

        public IEnumerable<MenuItem> GetSpecialItems()
        {
            return _unitOfWork.MenuItems.GetSpecialItems();
        }

        public IEnumerable<MenuItem> GetItemsWithCategory()
        {
            return _unitOfWork.MenuItems.GetItemsWithCategory();
        }

        public void AddItem(MenuItem item)
        {
            item.CreatedDate = DateTime.Now;
            _unitOfWork.MenuItems.Add(item);
            _unitOfWork.SaveChanges();
        }

        public void UpdateItem(MenuItem item)
        {
            item.UpdatedDate = DateTime.Now;
            _unitOfWork.MenuItems.Update(item);
            _unitOfWork.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var item = _unitOfWork.MenuItems.GetById(id);
            if (item != null)
            {
                _unitOfWork.MenuItems.Delete(item);
                _unitOfWork.SaveChanges();
            }
        }

        #endregion
    }
}
