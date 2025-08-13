using Microsoft.EntityFrameworkCore;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.DataAccess.Context;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.DataAccess.Concrete
{
    public class HomePageRepository : GenericRepository<HomePage>, IHomePageRepository
    {
        public HomePageRepository(HarborDbContext context) : base(context)
        {
        }

        public async Task<HomePage?> GetActiveHomePageAsync()
        {
            return await _dbSet.FirstOrDefaultAsync(h => h.IsActive);
        }
    }
}
