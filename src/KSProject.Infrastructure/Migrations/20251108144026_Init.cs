using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KSProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    OccuredOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProcessedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestAggregates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAggregates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    HashedPassword = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    SuperAdmin = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserProfileId = table.Column<Guid>(type: "uuid", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    TestAggregateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestEntities_TestAggregates_TestAggregateId",
                        column: x => x.TestAggregateId,
                        principalTable: "TestAggregates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLoginDates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IpAddress = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginDates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLoginDates_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "text", nullable: false),
                    AboutMe = table.Column<string>(type: "text", nullable: false),
                    BirthDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserSecurityStamps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SecurityStamp = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSecurityStamps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSecurityStamps_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersRoles",
                columns: table => new
                {
                    RolesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRoles", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UsersRoles_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersRoles_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    ExpirationDateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedOnUtc", "Description", "IsDeleted", "ModifiedAt", "ModifiedBy", "Name", "Version" },
                values: new object[,]
                {
                    { new Guid("1fd5d547-737a-45d3-b71f-c5e8f692d434"), new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", null, "Standard user role with limited permissions.", false, new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", "User", 0 },
                    { new Guid("3fd5d547-737a-45d3-b71f-c5e8f692d434"), new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", null, "Test Role to test soft delete", false, new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", "TestRole", 0 },
                    { new Guid("98f4f7df-15bb-4547-8495-f098a753536f"), new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", null, "Administrator role with all permissions.", false, new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", "Admin", 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreatedAt", "CreatedBy", "DeletedOnUtc", "Email", "HashedPassword", "ModifiedAt", "ModifiedBy", "PhoneNumber", "UserName", "UserProfileId", "Version" },
                values: new object[] { new Guid("2fd5d547-737a-45d3-b71f-c5e8f692d434"), true, new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", null, "test@user.com", "vFhI8ifMFh619o3+mMsTEQqchDzmnpU6iBB9hlWD05c=", new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", "09123456783", "test", null, 0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreatedAt", "CreatedBy", "DeletedOnUtc", "Email", "HashedPassword", "ModifiedAt", "ModifiedBy", "PhoneNumber", "SuperAdmin", "UserName", "UserProfileId", "Version" },
                values: new object[] { new Guid("551de0bd-f8bf-4fa4-9523-f19b7c6dd95b"), true, new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", null, "superadmin@superadmin.com", "01cVBRfT5lroSYX3twWtmf3Dg3KiLs6gzsr4qvggokk=", new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", "09123456780", true, "superadmin", null, 0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreatedAt", "CreatedBy", "DeletedOnUtc", "Email", "HashedPassword", "ModifiedAt", "ModifiedBy", "PhoneNumber", "UserName", "UserProfileId", "Version" },
                values: new object[,]
                {
                    { new Guid("5d2b2a64-0fa7-46af-bf1c-aadf1d7fb120"), true, new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", null, "admin@admin.com", "PrP+ZrMeO00Q+nC1ytSccRIpSvauTkdqHEBRVdRaoSE=", new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", "09123456789", "admin", null, 0 },
                    { new Guid("9650f7f3-333b-4a77-b992-9a55179bfa12"), true, new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", null, "user2@user.com", "vFhI8ifMFh619o3+mMsTEQqchDzmnpU6iBB9hlWD05c=", new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", "09123456787", "user2", null, 0 },
                    { new Guid("c75e1cf0-84c0-4f9e-a608-e9a9b0e7d62f"), true, new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", null, "user1@user.com", "vFhI8ifMFh619o3+mMsTEQqchDzmnpU6iBB9hlWD05c=", new DateTimeOffset(new DateTime(2025, 11, 6, 3, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 30, 0, 0)), "System", "09123456782", "user1", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "UsersRoles",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("1fd5d547-737a-45d3-b71f-c5e8f692d434"), new Guid("551de0bd-f8bf-4fa4-9523-f19b7c6dd95b") },
                    { new Guid("1fd5d547-737a-45d3-b71f-c5e8f692d434"), new Guid("9650f7f3-333b-4a77-b992-9a55179bfa12") },
                    { new Guid("1fd5d547-737a-45d3-b71f-c5e8f692d434"), new Guid("c75e1cf0-84c0-4f9e-a608-e9a9b0e7d62f") },
                    { new Guid("3fd5d547-737a-45d3-b71f-c5e8f692d434"), new Guid("2fd5d547-737a-45d3-b71f-c5e8f692d434") },
                    { new Guid("98f4f7df-15bb-4547-8495-f098a753536f"), new Guid("5d2b2a64-0fa7-46af-bf1c-aadf1d7fb120") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TestEntities_TestAggregateId",
                table: "TestEntities",
                column: "TestAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginDates_UserId",
                table: "UserLoginDates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_UserId",
                table: "UserPermissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSecurityStamps_UserId",
                table: "UserSecurityStamps",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_UsersId",
                table: "UsersRoles",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTokens_UserId",
                table: "UserTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutboxMessages");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "TestEntities");

            migrationBuilder.DropTable(
                name: "UserLoginDates");

            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "UserSecurityStamps");

            migrationBuilder.DropTable(
                name: "UsersRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "TestAggregates");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
