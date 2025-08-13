using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.DataAccess.Abstract
{
    public interface IHomePageRepository : IGenericRepository<HomePage>
    {
        Task<HomePage?> GetActiveHomePageAsync();
    }
}
