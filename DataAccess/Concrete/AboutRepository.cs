using Microsoft.EntityFrameworkCore;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.DataAccess.Context;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.DataAccess.Concrete
{
    public class AboutRepository : GenericRepository<About>, IAboutRepository
    {
        public AboutRepository(HarborDbContext context) : base(context)
        {
        }

        public async Task<About?> GetActiveAboutAsync()
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.IsActive);
        }
    }
}
