using Microsoft.EntityFrameworkCore;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.DataAccess.Context;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.DataAccess.Concrete
{
    public class BlogPostRepository : GenericRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(HarborDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BlogPost>> GetActivePostsAsync()
        {
            return await _dbSet
                .Where(p => p.IsActive && p.PublishedDate <= DateTime.Now)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetFeaturedPostsAsync()
        {
            return await _dbSet
                .Where(p => p.IsFeatured && p.IsActive && p.PublishedDate <= DateTime.Now)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetPostsByCategoryAsync(int categoryId)
        {
            return await _dbSet
                .Where(p => p.CategoryId == categoryId && p.IsActive && p.PublishedDate <= DateTime.Now)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetPostsWithCategoryAsync()
        {
            return await _dbSet
                .Include(p => p.Category)
                .Where(p => p.IsActive && p.PublishedDate <= DateTime.Now)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetAllPostsWithCategoryAsync()
        {
            return await _dbSet
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetRecentPostsAsync(int count = 5)
        {
            return await _dbSet
                .Where(p => p.IsActive && p.PublishedDate <= DateTime.Now)
                .OrderByDescending(p => p.CreatedDate)
                .Take(count)
                .ToListAsync();
        }

        public async Task IncrementViewCountAsync(int postId)
        {
            var post = await _dbSet.FindAsync(postId);
            if (post != null)
            {
                post.ViewCount++;
                _dbSet.Update(post);
            }
        }
    }
}
