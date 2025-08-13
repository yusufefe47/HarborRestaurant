using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.DataAccess.Abstract
{
    public interface IAboutRepository : IGenericRepository<About>
    {
        Task<About?> GetActiveAboutAsync();
    }
}
