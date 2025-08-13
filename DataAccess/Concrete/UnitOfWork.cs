using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.DataAccess.Context;

namespace HarborRestaurant.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HarborDbContext _context;

        private IAboutRepository? _abouts;
        private IContactRepository? _contacts;
        private IContactMessageRepository? _contactMessages;
        private IHomePageRepository? _homePages;
        private IMenuCategoryRepository? _menuCategories;
        private IMenuItemRepository? _menuItems;
        private ITableRepository? _tables;
        private IReservationRepository? _reservations;
        private IBlogCategoryRepository? _blogCategories;
        private IBlogPostRepository? _blogPosts;

        public UnitOfWork(HarborDbContext context)
        {
            _context = context;
        }

        public IAboutRepository Abouts => _abouts ??= new AboutRepository(_context);
        public IContactRepository Contacts => _contacts ??= new ContactRepository(_context);
        public IContactMessageRepository ContactMessages => _contactMessages ??= new ContactMessageRepository(_context);
        public IHomePageRepository HomePages => _homePages ??= new HomePageRepository(_context);
        public IMenuCategoryRepository MenuCategories => _menuCategories ??= new MenuCategoryRepository(_context);
        public IMenuItemRepository MenuItems => _menuItems ??= new MenuItemRepository(_context);
        public ITableRepository Tables => _tables ??= new TableRepository(_context);
        public IReservationRepository Reservations => _reservations ??= new ReservationRepository(_context);
        public IBlogCategoryRepository BlogCategories => _blogCategories ??= new BlogCategoryRepository(_context);
        public IBlogPostRepository BlogPosts => _blogPosts ??= new BlogPostRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
