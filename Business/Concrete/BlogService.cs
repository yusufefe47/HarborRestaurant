using HarborRestaurant.Business.Abstract;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Concrete
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Blog Categories

        public async Task<BlogCategory?> GetCategoryByIdAsync(int id)
        {
            return await _unitOfWork.BlogCategories.GetByIdAsync(id);
        }

        public async Task<IEnumerable<BlogCategory>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.BlogCategories.GetAllAsync();
            return categories.OrderBy(c => c.SortOrder);
        }

        public async Task<IEnumerable<BlogCategory>> GetActiveCategoriesAsync()
        {
            return await _unitOfWork.BlogCategories.GetActiveCategoriesAsync();
        }

        public async Task<IEnumerable<BlogCategory>> GetCategoriesWithPostsAsync()
        {
            return await _unitOfWork.BlogCategories.GetCategoriesWithPostsAsync();
        }

        public async Task AddCategoryAsync(BlogCategory category)
        {
            category.CreatedDate = DateTime.Now;
            await _unitOfWork.BlogCategories.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(BlogCategory category)
        {
            category.UpdatedDate = DateTime.Now;
            _unitOfWork.BlogCategories.Update(category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.BlogCategories.GetByIdAsync(id);
            if (category != null)
            {
                _unitOfWork.BlogCategories.Remove(category);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        #endregion

        #region Blog Posts

        public async Task<BlogPost?> GetPostByIdAsync(int id)
        {
            return await _unitOfWork.BlogPosts.GetByIdAsync(id);
        }

        public async Task<IEnumerable<BlogPost>> GetAllPostsAsync()
        {
            // Admin tarafında tüm yazıları (pasif/aktif fark etmeksizin) gösterebilmek için
            return await _unitOfWork.BlogPosts.GetAllPostsWithCategoryAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetActivePostsAsync()
        {
            return await _unitOfWork.BlogPosts.GetActivePostsAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetFeaturedPostsAsync()
        {
            return await _unitOfWork.BlogPosts.GetFeaturedPostsAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetPostsByCategoryAsync(int categoryId)
        {
            return await _unitOfWork.BlogPosts.GetPostsByCategoryAsync(categoryId);
        }

        public async Task<IEnumerable<BlogPost>> GetPostsWithCategoryAsync()
        {
            return await _unitOfWork.BlogPosts.GetPostsWithCategoryAsync();
        }

        public async Task<IEnumerable<BlogPost>> GetRecentPostsAsync(int count = 5)
        {
            return await _unitOfWork.BlogPosts.GetRecentPostsAsync(count);
        }

        public async Task AddPostAsync(BlogPost post)
        {
            post.CreatedDate = DateTime.Now;
            if (!post.PublishedDate.HasValue)
            {
                post.PublishedDate = DateTime.Now;
            }
            
            await _unitOfWork.BlogPosts.AddAsync(post);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(BlogPost post)
        {
            post.UpdatedDate = DateTime.Now;
            _unitOfWork.BlogPosts.Update(post);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await _unitOfWork.BlogPosts.GetByIdAsync(id);
            if (post != null)
            {
                _unitOfWork.BlogPosts.Remove(post);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task IncrementViewCountAsync(int postId)
        {
            await _unitOfWork.BlogPosts.IncrementViewCountAsync(postId);
            await _unitOfWork.SaveChangesAsync();
        }

        #endregion

        #region Sync Methods for Dashboard

        public IEnumerable<BlogPost> GetAllPosts()
        {
            return GetAllPostsAsync().GetAwaiter().GetResult();
        }

        public IEnumerable<BlogPost> GetFeaturedPosts()
        {
            return GetFeaturedPostsAsync().GetAwaiter().GetResult();
        }

        public BlogCategory? GetCategoryById(int id)
        {
            return GetCategoryByIdAsync(id).GetAwaiter().GetResult();
        }

        public IEnumerable<BlogCategory> GetAllCategories()
        {
            return GetAllCategoriesAsync().GetAwaiter().GetResult();
        }

        public IEnumerable<BlogCategory> GetActiveCategories()
        {
            return GetActiveCategoriesAsync().GetAwaiter().GetResult();
        }

        public IEnumerable<BlogCategory> GetCategoriesWithPosts()
        {
            return GetCategoriesWithPostsAsync().GetAwaiter().GetResult();
        }

        public void AddCategory(BlogCategory category)
        {
            AddCategoryAsync(category).GetAwaiter().GetResult();
        }

        public void UpdateCategory(BlogCategory category)
        {
            UpdateCategoryAsync(category).GetAwaiter().GetResult();
        }

        public void DeleteCategory(int id)
        {
            DeleteCategoryAsync(id).GetAwaiter().GetResult();
        }

        public BlogPost? GetPostById(int id)
        {
            return GetPostByIdAsync(id).GetAwaiter().GetResult();
        }

        public IEnumerable<BlogPost> GetActivePosts()
        {
            return GetActivePostsAsync().GetAwaiter().GetResult();
        }

        public IEnumerable<BlogPost> GetPostsByCategory(int categoryId)
        {
            return GetPostsByCategoryAsync(categoryId).GetAwaiter().GetResult();
        }

        public IEnumerable<BlogPost> GetPostsWithCategory()
        {
            return GetPostsWithCategoryAsync().GetAwaiter().GetResult();
        }

        public IEnumerable<BlogPost> GetRecentPosts(int count = 5)
        {
            return GetRecentPostsAsync(count).GetAwaiter().GetResult();
        }

        public void AddPost(BlogPost post)
        {
            AddPostAsync(post).GetAwaiter().GetResult();
        }

        public void UpdatePost(BlogPost post)
        {
            UpdatePostAsync(post).GetAwaiter().GetResult();
        }

        public void DeletePost(int id)
        {
            DeletePostAsync(id).GetAwaiter().GetResult();
        }

        public void IncrementViewCount(int postId)
        {
            IncrementViewCountAsync(postId).GetAwaiter().GetResult();
        }

        #endregion
    }
}
