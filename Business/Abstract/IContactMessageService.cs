using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.Business.Abstract
{
    public interface IContactMessageService
    {
        // Async Methods
        Task<ContactMessage?> GetByIdAsync(int id);
        Task<ContactMessage?> GetContactMessageByIdAsync(int id);
        Task<IEnumerable<ContactMessage>> GetAllAsync();
        Task<IEnumerable<ContactMessage>> GetAllContactMessagesAsync();
        Task<IEnumerable<ContactMessage>> GetUnreadMessagesAsync();
        Task<IEnumerable<ContactMessage>> GetUnreadContactMessagesAsync();
        Task<IEnumerable<ContactMessage>> GetMessagesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task CreateAsync(ContactMessage message);
        Task AddAsync(ContactMessage message);
        Task UpdateAsync(ContactMessage message);
        Task UpdateContactMessageAsync(ContactMessage message);
        Task MarkAsReadAsync(int messageId);
        Task DeleteAsync(int id);
        Task DeleteContactMessageAsync(int id);
        Task<int> GetUnreadMessageCountAsync();

        // Sync Methods
        ContactMessage? GetById(int id);
        IEnumerable<ContactMessage> GetAll();
        IEnumerable<ContactMessage> GetUnreadMessages();
        IEnumerable<ContactMessage> GetMessagesByDateRange(DateTime startDate, DateTime endDate);
        void Create(ContactMessage message);
        void Add(ContactMessage message);
        void Update(ContactMessage message);
        void MarkAsRead(int messageId);
        void Delete(ContactMessage message);
        int GetUnreadMessageCount();
    }
}
