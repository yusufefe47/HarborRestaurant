using Microsoft.EntityFrameworkCore;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.DataAccess.Context;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.DataAccess.Concrete
{
    public class MenuCategoryRepository : GenericRepository<MenuCategory>, IMenuCategoryRepository
    {
        public MenuCategoryRepository(HarborDbContext context) : base(context)
        {
        }

        // Async methods
        public async Task<IEnumerable<MenuCategory>> GetActiveCategoriesAsync()
        {
            return await _dbSet
                .Where(c => c.IsActive)
                .OrderBy(c => c.SortOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuCategory>> GetCategoriesWithMenuItemsAsync()
        {
            return await _dbSet
                .Include(c => c.MenuItems.Where(mi => mi.IsActive))
                .Where(c => c.IsActive)
                .OrderBy(c => c.SortOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuCategory>> GetActiveMenuCategoriesAsync()
        {
            return await GetActiveCategoriesAsync();
        }

        // Sync methods
        public MenuCategory? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<MenuCategory> GetAll()
        {
            return _dbSet.OrderBy(c => c.SortOrder).ToList();
        }

        public IEnumerable<MenuCategory> GetActiveCategories()
        {
            return _dbSet
                .Where(c => c.IsActive)
                .OrderBy(c => c.SortOrder)
                .ToList();
        }

        public IEnumerable<MenuCategory> GetCategoriesWithMenuItems()
        {
            return _dbSet
                .Include(c => c.MenuItems.Where(mi => mi.IsActive))
                .Where(c => c.IsActive)
                .OrderBy(c => c.SortOrder)
                .ToList();
        }

        public IEnumerable<MenuCategory> GetActiveMenuCategories()
        {
            return GetActiveCategories();
        }

        public void Add(MenuCategory entity)
        {
            entity.CreatedDate = DateTime.Now;
            _dbSet.Add(entity);
        }

        public void Delete(MenuCategory entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
