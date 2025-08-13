using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HarborRestaurant.Entities.Concrete;

namespace HarborRestaurant.DataAccess.Context
{
    public class HarborDbContext : IdentityDbContext<AppUser>
    {
        public HarborDbContext(DbContextOptions<HarborDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.ConfigureWarnings(warnings => 
                    warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
            }
        }

        // DbSet tanımlamaları
        public DbSet<About> Abouts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<HomePage> HomePages { get; set; }
        public DbSet<MenuCategory> MenuCategories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // MenuCategory - MenuItem ilişkisi
            modelBuilder.Entity<MenuItem>()
                .HasOne(mi => mi.Category)
                .WithMany(mc => mc.MenuItems)
                .HasForeignKey(mi => mi.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Table - Reservation ilişkisi
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Table)
                .WithMany(t => t.Reservations)
                .HasForeignKey(r => r.TableId)
                .OnDelete(DeleteBehavior.SetNull);

            // Room - Reservation ilişkisi (opsiyonel)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Room)
                .WithMany()
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.SetNull);

            // BlogCategory - BlogPost ilişkisi
            modelBuilder.Entity<BlogPost>()
                .HasOne(bp => bp.Category)
                .WithMany(bc => bc.BlogPosts)
                .HasForeignKey(bp => bp.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Decimal precision ayarları
            modelBuilder.Entity<MenuItem>()
                .Property(m => m.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Room>()
                .Property(r => r.MinimumOrderAmount)
                .HasPrecision(10, 2);

            // Index tanımlamaları
            modelBuilder.Entity<ContactMessage>()
                .HasIndex(cm => cm.Email);

            modelBuilder.Entity<Reservation>()
                .HasIndex(r => r.Email);

            modelBuilder.Entity<Reservation>()
                .HasIndex(r => new { r.CheckInDate, r.ReservationTime });

            modelBuilder.Entity<BlogPost>()
                .HasIndex(bp => bp.CreatedDate);

            // Seed Data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // HomePage seed data
            modelBuilder.Entity<HomePage>().HasData(
                new HomePage
                {
                    HomePageId = 1,
                    MainTitle = "Harbor Restaurant'a Hoş Geldiniz",
                    Subtitle = "Deniz Manzarası Eşliğinde Eşsiz Lezzetler",
                    Description = "Harbor Restaurant olarak, deniz ürünlerinden et yemeklerine kadar geniş bir menü yelpazesi ile hizmet vermekteyiz.",
                    HeroImageUrl = "/images/bg_1.jpg",
                    ButtonText = "Rezervasyon Yap",
                    ButtonUrl = "/Reservation",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                }
            );

            // About seed data
            modelBuilder.Entity<About>().HasData(
                new About
                {
                    AboutId = 1,
                    Title = "Hakkımızda",
                    Subtitle = "25 Yıllık Deneyim",
                    Description = "Harbor Restaurant olarak 1998 yılından beri müşterilerimize kaliteli hizmet vermekteyiz. Deniz manzarası eşliğinde, taze deniz ürünleri ve özel etlerle hazırladığımız lezzetleri sizlere sunuyoruz.",
                    ImageUrl = "/images/about.jpg",
                    VideoUrl = "https://vimeo.com/45830194",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                }
            );

            // Contact seed data
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    ContactId = 1,
                    Address = "Atatürk Bulvarı No:123, Alsancak/İzmir",
                    Phone = "+90 232 123 45 67",
                    Email = "info@harborrestaurant.com",
                    WorkingHours = "Pazartesi - Pazar: 11:00 - 24:00",
                    MapUrl = "https://goo.gl/maps/xyz",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                }
            );

            // MenuCategory seed data
            modelBuilder.Entity<MenuCategory>().HasData(
                new MenuCategory { CategoryId = 1, Name = "Başlangıçlar", Description = "Nefis başlangıç lezzetleri", SortOrder = 1, IsActive = true, CreatedDate = DateTime.Now },
                new MenuCategory { CategoryId = 2, Name = "Deniz Ürünleri", Description = "Taze deniz ürünleri", SortOrder = 2, IsActive = true, CreatedDate = DateTime.Now },
                new MenuCategory { CategoryId = 3, Name = "Et Yemekleri", Description = "Özel et yemekleri", SortOrder = 3, IsActive = true, CreatedDate = DateTime.Now },
                new MenuCategory { CategoryId = 4, Name = "Tatlılar", Description = "Ev yapımı tatlılar", SortOrder = 4, IsActive = true, CreatedDate = DateTime.Now },
                new MenuCategory { CategoryId = 5, Name = "İçecekler", Description = "Soğuk ve sıcak içecekler", SortOrder = 5, IsActive = true, CreatedDate = DateTime.Now }
            );

            // MenuItem seed data
            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem { ItemId = 1, Name = "Deniz Börülcesi", Description = "Taze deniz börülcesi salatası", Price = 85.00m, CategoryId = 1, ImageUrl = "/images/menu-1.jpg", IsActive = true, IsSpecial = true, CreatedDate = DateTime.Now },
                new MenuItem { ItemId = 2, Name = "Karides Kokteyli", Description = "Avokado eşliğinde karides", Price = 125.00m, CategoryId = 1, ImageUrl = "/images/menu-2.jpg", IsActive = true, CreatedDate = DateTime.Now },
                new MenuItem { ItemId = 3, Name = "Levrek Izgara", Description = "Taze levrek balığı ızgara", Price = 185.00m, CategoryId = 2, ImageUrl = "/images/menu-3.jpg", IsActive = true, IsSpecial = true, CreatedDate = DateTime.Now },
                new MenuItem { ItemId = 4, Name = "Somon Teriyaki", Description = "Özel soslu somon fileto", Price = 225.00m, CategoryId = 2, ImageUrl = "/images/menu-4.jpg", IsActive = true, CreatedDate = DateTime.Now },
                new MenuItem { ItemId = 5, Name = "Dana Bonfile", Description = "200gr dana bonfile, garnitür eşliğinde", Price = 285.00m, CategoryId = 3, ImageUrl = "/images/menu-5.jpg", IsActive = true, IsSpecial = true, CreatedDate = DateTime.Now },
                new MenuItem { ItemId = 6, Name = "Kuzu Pirzola", Description = "Özel marine kuzu pirzola", Price = 245.00m, CategoryId = 3, ImageUrl = "/images/menu-6.jpg", IsActive = true, CreatedDate = DateTime.Now },
                new MenuItem { ItemId = 7, Name = "Tiramisu", Description = "Ev yapımı tiramisu", Price = 65.00m, CategoryId = 4, ImageUrl = "/images/menu-7.jpg", IsActive = true, CreatedDate = DateTime.Now },
                new MenuItem { ItemId = 8, Name = "Çikolata Sufle", Description = "Sıcak çikolata sufle", Price = 75.00m, CategoryId = 4, ImageUrl = "/images/menu-8.jpg", IsActive = true, CreatedDate = DateTime.Now },
                new MenuItem { ItemId = 9, Name = "Şef Özel Kokteylli", Description = "Günün özel kokteyli", Price = 95.00m, CategoryId = 5, ImageUrl = "/images/menu-9.jpg", IsActive = true, CreatedDate = DateTime.Now }
            );

            // BlogCategory seed data
            modelBuilder.Entity<BlogCategory>().HasData(
                new BlogCategory { CategoryId = 1, Name = "Haberler", Description = "Restaurant haberleri", SortOrder = 1, IsActive = true, CreatedDate = DateTime.Now },
                new BlogCategory { CategoryId = 2, Name = "Etkinlikler", Description = "Özel etkinlikler", SortOrder = 2, IsActive = true, CreatedDate = DateTime.Now },
                new BlogCategory { CategoryId = 3, Name = "Tarifler", Description = "Şef tarifleri", SortOrder = 3, IsActive = true, CreatedDate = DateTime.Now }
            );

            // Table seed data
            modelBuilder.Entity<Table>().HasData(
                new Table { TableId = 1, TableNumber = "1", Capacity = 2, Location = "Pencere kenarı", IsActive = true, CreatedDate = DateTime.Now },
                new Table { TableId = 2, TableNumber = "2", Capacity = 4, Location = "Merkez", IsActive = true, CreatedDate = DateTime.Now },
                new Table { TableId = 3, TableNumber = "3", Capacity = 6, Location = "Terasta", IsActive = true, CreatedDate = DateTime.Now },
                new Table { TableId = 4, TableNumber = "4", Capacity = 8, Location = "VIP", IsActive = true, CreatedDate = DateTime.Now },
                new Table { TableId = 5, TableNumber = "5", Capacity = 2, Location = "Pencere kenarı", IsActive = true, CreatedDate = DateTime.Now }
            );

            // Room seed data
            modelBuilder.Entity<Room>().HasData(
                new Room { RoomId = 1, Name = "Aile Salonu", Description = "8-15 kişilik kapasiteye sahip Aile Salonu, aile toplantıları ve özel kutlamalar için ideal.", Capacity = 15, MinimumOrderAmount = 200, ImageUrl = "/images/room-1.jpg", Features = "Rahat oturma düzeni, Özel menü seçenekleri, Çocuk dostu ortam", StarRating = 5, IsActive = true, CreatedDate = DateTime.Now, SortOrder = 1 },
                new Room { RoomId = 2, Name = "Lüks Salon", Description = "15-25 kişilik kapasiteye sahip Lüks Salon, özel etkinlikler ve iş yemekleri için mükemmel.", Capacity = 25, MinimumOrderAmount = 300, ImageUrl = "/images/room-2.jpg", Features = "Şık dekorasyon, Konforlu atmosfer, Özel servis", StarRating = 5, IsActive = true, CreatedDate = DateTime.Now, SortOrder = 2 },
                new Room { RoomId = 3, Name = "Konferans Salonu", Description = "30-50 kişilik kapasiteye sahip Konferans Salonu, kurumsal etkinlikler için ideal.", Capacity = 50, MinimumOrderAmount = 350, ImageUrl = "/images/room-3.jpg", Features = "Projeksiyon sistemi, Sesli sistem, Klima", StarRating = 5, IsActive = true, CreatedDate = DateTime.Now, SortOrder = 3 },
                new Room { RoomId = 4, Name = "Deniz Manzaralı Salon", Description = "20-35 kişilik kapasiteye sahip muhteşem deniz manzaralı salon.", Capacity = 35, MinimumOrderAmount = 400, ImageUrl = "/images/room-4.jpg", Features = "Deniz manzarası, Doğal ışık, Fotoğraf çekimi için ideal", StarRating = 5, IsActive = true, CreatedDate = DateTime.Now, SortOrder = 4 },
                new Room { RoomId = 5, Name = "Bahçe Salonu", Description = "40-60 kişilik kapasiteye sahip açık havada hizmet veren bahçe salonu.", Capacity = 60, MinimumOrderAmount = 250, ImageUrl = "/images/room-5.jpg", Features = "Açık hava, Bahçe manzarası, Doğal ortam", StarRating = 5, IsActive = true, CreatedDate = DateTime.Now, SortOrder = 5 },
                new Room { RoomId = 6, Name = "VIP Salon", Description = "10-20 kişilik kapasiteye sahip özel VIP salon, lüks hizmet.", Capacity = 20, MinimumOrderAmount = 500, ImageUrl = "/images/room-6.jpg", Features = "VIP servis, Özel menü, Lüks donanım", StarRating = 5, IsActive = true, CreatedDate = DateTime.Now, SortOrder = 6 }
            );
        }
    }
}
