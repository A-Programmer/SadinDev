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
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServiceType = table.Column<string>(type: "text", nullable: false),
                    MetricType = table.Column<string>(type: "text", nullable: false),
                    Variant = table.Column<string>(type: "text", nullable: false),
                    RatePerUnit = table.Column<decimal>(type: "numeric", nullable: false),
                    RulesJson = table.Column<string>(type: "jsonb", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestAggregates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAggregates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
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
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
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
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    ServiceType = table.Column<string>(type: "text", nullable: false),
                    MetricType = table.Column<string>(type: "text", nullable: false),
                    MetricValue = table.Column<decimal>(type: "numeric", nullable: false),
                    TransactionDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MetricDetails = table.Column<string>(type: "text", nullable: true),
                    WalletId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    WalletId = table.Column<Guid>(type: "uuid", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiKeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Scopes = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "UserLoginDates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
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
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                    ExpirationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                    { new Guid("1fd5d547-737a-45d3-b71f-c5e8f692d434"), new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, "Standard user role with limited permissions.", false, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "User", 0 },
                    { new Guid("3fd5d547-737a-45d3-b71f-c5e8f692d434"), new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, "Test Role to test soft delete", false, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "TestRole", 0 },
                    { new Guid("98f4f7df-15bb-4547-8495-f098a753536f"), new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, "Administrator role with all permissions.", false, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "Admin", 0 }
                });

            migrationBuilder.InsertData(
                table: "ServiceRates",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedOnUtc", "MetricType", "ModifiedAt", "ModifiedBy", "RatePerUnit", "RulesJson", "ServiceType", "Variant", "Version" },
                values: new object[,]
                {
                    { new Guid("11111111-2222-3333-4444-555555555555"), new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, "Posts_Count", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", 0.01m, null, "Blog", "Default", 0 },
                    { new Guid("22222222-3333-4444-5555-666666666666"), new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, "Posts_Count", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", 0.005m, "{\"minQuantity\": 50, \"discountPercent\": 10}", "Blog", "Premium", 0 },
                    { new Guid("33333333-4444-5555-6666-777777777777"), new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, "SMS_Count", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", 0.02m, null, "Notification", "Default", 0 },
                    { new Guid("44444444-5555-6666-7777-888888888888"), new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, "Transactions_Count", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", 0.015m, "{\"minQuantity\": 100, \"discountPercent\": 15}", "OnlineStore", "Tier1", 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreatedAt", "CreatedBy", "DeletedOnUtc", "Email", "HashedPassword", "ModifiedAt", "ModifiedBy", "PhoneNumber", "UserName", "UserProfileId", "Version", "WalletId" },
                values: new object[] { new Guid("2fd5d547-737a-45d3-b71f-c5e8f692d434"), true, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, "test@user.com", "vFhI8ifMFh619o3+mMsTEQqchDzmnpU6iBB9hlWD05c=", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "09123456783", "test", null, 0, null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreatedAt", "CreatedBy", "DeletedOnUtc", "Email", "HashedPassword", "ModifiedAt", "ModifiedBy", "PhoneNumber", "SuperAdmin", "UserName", "UserProfileId", "Version", "WalletId" },
                values: new object[] { new Guid("551de0bd-f8bf-4fa4-9523-f19b7c6dd95b"), true, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, "superadmin@superadmin.com", "01cVBRfT5lroSYX3twWtmf3Dg3KiLs6gzsr4qvggokk=", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "09123456780", true, "superadmin", null, 0, null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreatedAt", "CreatedBy", "DeletedOnUtc", "Email", "HashedPassword", "ModifiedAt", "ModifiedBy", "PhoneNumber", "UserName", "UserProfileId", "Version", "WalletId" },
                values: new object[,]
                {
                    { new Guid("5d2b2a64-0fa7-46af-bf1c-aadf1d7fb120"), true, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, "admin@admin.com", "PrP+ZrMeO00Q+nC1ytSccRIpSvauTkdqHEBRVdRaoSE=", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "09123456789", "admin", null, 0, null },
                    { new Guid("9650f7f3-333b-4a77-b992-9a55179bfa12"), true, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, "user2@user.com", "vFhI8ifMFh619o3+mMsTEQqchDzmnpU6iBB9hlWD05c=", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "09123456787", "user2", null, 0, null },
                    { new Guid("c75e1cf0-84c0-4f9e-a608-e9a9b0e7d62f"), true, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, "user1@user.com", "vFhI8ifMFh619o3+mMsTEQqchDzmnpU6iBB9hlWD05c=", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "09123456782", "user1", null, 0, null }
                });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "CreatedAt", "CreatedBy", "DeletedOnUtc", "ModifiedAt", "ModifiedBy", "UserId", "Version" },
                values: new object[,]
                {
                    { new Guid("0acc9f75-9201-4ea5-9a16-5be1c30d6f60"), 50.0m, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("5d2b2a64-0fa7-46af-bf1c-aadf1d7fb120"), 0 },
                    { new Guid("17f9e83c-b763-4e38-8902-1d0583adab05"), 0.0m, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("9650f7f3-333b-4a77-b992-9a55179bfa12"), 0 },
                    { new Guid("2a5018f6-c8db-490a-9707-221469d20bb7"), 10.0m, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("2fd5d547-737a-45d3-b71f-c5e8f692d434"), 0 },
                    { new Guid("c55fb374-3d74-4aa3-b576-d144c49cd184"), 100.0m, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("551de0bd-f8bf-4fa4-9523-f19b7c6dd95b"), 0 },
                    { new Guid("ed12b679-8fd0-4a0c-ade5-fa6aaccf42fd"), 20.0m, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", new Guid("c75e1cf0-84c0-4f9e-a608-e9a9b0e7d62f"), 0 }
                });

            migrationBuilder.InsertData(
                table: "ApiKeys",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedOnUtc", "ExpirationDate", "IsActive", "IsDeleted", "Key", "ModifiedAt", "ModifiedBy", "Scopes", "UserId", "Version" },
                values: new object[,]
                {
                    { new Guid("0acc9f75-9201-4ea5-9a16-5be1c30d6f60"), new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, new DateTime(2026, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), true, false, "0acc9f7592014ea59a165be1c30d6f60", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "sliders.create,sliders.show-all,sliders.update,users.show-all,users.create,users.update,users.delete", new Guid("5d2b2a64-0fa7-46af-bf1c-aadf1d7fb120"), 0 },
                    { new Guid("17f9e83c-b763-4e38-8902-1d0583adab05"), new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, new DateTime(2026, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), true, false, "17f9e83cb7634e3889021d0583adab05", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "sliders.create,sliders.show-all", new Guid("c75e1cf0-84c0-4f9e-a608-e9a9b0e7d62f"), 0 },
                    { new Guid("2a5018f6-c8db-490a-9707-221469d20bb7"), new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, new DateTime(2026, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), true, false, "2a5018f6c8db490a9707221469d20bb7", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "sliders.create,sliders.show-all", new Guid("2fd5d547-737a-45d3-b71f-c5e8f692d434"), 0 },
                    { new Guid("c55fb374-3d74-4aa3-b576-d144c49cd184"), new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, new DateTime(2026, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), true, false, "c55fb3743d744aa3b576d144c49cd184", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "sliders.create,sliders.show-all,sliders.update,users.show-all,users.create,users.update,users.delete", new Guid("551de0bd-f8bf-4fa4-9523-f19b7c6dd95b"), 0 },
                    { new Guid("ed12b679-8fd0-4a0c-ade5-fa6aaccf42fd"), new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, new DateTime(2026, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), true, false, "ed12b6798fd04a0cade5fa6aaccf42fd", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "sliders.show-all", new Guid("c75e1cf0-84c0-4f9e-a608-e9a9b0e7d62f"), 0 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "CreatedAt", "CreatedBy", "DeletedOnUtc", "IsDeleted", "MetricDetails", "MetricType", "MetricValue", "ModifiedAt", "ModifiedBy", "ServiceType", "TransactionDateTime", "Type", "Version", "WalletId" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4789-abc1-def234567890"), 100.0m, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, false, null, "", 0m, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Charge", 0, new Guid("c55fb374-3d74-4aa3-b576-d144c49cd184") },
                    { new Guid("b2c3d4e5-f678-9abc-1def-234567890abc"), -5.0m, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, false, null, "Posts_Count", 5.0m, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "Blog", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Usage", 0, new Guid("c55fb374-3d74-4aa3-b576-d144c49cd184") },
                    { new Guid("c3d4e5f6-789a-bc1d-ef23-4567890abcde"), 50.0m, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, false, null, "", 0m, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Charge", 0, new Guid("0acc9f75-9201-4ea5-9a16-5be1c30d6f60") },
                    { new Guid("d4e5f678-9abc-1def-2345-67890abcde12"), -2.0m, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, false, null, "SMS_Count", 10.0m, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "Notification", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Usage", 0, new Guid("ed12b679-8fd0-4a0c-ade5-fa6aaccf42fd") },
                    { new Guid("e5f6789a-bc1d-ef23-4567-890abcde1234"), 10.0m, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, false, null, "Transactions_Count", 1.0m, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "OnlineStore", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Refund", 0, new Guid("17f9e83c-b763-4e38-8902-1d0583adab05") },
                    { new Guid("f6789abc-1def-2345-6789-0abcde123456"), -1.0m, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, false, null, "", 0m, new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Adjustment", 0, new Guid("2a5018f6-c8db-490a-9707-221469d20bb7") }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "AboutMe", "BirthDate", "CreatedAt", "CreatedBy", "DeletedOnUtc", "FirstName", "IsDeleted", "LastName", "ModifiedAt", "ModifiedBy", "ProfileImageUrl", "UserId", "Version" },
                values: new object[,]
                {
                    { new Guid("29a0421c-6e4e-4793-bf3d-aad975155381"), "This is User Two Profile", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, "User", false, "Two", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "/image.png", new Guid("9650f7f3-333b-4a77-b992-9a55179bfa12"), 0 },
                    { new Guid("445819eb-053a-4c13-b8dd-fb736d46739f"), "This is User Test Profile", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, "Test", false, "User", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "/image.png", new Guid("2fd5d547-737a-45d3-b71f-c5e8f692d434"), 0 },
                    { new Guid("5e46e00a-5162-4417-a240-36dc48793ad5"), "This is Admin Profile", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, "Admin", false, "User", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "/image.png", new Guid("5d2b2a64-0fa7-46af-bf1c-aadf1d7fb120"), 0 },
                    { new Guid("b21013eb-7182-46ef-b543-b9606bc45c83"), "This is User One Profile", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, "User", false, "One", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "/image.png", new Guid("c75e1cf0-84c0-4f9e-a608-e9a9b0e7d62f"), 0 },
                    { new Guid("ec7a3150-c202-4895-8b00-232f28e0eb4f"), "This is SuperAdmin Profile", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", null, "Super", false, "Admin", new DateTime(2025, 11, 12, 10, 0, 0, 0, DateTimeKind.Utc), "System", "/image.png", new Guid("551de0bd-f8bf-4fa4-9523-f19b7c6dd95b"), 0 }
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
                name: "IX_ApiKeys_IsDeleted",
                table: "ApiKeys",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ApiKeys_UserId",
                table: "ApiKeys",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_IsDeleted",
                table: "Roles",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRates_IsDeleted",
                table: "ServiceRates",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRates_ServiceType_MetricType_Variant",
                table: "ServiceRates",
                columns: new[] { "ServiceType", "MetricType", "Variant" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestEntities_TestAggregateId",
                table: "TestEntities",
                column: "TestAggregateId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_IsDeleted",
                table: "Transactions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginDates_UserId",
                table: "UserLoginDates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_UserId",
                table: "UserPermissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_IsDeleted",
                table: "UserProfiles",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsDeleted",
                table: "Users",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Users_WalletId",
                table: "Users",
                column: "WalletId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_IsDeleted",
                table: "Wallets",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiKeys");

            migrationBuilder.DropTable(
                name: "OutboxMessages");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "ServiceRates");

            migrationBuilder.DropTable(
                name: "TestEntities");

            migrationBuilder.DropTable(
                name: "Transactions");

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

            migrationBuilder.DropTable(
                name: "Wallets");
        }
    }
}
