using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementPortal.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookAuthor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BorrowRequests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestStatus = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestedByUserId = table.Column<int>(type: "int", nullable: false),
                    ProcessedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowRequests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_BorrowRequests_Users_ProcessedByUserId",
                        column: x => x.ProcessedByUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BorrowRequests_Users_RequestedByUserId",
                        column: x => x.RequestedByUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BorrowRequestDetails",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    BorrowRequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowRequestDetails", x => new { x.BorrowRequestId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BorrowRequestDetails_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BorrowRequestDetails_BorrowRequests_BorrowRequestId",
                        column: x => x.BorrowRequestId,
                        principalTable: "BorrowRequests",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryDescription", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Books about Philosophy", "Philosophy" },
                    { 2, "Books containing a collection of jokes", "Humor" },
                    { 3, "Books for teaching", "Education" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "FirstName", "LastName", "Password", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, "Admin", "Istrator", "cGFzc3dvcmQ=", 1, "admin" },
                    { 2, "Do", "Trung Anh", "dHJ1bmdhbmhkbw==", 0, "trunganhdo" },
                    { 3, "User", "Anon", "dXNlcg==", 0, "user" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "BookAuthor", "BookName", "CategoryId" },
                values: new object[,]
                {
                    { 1, "Someone", "A Book (Vol.1)", 1 },
                    { 2, "Another one", "Another Book (Vol.1)", 2 },
                    { 3, "A Teacher", "B for Book", 3 },
                    { 4, "A Teacher", "A for Another Book", 3 },
                    { 5, "Another one", "Another Book (Vol.2)", 2 },
                    { 6, "Someone", "A Book (Vol.2)", 1 },
                    { 7, "Someone", "A Book (Vol.3)", 1 },
                    { 8, "Another one", "Another Book (Vol.3)", 2 },
                    { 9, "A Teacher", "C for Category of Books", 3 }
                });

            migrationBuilder.InsertData(
                table: "BorrowRequests",
                columns: new[] { "RequestId", "ProcessedByUserId", "RequestDate", "RequestStatus", "RequestedByUserId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 2 },
                    { 2, 1, new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 },
                    { 3, 1, new DateTime(2022, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "BorrowRequestDetails",
                columns: new[] { "BookId", "BorrowRequestId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 2 },
                    { 6, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRequestDetails_BookId",
                table: "BorrowRequestDetails",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRequests_ProcessedByUserId",
                table: "BorrowRequests",
                column: "ProcessedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowRequests_RequestedByUserId",
                table: "BorrowRequests",
                column: "RequestedByUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowRequestDetails");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "BorrowRequests");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
