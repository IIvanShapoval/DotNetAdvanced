using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Persistance.Migrations
{
    public partial class Initial_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Poducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Poducts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Image", "LastModifiedBy", "LastModifiedDate", "Name", "ParentCategoryId" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/electronics.jpg", null, null, "Electronics", null });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Image", "LastModifiedBy", "LastModifiedDate", "Name", "ParentCategoryId" },
                values: new object[] { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/computers.jpg", null, null, "Computers", 1 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Image", "LastModifiedBy", "LastModifiedDate", "Name", "ParentCategoryId" },
                values: new object[] { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/smartphones.jpg", null, null, "Smartphones", 1 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Image", "LastModifiedBy", "LastModifiedDate", "Name", "ParentCategoryId" },
                values: new object[] { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://example.com/laptops.jpg", null, null, "Laptops", 2 });

            migrationBuilder.InsertData(
                table: "Poducts",
                columns: new[] { "Id", "Amount", "CategoryId", "CreatedBy", "CreatedDate", "Description", "Image", "LastModifiedBy", "LastModifiedDate", "Name", "Price" },
                values: new object[] { 1, 100, 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The latest and greatest iPhone.", "https://example.com/iphone13.jpg", null, null, "iPhone 13", 999.99m });

            migrationBuilder.InsertData(
                table: "Poducts",
                columns: new[] { "Id", "Amount", "CategoryId", "CreatedBy", "CreatedDate", "Description", "Image", "LastModifiedBy", "LastModifiedDate", "Name", "Price" },
                values: new object[] { 2, 50, 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A powerful and versatile laptop.", "https://example.com/macbookpro.jpg", null, null, "MacBook Pro", 1999.99m });

            migrationBuilder.InsertData(
                table: "Poducts",
                columns: new[] { "Id", "Amount", "CategoryId", "CreatedBy", "CreatedDate", "Description", "Image", "LastModifiedBy", "LastModifiedDate", "Name", "Price" },
                values: new object[] { 3, 30, 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A powerful gaming laptop.", "https://example.com/macbookpro.jpg", null, null, "Asus ROG Strix", 1999.99m });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Poducts_CategoryId",
                table: "Poducts",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Poducts");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
