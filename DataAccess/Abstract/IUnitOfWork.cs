namespace HarborRestaurant.DataAccess.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IAboutRepository Abouts { get; }
        IContactRepository Contacts { get; }
        IContactMessageRepository ContactMessages { get; }
        IHomePageRepository HomePages { get; }
        IMenuCategoryRepository MenuCategories { get; }
        IMenuItemRepository MenuItems { get; }
        ITableRepository Tables { get; }
        IReservationRepository Reservations { get; }
        IBlogCategoryRepository BlogCategories { get; }
        IBlogPostRepository BlogPosts { get; }

        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
