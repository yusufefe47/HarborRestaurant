using Microsoft.EntityFrameworkCore;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.DataAccess.Context;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.DataAccess.Concrete
{
    public class BlogCategoryRepository : GenericRepository<BlogCategory>, IBlogCategoryRepository
    {
        public BlogCategoryRepository(HarborDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BlogCategory>> GetActiveCategoriesAsync()
        {
            return await _dbSet
                .Where(c => c.IsActive)
                .OrderBy(c => c.SortOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<BlogCategory>> GetCategoriesWithPostsAsync()
        {
            return await _dbSet
                .Include(c => c.BlogPosts.Where(bp => bp.IsActive))
                .Where(c => c.IsActive)
                .OrderBy(c => c.SortOrder)
                .ToListAsync();
        }
    }
}
