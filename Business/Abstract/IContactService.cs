using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Abstract
{
    public interface IContactService
    {
        Task<Contact?> GetActiveContactAsync();
        Task<Contact?> GetByIdAsync(int id);
        Task<IEnumerable<Contact>> GetAllAsync();
        Task AddAsync(Contact contact);
        Task<Contact> CreateAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task DeleteAsync(int id);
    }
}
