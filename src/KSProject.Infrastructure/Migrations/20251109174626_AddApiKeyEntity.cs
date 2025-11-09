using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KSProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddApiKeyEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiKeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Scopes = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiKeys_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApiKeys",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedOnUtc", "ExpirationDate", "IsActive", "IsDeleted", "Key", "ModifiedAt", "ModifiedBy", "Scopes", "UserId", "Version" },
                values: new object[,]
                {
                    { new Guid("0acc9f75-9201-4ea5-9a16-5be1c30d6f60"), new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", null, new DateTimeOffset(new DateTime(2026, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), true, false, "0acc9f7592014ea59a165be1c30d6f60", new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", "sliders.create,sliders.show-all,sliders.update,users.show-all,users.create,users.update,users.delete", new Guid("5d2b2a64-0fa7-46af-bf1c-aadf1d7fb120"), 0 },
                    { new Guid("17f9e83c-b763-4e38-8902-1d0583adab05"), new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", null, new DateTimeOffset(new DateTime(2026, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), true, false, "17f9e83cb7634e3889021d0583adab05", new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", "sliders.create,sliders.show-all", new Guid("c75e1cf0-84c0-4f9e-a608-e9a9b0e7d62f"), 0 },
                    { new Guid("2a5018f6-c8db-490a-9707-221469d20bb7"), new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", null, new DateTimeOffset(new DateTime(2026, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), true, false, "2a5018f6c8db490a9707221469d20bb7", new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", "sliders.create,sliders.show-all", new Guid("2fd5d547-737a-45d3-b71f-c5e8f692d434"), 0 },
                    { new Guid("c55fb374-3d74-4aa3-b576-d144c49cd184"), new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", null, new DateTimeOffset(new DateTime(2026, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), true, false, "c55fb3743d744aa3b576d144c49cd184", new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", "sliders.create,sliders.show-all,sliders.update,users.show-all,users.create,users.update,users.delete", new Guid("551de0bd-f8bf-4fa4-9523-f19b7c6dd95b"), 0 },
                    { new Guid("ed12b679-8fd0-4a0c-ade5-fa6aaccf42fd"), new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", null, new DateTimeOffset(new DateTime(2026, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), true, false, "ed12b6798fd04a0cade5fa6aaccf42fd", new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", "sliders.show-all", new Guid("c75e1cf0-84c0-4f9e-a608-e9a9b0e7d62f"), 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiKeys_UserId",
                table: "ApiKeys",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiKeys");
        }
    }
}
