using HarborRestaurant.Business.Abstract;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Concrete
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactMessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Async Methods
        public async Task<ContactMessage?> GetByIdAsync(int id)
        {
            return await _unitOfWork.ContactMessages.GetByIdAsync(id);
        }

        public async Task<ContactMessage?> GetContactMessageByIdAsync(int id)
        {
            return await _unitOfWork.ContactMessages.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ContactMessage>> GetAllAsync()
        {
            var messages = await _unitOfWork.ContactMessages.GetAllAsync();
            return messages.OrderByDescending(m => m.SentDate);
        }

        public async Task<IEnumerable<ContactMessage>> GetAllContactMessagesAsync()
        {
            var messages = await _unitOfWork.ContactMessages.GetAllAsync();
            return messages.OrderByDescending(m => m.SentDate);
        }

        public async Task<IEnumerable<ContactMessage>> GetUnreadMessagesAsync()
        {
            return await _unitOfWork.ContactMessages.GetUnreadMessagesAsync();
        }

        public async Task<IEnumerable<ContactMessage>> GetUnreadContactMessagesAsync()
        {
            return await _unitOfWork.ContactMessages.GetUnreadMessagesAsync();
        }

        public async Task<IEnumerable<ContactMessage>> GetMessagesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _unitOfWork.ContactMessages.GetMessagesByDateRangeAsync(startDate, endDate);
        }

        public async Task AddAsync(ContactMessage message)
        {
            message.SentDate = DateTime.Now;
            message.IsRead = false;
            
            await _unitOfWork.ContactMessages.AddAsync(message);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateAsync(ContactMessage message)
        {
            await AddAsync(message);
        }

        public async Task UpdateAsync(ContactMessage message)
        {
            _unitOfWork.ContactMessages.Update(message);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateContactMessageAsync(ContactMessage message)
        {
            _unitOfWork.ContactMessages.Update(message);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task MarkAsReadAsync(int messageId)
        {
            await _unitOfWork.ContactMessages.MarkAsReadAsync(messageId);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var message = await _unitOfWork.ContactMessages.GetByIdAsync(id);
            if (message != null)
            {
                _unitOfWork.ContactMessages.Delete(message);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteContactMessageAsync(int id)
        {
            var message = await _unitOfWork.ContactMessages.GetByIdAsync(id);
            if (message != null)
            {
                _unitOfWork.ContactMessages.Delete(message);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<int> GetUnreadMessageCountAsync()
        {
            return await _unitOfWork.ContactMessages.GetUnreadMessageCountAsync();
        }

        // Sync Methods
        public ContactMessage? GetById(int id)
        {
            return _unitOfWork.ContactMessages.GetById(id);
        }

        public IEnumerable<ContactMessage> GetAll()
        {
            var messages = _unitOfWork.ContactMessages.GetAll();
            return messages.OrderByDescending(m => m.SentDate);
        }

        public IEnumerable<ContactMessage> GetUnreadMessages()
        {
            return _unitOfWork.ContactMessages.GetUnreadMessages();
        }

        public IEnumerable<ContactMessage> GetMessagesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _unitOfWork.ContactMessages.GetMessagesByDateRange(startDate, endDate);
        }

        public void Create(ContactMessage message)
        {
            Add(message);
        }

        public void Add(ContactMessage message)
        {
            message.SentDate = DateTime.Now;
            message.IsRead = false;
            
            _unitOfWork.ContactMessages.Add(message);
            _unitOfWork.SaveChanges();
        }

        public void Update(ContactMessage message)
        {
            _unitOfWork.ContactMessages.Update(message);
            _unitOfWork.SaveChanges();
        }

        public void MarkAsRead(int messageId)
        {
            _unitOfWork.ContactMessages.MarkAsRead(messageId);
            _unitOfWork.SaveChanges();
        }

        public void Delete(ContactMessage message)
        {
            _unitOfWork.ContactMessages.Delete(message);
            _unitOfWork.SaveChanges();
        }

        public int GetUnreadMessageCount()
        {
            return _unitOfWork.ContactMessages.GetUnreadMessageCount();
        }
    }
}
