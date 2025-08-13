using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HarborRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class AddEnglishTranslationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "Rooms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                table: "MenuItems",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "MenuItems",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentEn",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SummaryEn",
                table: "BlogPosts",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleEn",
                table: "BlogPosts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Abouts",
                keyColumn: "AboutId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 462, DateTimeKind.Local).AddTicks(4622));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(1956));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(2116));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(2118));

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "ContactId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 462, DateTimeKind.Local).AddTicks(6332));

            migrationBuilder.UpdateData(
                table: "HomePages",
                keyColumn: "HomePageId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 461, DateTimeKind.Local).AddTicks(8856));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 462, DateTimeKind.Local).AddTicks(8064));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 462, DateTimeKind.Local).AddTicks(8222));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 462, DateTimeKind.Local).AddTicks(8224));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 462, DateTimeKind.Local).AddTicks(8226));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 462, DateTimeKind.Local).AddTicks(8227));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "DescriptionEn", "NameEn" },
                values: new object[] { new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(80), null, null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "DescriptionEn", "NameEn" },
                values: new object[] { new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(232), null, null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "DescriptionEn", "NameEn" },
                values: new object[] { new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(234), null, null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 4,
                columns: new[] { "CreatedDate", "DescriptionEn", "NameEn" },
                values: new object[] { new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(236), null, null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 5,
                columns: new[] { "CreatedDate", "DescriptionEn", "NameEn" },
                values: new object[] { new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(238), null, null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 6,
                columns: new[] { "CreatedDate", "DescriptionEn", "NameEn" },
                values: new object[] { new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(240), null, null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 7,
                columns: new[] { "CreatedDate", "DescriptionEn", "NameEn" },
                values: new object[] { new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(261), null, null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 8,
                columns: new[] { "CreatedDate", "DescriptionEn", "NameEn" },
                values: new object[] { new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(263), null, null });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 9,
                columns: new[] { "CreatedDate", "DescriptionEn", "NameEn" },
                values: new object[] { new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(303), null, null });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "DescriptionEn", "NameEn" },
                values: new object[] { new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(8234), null, null });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "DescriptionEn", "NameEn" },
                values: new object[] { new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(8538), null, null });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "DescriptionEn", "NameEn" },
                values: new object[] { new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(8542), null, null });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 4,
                columns: new[] { "CreatedDate", "DescriptionEn", "NameEn" },
                values: new object[] { new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(8544), null, null });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 5,
                columns: new[] { "CreatedDate", "DescriptionEn", "NameEn" },
                values: new object[] { new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(8546), null, null });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 6,
                columns: new[] { "CreatedDate", "DescriptionEn", "NameEn" },
                values: new object[] { new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(8548), null, null });

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(4396));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(4685));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(4688));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(4690));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(4692));

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_RoomId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "ContentEn",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "SummaryEn",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "TitleEn",
                table: "BlogPosts");

            migrationBuilder.UpdateData(
                table: "Abouts",
                keyColumn: "AboutId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 234, DateTimeKind.Local).AddTicks(9498));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(2419));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(2740));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(2742));

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "ContactId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(3083));

            migrationBuilder.UpdateData(
                table: "HomePages",
                keyColumn: "HomePageId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 234, DateTimeKind.Local).AddTicks(444));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(5955));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(6254));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(6256));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(6259));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(6261));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(9440));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(9759));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(9761));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(9787));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(9790));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(9793));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(9795));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(9797));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 235, DateTimeKind.Local).AddTicks(9799));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(9230));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(9804));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(9808));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(9811));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(9814));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(9816));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(5211));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(5496));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(5499));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(5501));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 7, 0, 24, 35, 236, DateTimeKind.Local).AddTicks(5503));
        }
    }
}
