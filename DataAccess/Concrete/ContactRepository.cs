using Microsoft.EntityFrameworkCore;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.DataAccess.Context;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.DataAccess.Concrete
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(HarborDbContext context) : base(context)
        {
        }

        public async Task<Contact?> GetActiveContactAsync()
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.IsActive);
        }
    }
}
