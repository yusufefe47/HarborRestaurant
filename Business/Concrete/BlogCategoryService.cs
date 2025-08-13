using HarborRestaurant.Business.Abstract;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace HarborRestaurant.Business.Concrete
{
    public class BlogCategoryService : IBlogCategoryService
    {
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BlogCategoryService(IBlogCategoryRepository blogCategoryRepository, IUnitOfWork unitOfWork)
        {
            _blogCategoryRepository = blogCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<BlogCategory>> GetAllAsync()
        {
            var categories = await _blogCategoryRepository.GetAllAsync();
            return categories.ToList();
        }

        public async Task<List<BlogCategory>> GetActiveAsync()
        {
            var categories = await _blogCategoryRepository.FindAsync(x => x.IsActive);
            return categories.ToList();
        }

        public async Task<BlogCategory?> GetByIdAsync(int id)
        {
            return await _blogCategoryRepository.GetByIdAsync(id);
        }

        public async Task<BlogCategory> CreateAsync(BlogCategory blogCategory)
        {
            blogCategory.CreatedDate = DateTime.Now;
            blogCategory.UpdatedDate = null;
            
            await _blogCategoryRepository.AddAsync(blogCategory);
            await _unitOfWork.SaveChangesAsync();
            return blogCategory;
        }

        public async Task<BlogCategory> UpdateAsync(BlogCategory blogCategory)
        {
            var existing = await _blogCategoryRepository.GetByIdAsync(blogCategory.CategoryId);
            if (existing == null)
                throw new ArgumentException("Blog kategorisi bulunamadı");

            existing.Name = blogCategory.Name;
            existing.Description = blogCategory.Description;
            existing.SortOrder = blogCategory.SortOrder;
            existing.IsActive = blogCategory.IsActive;
            existing.UpdatedDate = DateTime.Now;

            _blogCategoryRepository.Update(existing);
            await _unitOfWork.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var blogCategory = await _blogCategoryRepository.GetByIdAsync(id);
            if (blogCategory == null)
                return false;

            // Check if category has blog posts
            if (blogCategory.BlogPosts?.Any() == true)
            {
                throw new InvalidOperationException("Bu kategoriye ait blog yazıları bulunduğu için silinemez");
            }

            _blogCategoryRepository.Remove(blogCategory);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ToggleActiveAsync(int id)
        {
            var blogCategory = await _blogCategoryRepository.GetByIdAsync(id);
            if (blogCategory == null)
                return false;

            blogCategory.IsActive = !blogCategory.IsActive;
            blogCategory.UpdatedDate = DateTime.Now;
            
            _blogCategoryRepository.Update(blogCategory);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _blogCategoryRepository.AnyAsync(x => x.CategoryId == id);
        }

        public async Task<bool> NameExistsAsync(string name, int? excludeId = null)
        {
            return await _blogCategoryRepository.AnyAsync(x => 
                x.Name.ToLower() == name.ToLower() && 
                (excludeId == null || x.CategoryId != excludeId));
        }
    }
}
