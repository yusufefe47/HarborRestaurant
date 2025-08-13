using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.DataAccess.Abstract
{
    public interface IBlogPostRepository : IGenericRepository<BlogPost>
    {
        Task<IEnumerable<BlogPost>> GetActivePostsAsync();
        Task<IEnumerable<BlogPost>> GetFeaturedPostsAsync();
        Task<IEnumerable<BlogPost>> GetPostsByCategoryAsync(int categoryId);
        Task<IEnumerable<BlogPost>> GetPostsWithCategoryAsync();
    Task<IEnumerable<BlogPost>> GetAllPostsWithCategoryAsync();
        Task<IEnumerable<BlogPost>> GetRecentPostsAsync(int count = 5);
        Task IncrementViewCountAsync(int postId);
    }
}
