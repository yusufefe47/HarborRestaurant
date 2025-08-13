using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.DataAccess.Abstract
{
    public interface IBlogCategoryRepository : IGenericRepository<BlogCategory>
    {
        Task<IEnumerable<BlogCategory>> GetActiveCategoriesAsync();
        Task<IEnumerable<BlogCategory>> GetCategoriesWithPostsAsync();
    }
}
