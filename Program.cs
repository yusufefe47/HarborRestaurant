using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HarborRestaurant.DataAccess.Context;
using HarborRestaurant.DataAccess.Abstract;
using HarborRestaurant.DataAccess.Concrete;
using HarborRestaurant.Business.Abstract;
using HarborRestaurant.Business.Concrete;
using HarborRestaurant.Entities.Concrete;
using HarborRestaurant.Hubs;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel for maximum performance
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxConcurrentConnections = 1000;
    serverOptions.Limits.MaxConcurrentUpgradedConnections = 1000;
    serverOptions.Limits.MaxRequestBodySize = 52428800; // 50MB
    serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
    serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(30);
    
    // Enable HTTP/2 and HTTP/3 for better performance
    serverOptions.ConfigureEndpointDefaults(listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
    });
});

// Add services to the container.
builder.Services
    .AddControllersWithViews(options =>
    {
        // Antiforgery'yi tamamen kapat - WARNING'leri önlemek için
        options.Filters.Add(new Microsoft.AspNetCore.Mvc.IgnoreAntiforgeryTokenAttribute());
    })
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();

// Performance Optimization: Response Caching - WARNING'leri azaltmak için geçici kapalı
/*
builder.Services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 1024 * 1024; // 1MB
    options.UseCaseSensitivePaths = false;
});
*/

// Performance Optimization: Memory Cache
builder.Services.AddMemoryCache();

// Performance Optimization: Response Compression with optimized settings
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = new[]
    {
        "text/plain",
        "text/css",
        "application/javascript",
        "text/html",
        "application/xml",
        "text/xml",
        "application/json",
        "text/json",
        "image/svg+xml"
    };
});

// Configure compression levels for maximum performance
builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Optimal;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.SmallestSize;
});

// Entity Framework ve Identity yapılandırması
builder.Services.AddDbContext<HarborDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(3);
        sqlOptions.CommandTimeout(30);
    })
    .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
    .EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
    .EnableServiceProviderCaching()
    .ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning)));

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    // User settings
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<HarborDbContext>()
.AddDefaultTokenProviders();

// Repository ve Service kayıtları
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAboutService, AboutService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IContactMessageService, ContactMessageService>();
builder.Services.AddScoped<IHomePageService, HomePageService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IBlogCategoryService, BlogCategoryService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<ITranslationService, TranslationService>();

// Performance Services
builder.Services.AddScoped<CacheInvalidationService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IReportService, ReportService>();
// HttpClient (tek kayıt yeterli)
builder.Services.AddHttpClient();

// Repository kayıtları
builder.Services.AddScoped<IAboutRepository, AboutRepository>();
builder.Services.AddScoped<IBlogCategoryRepository, BlogCategoryRepository>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactMessageRepository, ContactMessageRepository>();
builder.Services.AddScoped<IHomePageRepository, HomePageRepository>();
builder.Services.AddScoped<IMenuCategoryRepository, MenuCategoryRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("tr"),
        new CultureInfo("en")
    };

    options.DefaultRequestCulture = new RequestCulture("tr");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    
    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
    options.RequestCultureProviders.Insert(1, new CookieRequestCultureProvider());
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// SignalR
builder.Services.AddSignalR();

// Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Performance Optimization: Response Compression (must be early in pipeline)
app.UseResponseCompression();

// Performance Optimization: Response Caching - WARNING'leri azaltmak için geçici kapalı
// app.UseResponseCaching();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}
app.UseRouting();

// Localization
app.UseRequestLocalization();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Performance Optimization: Static Files with Moderate Caching
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // Cache static files for 7 days - moderate caching
        var headers = ctx.Context.Response.GetTypedHeaders();
        headers.CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
        {
            Public = true,
            MaxAge = TimeSpan.FromDays(7) // 7 days cache for static assets
        };
    }
});

// SignalR Hub
app.MapHub<NotificationHub>("/notificationHub");

// Areas route
app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

// Default route with caching
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Performance Optimization: Map static assets with caching
app.MapStaticAssets();

// Update translations on startup (run once)
try
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<HarborDbContext>();
        
        // Update Rooms
        var rooms = context.Rooms.ToList();
        foreach (var room in rooms)
        {
            if (string.IsNullOrEmpty(room.NameEn))
            {
                switch (room.RoomId)
                {
                    case 1:
                        room.NameEn = "Deluxe Sea View Room";
                        room.DescriptionEn = "Spacious room with stunning sea view, private balcony and modern amenities. Perfect for couples seeking luxury and comfort.";
                        break;
                    case 2:
                        room.NameEn = "Premium Suite";
                        room.DescriptionEn = "Luxurious suite with separate living area, panoramic sea view and exclusive services. Ideal for special occasions.";
                        break;
                    case 3:
                        room.NameEn = "Family Room";
                        room.DescriptionEn = "Comfortable family room with multiple beds, children's area and all necessary amenities for a perfect family vacation.";
                        break;
                    case 4:
                        room.NameEn = "Standard Room";
                        room.DescriptionEn = "Cozy and comfortable room with modern amenities, perfect for business travelers and couples.";
                        break;
                    case 5:
                        room.NameEn = "Garden View Room";
                        room.DescriptionEn = "Peaceful room overlooking the beautiful garden, offering tranquility and comfort away from the city noise.";
                        break;
                    case 6:
                        room.NameEn = "Executive Suite";
                        room.DescriptionEn = "Premium executive suite with office area, meeting facilities and exclusive business services.";
                        break;
                }
            }
        }
        
        // Update MenuItems
        var menuItems = context.MenuItems.ToList();
        foreach (var item in menuItems)
        {
            if (string.IsNullOrEmpty(item.NameEn))
            {
                switch (item.ItemId)
                {
                    case 1:
                        item.NameEn = "Grilled Sea Bass";
                        item.DescriptionEn = "Fresh sea bass grilled to perfection, served with seasonal vegetables and lemon butter sauce.";
                        break;
                    case 2:
                        item.NameEn = "Mediterranean Seafood Platter";
                        item.DescriptionEn = "A selection of fresh seafood including shrimp, calamari, and mussels with Mediterranean herbs.";
                        break;
                    case 3:
                        item.NameEn = "Lamb Chops";
                        item.DescriptionEn = "Tender lamb chops marinated with Turkish spices, grilled and served with roasted vegetables.";
                        break;
                    case 4:
                        item.NameEn = "Stuffed Eggplant";
                        item.DescriptionEn = "Traditional Turkish stuffed eggplant with ground meat, onions, and aromatic spices.";
                        break;
                    case 5:
                        item.NameEn = "Turkish Breakfast";
                        item.DescriptionEn = "Complete Turkish breakfast with cheese, olives, tomatoes, cucumbers, honey and fresh bread.";
                        break;
                    case 6:
                        item.NameEn = "Baklava";
                        item.DescriptionEn = "Traditional Turkish dessert made with layers of phyllo pastry, nuts and honey syrup.";
                        break;
                    case 7:
                        item.NameEn = "Turkish Coffee";
                        item.DescriptionEn = "Authentic Turkish coffee served with Turkish delight, a UNESCO recognized cultural heritage.";
                        break;
                    case 8:
                        item.NameEn = "Meze Platter";
                        item.DescriptionEn = "Assorted Turkish appetizers including hummus, ezme, cacik and stuffed grape leaves.";
                        break;
                    case 9:
                        item.NameEn = "Grilled Octopus";
                        item.DescriptionEn = "Tender grilled octopus with olive oil, lemon and fresh herbs, a Mediterranean delicacy.";
                        break;
                }
            }
        }
        
        // About çevirilerini ekle
    var about = context.Abouts.OrderBy(a => a.CreatedDate).FirstOrDefault();
        if (about != null && string.IsNullOrEmpty(about.TitleEn))
        {
            about.TitleEn = "About Harborlights";
            about.SubtitleEn = "Welcome to Harborlights";
            about.DescriptionEn = "Harborlights has been serving on Izmir's most prestigious coastline since 1998, offering unique flavors with sea views. We create unforgettable moments with fresh seafood and carefully prepared dishes.";
        }
        
        // Contact çevirilerini ekle
    var contact = context.Contacts.OrderBy(c => c.CreatedDate).FirstOrDefault();
        if (contact != null && string.IsNullOrEmpty(contact.AddressEn))
        {
            contact.AddressEn = "Alsancak Boulevard No:123, Alsancak/Izmir";
            contact.WorkingHoursEn = "Monday - Thursday: 11:00 AM - 12:00 AM | Friday - Sunday: 11:00 AM - 02:00 AM";
        }
        
        context.SaveChanges();
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Translation update error: {ex.Message}");
}

app.Run();
