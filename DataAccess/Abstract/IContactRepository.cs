using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.DataAccess.Abstract
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        Task<Contact?> GetActiveContactAsync();
    }
}
