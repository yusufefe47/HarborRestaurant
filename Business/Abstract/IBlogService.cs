using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Abstract
{
    public interface IBlogService
    {
        // Blog Categories Async
        Task<BlogCategory?> GetCategoryByIdAsync(int id);
        Task<IEnumerable<BlogCategory>> GetAllCategoriesAsync();
        Task<IEnumerable<BlogCategory>> GetActiveCategoriesAsync();
        Task<IEnumerable<BlogCategory>> GetCategoriesWithPostsAsync();
        Task AddCategoryAsync(BlogCategory category);
        Task UpdateCategoryAsync(BlogCategory category);
        Task DeleteCategoryAsync(int id);

        // Blog Posts Async
        Task<BlogPost?> GetPostByIdAsync(int id);
        Task<IEnumerable<BlogPost>> GetAllPostsAsync();
        Task<IEnumerable<BlogPost>> GetActivePostsAsync();
        Task<IEnumerable<BlogPost>> GetFeaturedPostsAsync();
        Task<IEnumerable<BlogPost>> GetPostsByCategoryAsync(int categoryId);
        Task<IEnumerable<BlogPost>> GetPostsWithCategoryAsync();
        Task<IEnumerable<BlogPost>> GetRecentPostsAsync(int count = 5);
        Task AddPostAsync(BlogPost post);
        Task UpdatePostAsync(BlogPost post);
        Task DeletePostAsync(int id);
        Task IncrementViewCountAsync(int postId);

        // Blog Categories Sync
        BlogCategory? GetCategoryById(int id);
        IEnumerable<BlogCategory> GetAllCategories();
        IEnumerable<BlogCategory> GetActiveCategories();
        IEnumerable<BlogCategory> GetCategoriesWithPosts();
        void AddCategory(BlogCategory category);
        void UpdateCategory(BlogCategory category);
        void DeleteCategory(int id);

        // Blog Posts Sync
        BlogPost? GetPostById(int id);
        IEnumerable<BlogPost> GetAllPosts();
        IEnumerable<BlogPost> GetActivePosts();
        IEnumerable<BlogPost> GetFeaturedPosts();
        IEnumerable<BlogPost> GetPostsByCategory(int categoryId);
        IEnumerable<BlogPost> GetPostsWithCategory();
        IEnumerable<BlogPost> GetRecentPosts(int count = 5);
        void AddPost(BlogPost post);
        void UpdatePost(BlogPost post);
        void DeletePost(int id);
        void IncrementViewCount(int postId);
    }
}
