using Microsoft.EntityFrameworkCore;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.DataAccess.Context;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.DataAccess.Concrete
{
    public class MenuItemRepository : GenericRepository<MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(HarborDbContext context) : base(context)
        {
        }

        // Async methods
        public async Task<IEnumerable<MenuItem>> GetActiveItemsAsync()
        {
            return await _dbSet
                .Where(i => i.IsActive)
                .OrderBy(i => i.SortOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetItemsByCategoryAsync(int categoryId)
        {
            return await _dbSet
                .Where(i => i.CategoryId == categoryId && i.IsActive)
                .OrderBy(i => i.SortOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetSpecialItemsAsync()
        {
            return await _dbSet
                .Where(i => i.IsSpecial && i.IsActive)
                .OrderBy(i => i.SortOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetItemsWithCategoryAsync()
        {
            return await _dbSet
                .Include(i => i.Category)
                .Where(i => i.IsActive)
                .OrderBy(i => i.Category!.SortOrder)
                .ThenBy(i => i.SortOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItemsByCategoryAsync(int categoryId)
        {
            return await GetItemsByCategoryAsync(categoryId);
        }

        public async Task<IEnumerable<MenuItem>> GetAvailableMenuItemsAsync()
        {
            return await GetActiveItemsAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetAvailableMenuItemsByCategoryAsync(int categoryId)
        {
            return await GetItemsByCategoryAsync(categoryId);
        }

        public async Task<IEnumerable<MenuItem>> GetAllWithCategoryAsync()
        {
            return await _dbSet
                .Include(i => i.Category)
                .OrderBy(i => i.Category!.SortOrder)
                .ThenBy(i => i.SortOrder)
                .ToListAsync();
        }

        // Sync methods
        public MenuItem? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<MenuItem> GetAll()
        {
            return _dbSet.OrderBy(i => i.SortOrder).ToList();
        }

        public IEnumerable<MenuItem> GetActiveItems()
        {
            return _dbSet
                .Where(i => i.IsActive)
                .OrderBy(i => i.SortOrder)
                .ToList();
        }

        public IEnumerable<MenuItem> GetItemsByCategory(int categoryId)
        {
            return _dbSet
                .Where(i => i.CategoryId == categoryId && i.IsActive)
                .OrderBy(i => i.SortOrder)
                .ToList();
        }

        public IEnumerable<MenuItem> GetSpecialItems()
        {
            return _dbSet
                .Where(i => i.IsSpecial && i.IsActive)
                .OrderBy(i => i.SortOrder)
                .ToList();
        }

        public IEnumerable<MenuItem> GetItemsWithCategory()
        {
            return _dbSet
                .Include(i => i.Category)
                .Where(i => i.IsActive)
                .OrderBy(i => i.Category!.SortOrder)
                .ThenBy(i => i.SortOrder)
                .ToList();
        }

        public IEnumerable<MenuItem> GetMenuItemsByCategory(int categoryId)
        {
            return GetItemsByCategory(categoryId);
        }

        public IEnumerable<MenuItem> GetAvailableMenuItems()
        {
            return GetActiveItems();
        }

        public IEnumerable<MenuItem> GetAvailableMenuItemsByCategory(int categoryId)
        {
            return GetItemsByCategory(categoryId);
        }

        public IEnumerable<MenuItem> GetAllWithCategory()
        {
            return _dbSet
                .Include(i => i.Category)
                .OrderBy(i => i.Category!.SortOrder)
                .ThenBy(i => i.SortOrder)
                .ToList();
        }

        public void Add(MenuItem entity)
        {
            entity.CreatedDate = DateTime.Now;
            _dbSet.Add(entity);
        }

        public void Delete(MenuItem entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
