using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HarborRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class AddEnglishFieldsToAboutAndContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressEn",
                table: "Contacts",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkingHoursEn",
                table: "Contacts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                table: "Abouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubtitleEn",
                table: "Abouts",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleEn",
                table: "Abouts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Abouts",
                keyColumn: "AboutId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "DescriptionEn", "SubtitleEn", "TitleEn" },
                values: new object[] { new DateTime(2025, 8, 13, 4, 50, 37, 931, DateTimeKind.Local).AddTicks(1826), null, null, null });

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
                columns: new[] { "AddressEn", "CreatedDate", "WorkingHoursEn" },
                values: new object[] { null, new DateTime(2025, 8, 13, 4, 50, 37, 931, DateTimeKind.Local).AddTicks(8175), null });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressEn",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "WorkingHoursEn",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "SubtitleEn",
                table: "Abouts");

            migrationBuilder.DropColumn(
                name: "TitleEn",
                table: "Abouts");

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
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(80));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(232));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(234));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(236));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(238));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(240));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 7,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(261));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 8,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(263));

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "ItemId",
                keyValue: 9,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(303));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(8234));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(8538));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(8542));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(8544));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(8546));

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "RoomId",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 8, 13, 4, 7, 47, 463, DateTimeKind.Local).AddTicks(8548));

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
        }
    }
}
