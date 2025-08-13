using Microsoft.EntityFrameworkCore;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.DataAccess.Context;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.DataAccess.Concrete
{
    public class ContactMessageRepository : GenericRepository<ContactMessage>, IContactMessageRepository
    {
        public ContactMessageRepository(HarborDbContext context) : base(context)
        {
        }

        // Async methods
        public async Task<IEnumerable<ContactMessage>> GetUnreadMessagesAsync()
        {
            return await _dbSet
                .Where(m => !m.IsRead)
                .OrderByDescending(m => m.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ContactMessage>> GetMessagesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(m => m.CreatedDate >= startDate && m.CreatedDate <= endDate)
                .OrderByDescending(m => m.CreatedDate)
                .ToListAsync();
        }

        public async Task MarkAsReadAsync(int messageId)
        {
            var message = await _dbSet.FindAsync(messageId);
            if (message != null)
            {
                message.IsRead = true;
                message.ReadDate = DateTime.Now;
                _dbSet.Update(message);
            }
        }

        public async Task<int> GetUnreadMessageCountAsync()
        {
            return await _dbSet.CountAsync(m => !m.IsRead);
        }

        // Sync methods
        public ContactMessage? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<ContactMessage> GetAll()
        {
            return _dbSet.OrderByDescending(m => m.CreatedDate).ToList();
        }

        public IEnumerable<ContactMessage> GetUnreadMessages()
        {
            return _dbSet
                .Where(m => !m.IsRead)
                .OrderByDescending(m => m.CreatedDate)
                .ToList();
        }

        public IEnumerable<ContactMessage> GetMessagesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _dbSet
                .Where(m => m.CreatedDate >= startDate && m.CreatedDate <= endDate)
                .OrderByDescending(m => m.CreatedDate)
                .ToList();
        }

        public void Add(ContactMessage entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.IsRead = false;
            _dbSet.Add(entity);
        }

        public void Delete(ContactMessage entity)
        {
            _dbSet.Remove(entity);
        }

        public void MarkAsRead(int messageId)
        {
            var message = _dbSet.Find(messageId);
            if (message != null)
            {
                message.IsRead = true;
                message.ReadDate = DateTime.Now;
                _dbSet.Update(message);
            }
        }

        public int GetUnreadMessageCount()
        {
            return _dbSet.Count(m => !m.IsRead);
        }
    }
}
