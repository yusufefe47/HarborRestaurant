using HarborRestaurant.Business.Abstract;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Concrete
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Contact?> GetActiveContactAsync()
        {
            return await _unitOfWork.Contacts.GetActiveContactAsync();
        }

        public async Task<Contact?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Contacts.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _unitOfWork.Contacts.GetAllAsync();
        }

        public async Task AddAsync(Contact contact)
        {
            // Yeni contact eklerken diğerlerini pasif yap
            var existingContacts = await _unitOfWork.Contacts.GetAllAsync();
            foreach (var existing in existingContacts)
            {
                existing.IsActive = false;
                existing.UpdatedDate = DateTime.Now;
                _unitOfWork.Contacts.Update(existing);
            }

            contact.IsActive = true;
            contact.CreatedDate = DateTime.Now;
            await _unitOfWork.Contacts.AddAsync(contact);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Contact> CreateAsync(Contact contact)
        {
            // Yeni contact eklerken diğerlerini pasif yap
            var existingContacts = await _unitOfWork.Contacts.GetAllAsync();
            foreach (var existing in existingContacts)
            {
                existing.IsActive = false;
                existing.UpdatedDate = DateTime.Now;
                _unitOfWork.Contacts.Update(existing);
            }

            contact.IsActive = true;
            contact.CreatedDate = DateTime.Now;
            await _unitOfWork.Contacts.AddAsync(contact);
            await _unitOfWork.SaveChangesAsync();
            return contact;
        }

        public async Task UpdateAsync(Contact contact)
        {
            contact.UpdatedDate = DateTime.Now;
            
            // Eğer bu contact aktif ediliyorsa diğerlerini pasif yap
            if (contact.IsActive)
            {
                var existingContacts = await _unitOfWork.Contacts.FindAsync(c => c.ContactId != contact.ContactId);
                foreach (var existing in existingContacts)
                {
                    existing.IsActive = false;
                    existing.UpdatedDate = DateTime.Now;
                    _unitOfWork.Contacts.Update(existing);
                }
            }

            _unitOfWork.Contacts.Update(contact);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var contact = await _unitOfWork.Contacts.GetByIdAsync(id);
            if (contact != null)
            {
                _unitOfWork.Contacts.Remove(contact);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
