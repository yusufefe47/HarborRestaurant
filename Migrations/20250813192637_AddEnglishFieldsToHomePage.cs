using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HarborRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class AddEnglishFieldsToHomePage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ButtonTextEn",
                table: "HomePages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                table: "HomePages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainTitleEn",
                table: "HomePages",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubtitleEn",
                table: "HomePages",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Abouts",
                keyColumn: "AboutId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 899, DateTimeKind.Local).AddTicks(6575));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(7141));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(7353));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(7355));

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "ContactId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 899, DateTimeKind.Local).AddTicks(9190));

            migrationBuilder.UpdateData(
                table: "HomePages",
                keyColumn: "HomePageId",
                keyValue: 1,
                columns: new[] { "ButtonTextEn", "CreatedDate", "DescriptionEn", "MainTitleEn", "SubtitleEn" },
                values: new object[] { null, new DateTime(2025, 8, 13, 22, 26, 36, 898, DateTimeKind.Local).AddTicks(8087), null, null, null });

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(1596));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(1822));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(1825));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(1827));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(1829));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(4456));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(4693));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(4696));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(4699));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(4702));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(4704));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(4743));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(4747));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(4749));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 901, DateTimeKind.Local).AddTicks(6415));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 901, DateTimeKind.Local).AddTicks(6823));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 901, DateTimeKind.Local).AddTicks(6826));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 901, DateTimeKind.Local).AddTicks(6832));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 901, DateTimeKind.Local).AddTicks(6836));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 901, DateTimeKind.Local).AddTicks(6838));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(9380));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(9611));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(9613));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(9615));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 22, 26, 36, 900, DateTimeKind.Local).AddTicks(9621));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ButtonTextEn",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "MainTitleEn",
                table: "HomePages");

            migrationBuilder.DropColumn(
                name: "SubtitleEn",
                table: "HomePages");

            migrationBuilder.UpdateData(
                table: "Abouts",
                keyColumn: "AboutId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 931, DateTimeKind.Local).AddTicks(1826));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 932, DateTimeKind.Local).AddTicks(9941));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 933, DateTimeKind.Local).AddTicks(131));

            migrationBuilder.UpdateData(
                table: "BlogCategories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 933, DateTimeKind.Local).AddTicks(133));

            migrationBuilder.UpdateData(
                table: "Contacts",
                keyColumn: "ContactId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 931, DateTimeKind.Local).AddTicks(8175));

            migrationBuilder.UpdateData(
                table: "HomePages",
                keyColumn: "HomePageId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 930, DateTimeKind.Local).AddTicks(1574));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 932, DateTimeKind.Local).AddTicks(1281));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 932, DateTimeKind.Local).AddTicks(1626));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 932, DateTimeKind.Local).AddTicks(1629));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 932, DateTimeKind.Local).AddTicks(1631));

            migrationBuilder.UpdateData(
                table: "MenuCategories",
                keyColumn: "CategoryId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 932, DateTimeKind.Local).AddTicks(1633));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 932, DateTimeKind.Local).AddTicks(7121));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 932, DateTimeKind.Local).AddTicks(7386));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 932, DateTimeKind.Local).AddTicks(7389));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 932, DateTimeKind.Local).AddTicks(7392));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 932, DateTimeKind.Local).AddTicks(7394));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 932, DateTimeKind.Local).AddTicks(7397));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 932, DateTimeKind.Local).AddTicks(7399));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 932, DateTimeKind.Local).AddTicks(7401));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 932, DateTimeKind.Local).AddTicks(7403));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 933, DateTimeKind.Local).AddTicks(6229));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 933, DateTimeKind.Local).AddTicks(6600));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 933, DateTimeKind.Local).AddTicks(6603));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 933, DateTimeKind.Local).AddTicks(6605));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 933, DateTimeKind.Local).AddTicks(6608));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 933, DateTimeKind.Local).AddTicks(6610));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 933, DateTimeKind.Local).AddTicks(1978));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 933, DateTimeKind.Local).AddTicks(2165));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 933, DateTimeKind.Local).AddTicks(2167));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 933, DateTimeKind.Local).AddTicks(2169));

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "TableId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 50, 37, 933, DateTimeKind.Local).AddTicks(2171));
        }
    }
}
