using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.DataAccess.Abstract
{
    public interface IContactMessageRepository : IGenericRepository<ContactMessage>
    {
        // Async methods
        Task<IEnumerable<ContactMessage>> GetUnreadMessagesAsync();
        Task<IEnumerable<ContactMessage>> GetMessagesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task MarkAsReadAsync(int messageId);
        Task<int> GetUnreadMessageCountAsync();
        
        // Sync methods
        ContactMessage? GetById(int id);
        IEnumerable<ContactMessage> GetAll();
        IEnumerable<ContactMessage> GetUnreadMessages();
        IEnumerable<ContactMessage> GetMessagesByDateRange(DateTime startDate, DateTime endDate);
        void Add(ContactMessage entity);
        void Delete(ContactMessage entity);
        void MarkAsRead(int messageId);
        int GetUnreadMessageCount();
    }
}
