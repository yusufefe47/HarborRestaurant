using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HarborRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class InitialMSSQLMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abouts",
                columns: table => new
                {
                    AboutId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VideoUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abouts", x => x.AboutId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    ContactMessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReadDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsReplied = table.Column<bool>(type: "bit", nullable: false),
                    ReplyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.ContactMessageId);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WorkingHours = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MapUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MapLatitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MapLongitude = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "HomePages",
                columns: table => new
                {
                    HomePageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    HeroImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ButtonText = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ButtonUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomePages", x => x.HomePageId);
                });

            migrationBuilder.CreateTable(
                name: "MenuCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    MinimumOrderAmount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Features = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StarRating = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    TableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.TableId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    PostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_BlogPosts_BlogCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BlogCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Ingredients = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    PreparationTime = table.Column<int>(type: "int", nullable: true),
                    Calories = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    IsSpecial = table.Column<bool>(type: "bit", nullable: false),
                    IsSpicy = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_MenuItems_MenuCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "MenuCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GuestCount = table.Column<int>(type: "int", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    SpecialRequests = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TableId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Tables_TableId",
                        column: x => x.TableId,
                        principalTable: "Tables",
                        principalColumn: "TableId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "Abouts",
                columns: new[] { "AboutId", "CreatedDate", "Description", "ImageUrl", "IsActive", "Subtitle", "Title", "UpdatedDate", "VideoUrl" },
                values: new object[] { 1, new DateTime(2025, 8, 7, 0, 24, 35, 234, DateTimeKind.Local).AddTicks(9498), "Harbor Restaurant olarak 1998 yılından beri müşterilerimize kaliteli hizmet vermekteyiz. Deniz manzarası eşliğinde, taze deniz ürünleri ve özel etlerle hazırladığımız lezzetleri sizlere sunuyoruz.", "/images/about.jpg", true, "25 Yıllık Deneyim", "Hakkımızda", null, "https://vimeo.com/45830194" });

            migrationBuilder.InsertData(
                table: "BlogCategories",
                columns: new[] { "CategoryId", "CreatedDate", "Description", "IsActive", "Name", "SortOrder", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(2419), "Restaurant haberleri", true, "Haberler", 1, null },
                    { 2, new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(2740), "Özel etkinlikler", true, "Etkinlikler", 2, null },
                    { 3, new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(2742), "Şef tarifleri", true, "Tarifler", 3, null }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "ContactId", "Address", "CreatedDate", "Email", "IsActive", "MapLatitude", "MapLongitude", "MapUrl", "Phone", "UpdatedDate", "WorkingHours" },
                values: new object[] { 1, "Atatürk Bulvarı No:123, Alsancak/İzmir", new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(3083), "info@harborrestaurant.com", true, null, null, "https://goo.gl/maps/xyz", "+90 232 123 45 67", null, "Pazartesi - Pazar: 11:00 - 24:00" });

            migrationBuilder.InsertData(
                table: "HomePages",
                columns: new[] { "HomePageId", "ButtonText", "ButtonUrl", "CreatedDate", "Description", "HeroImageUrl", "IsActive", "MainTitle", "Subtitle", "UpdatedDate" },
                values: new object[] { 1, "Rezervasyon Yap", "/Reservation", new DateTime(2025, 8, 7, 0, 24, 35, 234, DateTimeKind.Local).AddTicks(444), "Harbor Restaurant olarak, deniz ürünlerinden et yemeklerine kadar geniş bir menü yelpazesi ile hizmet vermekteyiz.", "/images/bg_1.jpg", true, "Harbor Restaurant'a Hoş Geldiniz", "Deniz Manzarası Eşliğinde Eşsiz Lezzetler", null });

            migrationBuilder.InsertData(
                table: "MenuCategories",
                columns: new[] { "CategoryId", "CreatedDate", "Description", "DisplayOrder", "ImageUrl", "IsActive", "Name", "SortOrder", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(5955), "Nefis başlangıç lezzetleri", 0, null, true, "Başlangıçlar", 1, null },
                    { 2, new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(6254), "Taze deniz ürünleri", 0, null, true, "Deniz Ürünleri", 2, null },
                    { 3, new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(6256), "Özel et yemekleri", 0, null, true, "Et Yemekleri", 3, null },
                    { 4, new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(6259), "Ev yapımı tatlılar", 0, null, true, "Tatlılar", 4, null },
                    { 5, new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(6261), "Soğuk ve sıcak içecekler", 0, null, true, "İçecekler", 5, null }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "RoomId", "Capacity", "CreatedDate", "Description", "Features", "ImageUrl", "IsActive", "MinimumOrderAmount", "Name", "SortOrder", "StarRating", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 15, new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(9230), "8-15 kişilik kapasiteye sahip Aile Salonu, aile toplantıları ve özel kutlamalar için ideal.", "Rahat oturma düzeni, Özel menü seçenekleri, Çocuk dostu ortam", "/images/room-1.jpg", true, 200m, "Aile Salonu", 1, 5, null },
                    { 2, 25, new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(9804), "15-25 kişilik kapasiteye sahip Lüks Salon, özel etkinlikler ve iş yemekleri için mükemmel.", "Şık dekorasyon, Konforlu atmosfer, Özel servis", "/images/room-2.jpg", true, 300m, "Lüks Salon", 2, 5, null },
                    { 3, 50, new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(9808), "30-50 kişilik kapasiteye sahip Konferans Salonu, kurumsal etkinlikler için ideal.", "Projeksiyon sistemi, Sesli sistem, Klima", "/images/room-3.jpg", true, 350m, "Konferans Salonu", 3, 5, null },
                    { 4, 35, new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(9811), "20-35 kişilik kapasiteye sahip muhteşem deniz manzaralı salon.", "Deniz manzarası, Doğal ışık, Fotoğraf çekimi için ideal", "/images/room-4.jpg", true, 400m, "Deniz Manzaralı Salon", 4, 5, null },
                    { 5, 60, new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(9814), "40-60 kişilik kapasiteye sahip açık havada hizmet veren bahçe salonu.", "Açık hava, Bahçe manzarası, Doğal ortam", "/images/room-5.jpg", true, 250m, "Bahçe Salonu", 5, 5, null },
                    { 6, 20, new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(9816), "10-20 kişilik kapasiteye sahip özel VIP salon, lüks hizmet.", "VIP servis, Özel menü, Lüks donanım", "/images/room-6.jpg", true, 500m, "VIP Salon", 6, 5, null }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "TableId", "Capacity", "CreatedDate", "IsActive", "IsAvailable", "Location", "TableNumber", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(5211), true, true, "Pencere kenarı", "1", null },
                    { 2, 4, new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(5496), true, true, "Merkez", "2", null },
                    { 3, 6, new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(5499), true, true, "Terasta", "3", null },
                    { 4, 8, new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(5501), true, true, "VIP", "4", null },
                    { 5, 2, new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(5503), true, true, "Pencere kenarı", "5", null }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "ItemId", "Calories", "CategoryId", "CreatedDate", "Description", "ImageUrl", "Ingredients", "IsActive", "IsAvailable", "IsFeatured", "IsSpecial", "IsSpicy", "Name", "PreparationTime", "Price", "SortOrder", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, null, 1, new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(9440), "Taze deniz börülcesi salatası", "/images/menu-1.jpg", null, true, true, false, true, false, "Deniz Börülcesi", null, 85.00m, 0, null },
                    { 2, null, 1, new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(9759), "Avokado eşliğinde karides", "/images/menu-2.jpg", null, true, true, false, false, false, "Karides Kokteyli", null, 125.00m, 0, null },
                    { 3, null, 2, new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(9761), "Taze levrek balığı ızgara", "/images/menu-3.jpg", null, true, true, false, true, false, "Levrek Izgara", null, 185.00m, 0, null },
                    { 4, null, 2, new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(9787), "Özel soslu somon fileto", "/images/menu-4.jpg", null, true, true, false, false, false, "Somon Teriyaki", null, 225.00m, 0, null },
                    { 5, null, 3, new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(9790), "200gr dana bonfile, garnitür eşliğinde", "/images/menu-5.jpg", null, true, true, false, true, false, "Dana Bonfile", null, 285.00m, 0, null },
                    { 6, null, 3, new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(9793), "Özel marine kuzu pirzola", "/images/menu-6.jpg", null, true, true, false, false, false, "Kuzu Pirzola", null, 245.00m, 0, null },
                    { 7, null, 4, new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(9795), "Ev yapımı tiramisu", "/images/menu-7.jpg", null, true, true, false, false, false, "Tiramisu", null, 65.00m, 0, null },
                    { 8, null, 4, new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(9797), "Sıcak çikolata sufle", "/images/menu-8.jpg", null, true, true, false, false, false, "Çikolata Sufle", null, 75.00m, 0, null },
                    { 9, null, 5, new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(9799), "Günün özel kokteyli", "/images/menu-9.jpg", null, true, true, false, false, false, "Şef Özel Kokteylli", null, 95.00m, 0, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_CategoryId",
                table: "BlogPosts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_CreatedDate",
                table: "BlogPosts",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_ContactMessages_Email",
                table: "ContactMessages",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_CategoryId",
                table: "MenuItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CheckInDate_ReservationTime",
                table: "Reservations",
                columns: new[] { "CheckInDate", "ReservationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_Email",
                table: "Reservations",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TableId",
                table: "Reservations",
                column: "TableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abouts");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "HomePages");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BlogCategories");

            migrationBuilder.DropTable(
                name: "MenuCategories");

            migrationBuilder.DropTable(
                name: "Tables");
        }
    }
}
