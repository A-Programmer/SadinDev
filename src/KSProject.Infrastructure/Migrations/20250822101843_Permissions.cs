using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KSProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Permissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUsers");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Users",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<bool>(
                name: "SuperAdmin",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLoginDates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    LoginDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "UserSecurityStamps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "UsersProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutMe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersRoles",
                columns: table => new
                {
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRoles", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UsersRoles_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersRoles_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "RolesPermissions",
                columns: table => new
                {
                    PermissionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesPermissions", x => new { x.PermissionsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Permissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolesPermissions_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersPermissions",
                columns: table => new
                {
                    PermissionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersPermissions", x => new { x.PermissionsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UsersPermissions_Permissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersPermissions_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name", "Title", "Version" },
                values: new object[,]
                {
                    { new Guid("018fd5fd-2b38-469e-87b1-5a08ed9a7e74"), "ViewPagedUsers", "نمایش لیست کاربران به صورت صفحه بندی", 0 },
                    { new Guid("08303878-6efb-4878-9f1f-c7c891e2a61c"), "DeletePermission", "حذف دسترسی", 0 },
                    { new Guid("1011ed50-08a7-4983-a3b9-7513e3dfbd3e"), "ViewUsers", "نمایش تمام لیست کاربران", 0 },
                    { new Guid("1920ad7b-9916-42e1-9d83-af5cc213a722"), "UpdateUser", "ویرایش کاربر", 0 },
                    { new Guid("1a6975cf-84a6-4cf8-9dcc-5344392141ff"), "UpdatePermission", "ویرایش دسترسی", 0 },
                    { new Guid("2b9bb0cc-c022-4ee8-8213-3c4235497d10"), "ViewPageRoles", "نمایش نقش ها به صورت صفحه بندی", 0 },
                    { new Guid("317632a4-2dc2-4465-a817-874c47d3fa16"), "DeleteRole", "حذف نقش", 0 },
                    { new Guid("32bd2686-c356-445c-969e-20ba1b5be265"), "AddUser", "افزودن کاربر", 0 },
                    { new Guid("44c01901-bb14-4c96-8db9-7feef8e3c9d9"), "ViewUserPermissions", "نمایش دسترسی های یک کاربر", 0 },
                    { new Guid("4b6b2561-44f6-4b7b-a1a0-85f9df29445d"), "AddPermission", "افزودن دسترسی جدید", 0 },
                    { new Guid("4f73803c-a22f-46d9-8f0c-fcba00c700e6"), "UpdateTestAggregate", "ویرایش موجودیت تستی", 0 },
                    { new Guid("5c8de111-0623-4f74-bad7-13e22ab84945"), "AddRole", "افزودن نقش جدید", 0 },
                    { new Guid("6d7d8938-1e8f-4676-bbe7-265282ad3a5f"), "ViewUser", "نمایش جزییات یک کاربر", 0 },
                    { new Guid("6ea6ddae-c013-4a24-8834-5aad69c6b564"), "ViewRolePermissions", "نمایش دسترسی های یک نقش", 0 },
                    { new Guid("769f69a8-f457-4337-a319-4e05631f641e"), "ViewRoles", "نمایش نقش ها", 0 },
                    { new Guid("7e2c20a0-32e1-4fef-ac87-aeb06a4a8e04"), "UpdateRolePermissions", "ویرایش دسترسی های یک نقش", 0 },
                    { new Guid("8eae9c79-c301-4717-9e4f-e9a921b640c0"), "DeleteTestAggregate", "حذف موجودیت تستس", 0 },
                    { new Guid("93532db2-1a40-42a3-8689-8851964a9ecf"), "ViewPagePermissions", "نمایش دسترسی ها به صورت صفحه بندی", 0 },
                    { new Guid("b12e7de8-5e95-4636-a0f3-323921b15640"), "UpdateUserPermissions", "ویرایش دسترسی های یک کاربر", 0 },
                    { new Guid("ccd14a65-90fc-45bf-86bd-36815d2aea4d"), "ViewPermissions", "نمایش دسترسی ها", 0 },
                    { new Guid("decd3cdf-9119-4522-9f54-969e2e5a2df6"), "DeleteUser", "حذف کاربر", 0 },
                    { new Guid("e2201ba3-5ae5-4947-b245-234cb1b8355c"), "ViewRole", "نمایش جزییات نقش", 0 },
                    { new Guid("eb737340-dc3e-49a6-8174-9360254372ea"), "ViewPermission", "نمایش جزییات دسترسی", 0 },
                    { new Guid("f7208df0-95b9-48ab-b0e9-bb4d1c6671b5"), "UpdateRole", "ویرایش نقش", 0 },
                    { new Guid("fc911fd3-8d58-4061-8307-8b0b1dc26bbb"), "AddTestAggregate", "افزودن موجودیت تستی جدید", 0 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name", "Version" },
                values: new object[,]
                {
                    { new Guid("1fd5d547-737a-45d3-b71f-c5e8f692d434"), "Standard user role with limited permissions.", "User", 0 },
                    { new Guid("98f4f7df-15bb-4547-8495-f098a753536f"), "Administrator role with all permissions.", "Admin", 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "Email", "HashedPassword", "PhoneNumber", "SuperAdmin", "UserName", "UserProfileId", "Version" },
                values: new object[] { new Guid("551de0bd-f8bf-4fa4-9523-f19b7c6dd95b"), true, "superadmin@superadmin.com", "01cVBRfT5lroSYX3twWtmf3Dg3KiLs6gzsr4qvggokk=", "09123456780", true, "superadmin", null, 0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "Email", "HashedPassword", "PhoneNumber", "UserName", "UserProfileId", "Version" },
                values: new object[,]
                {
                    { new Guid("5d2b2a64-0fa7-46af-bf1c-aadf1d7fb120"), true, "admin@admin.com", "01cVBRfT5lroSYX3twWtmf3Dg3KiLs6gzsr4qvggokk=", "09123456789", "admin", null, 0 },
                    { new Guid("9650f7f3-333b-4a77-b992-9a55179bfa12"), true, "user2@user.com", "vFhI8ifMFh619o3+mMsTEQqchDzmnpU6iBB9hlWD05c=", "09123456787", "user2", null, 0 },
                    { new Guid("c75e1cf0-84c0-4f9e-a608-e9a9b0e7d62f"), true, "user1@user.com", "vFhI8ifMFh619o3+mMsTEQqchDzmnpU6iBB9hlWD05c=", "09123456782", "user1", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "RolesPermissions",
                columns: new[] { "PermissionsId", "RolesId" },
                values: new object[,]
                {
                    { new Guid("018fd5fd-2b38-469e-87b1-5a08ed9a7e74"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("08303878-6efb-4878-9f1f-c7c891e2a61c"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("1011ed50-08a7-4983-a3b9-7513e3dfbd3e"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("1920ad7b-9916-42e1-9d83-af5cc213a722"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("1a6975cf-84a6-4cf8-9dcc-5344392141ff"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("2b9bb0cc-c022-4ee8-8213-3c4235497d10"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("317632a4-2dc2-4465-a817-874c47d3fa16"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("32bd2686-c356-445c-969e-20ba1b5be265"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("44c01901-bb14-4c96-8db9-7feef8e3c9d9"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("4b6b2561-44f6-4b7b-a1a0-85f9df29445d"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("5c8de111-0623-4f74-bad7-13e22ab84945"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("6d7d8938-1e8f-4676-bbe7-265282ad3a5f"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("6ea6ddae-c013-4a24-8834-5aad69c6b564"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("769f69a8-f457-4337-a319-4e05631f641e"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("7e2c20a0-32e1-4fef-ac87-aeb06a4a8e04"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("93532db2-1a40-42a3-8689-8851964a9ecf"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("b12e7de8-5e95-4636-a0f3-323921b15640"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("ccd14a65-90fc-45bf-86bd-36815d2aea4d"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("decd3cdf-9119-4522-9f54-969e2e5a2df6"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("e2201ba3-5ae5-4947-b245-234cb1b8355c"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("eb737340-dc3e-49a6-8174-9360254372ea"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") },
                    { new Guid("f7208df0-95b9-48ab-b0e9-bb4d1c6671b5"), new Guid("98f4f7df-15bb-4547-8495-f098a753536f") }
                });

            migrationBuilder.InsertData(
                table: "UsersPermissions",
                columns: new[] { "PermissionsId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("4f73803c-a22f-46d9-8f0c-fcba00c700e6"), new Guid("c75e1cf0-84c0-4f9e-a608-e9a9b0e7d62f") },
                    { new Guid("8eae9c79-c301-4717-9e4f-e9a921b640c0"), new Guid("c75e1cf0-84c0-4f9e-a608-e9a9b0e7d62f") },
                    { new Guid("fc911fd3-8d58-4061-8307-8b0b1dc26bbb"), new Guid("c75e1cf0-84c0-4f9e-a608-e9a9b0e7d62f") }
                });

            migrationBuilder.InsertData(
                table: "UsersRoles",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("1fd5d547-737a-45d3-b71f-c5e8f692d434"), new Guid("551de0bd-f8bf-4fa4-9523-f19b7c6dd95b") },
                    { new Guid("1fd5d547-737a-45d3-b71f-c5e8f692d434"), new Guid("9650f7f3-333b-4a77-b992-9a55179bfa12") },
                    { new Guid("1fd5d547-737a-45d3-b71f-c5e8f692d434"), new Guid("c75e1cf0-84c0-4f9e-a608-e9a9b0e7d62f") },
                    { new Guid("98f4f7df-15bb-4547-8495-f098a753536f"), new Guid("5d2b2a64-0fa7-46af-bf1c-aadf1d7fb120") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolesPermissions_RolesId",
                table: "RolesPermissions",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLoginDates_UserId",
                table: "UserLoginDates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSecurityStamps_UserId",
                table: "UserSecurityStamps",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersPermissions_UsersId",
                table: "UsersPermissions",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersProfiles_UserId",
                table: "UsersProfiles",
                column: "UserId",
                unique: true);

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
                name: "RolesPermissions");

            migrationBuilder.DropTable(
                name: "UserLoginDates");

            migrationBuilder.DropTable(
                name: "UserSecurityStamps");

            migrationBuilder.DropTable(
                name: "UsersPermissions");

            migrationBuilder.DropTable(
                name: "UsersProfiles");

            migrationBuilder.DropTable(
                name: "UsersRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1fd5d547-737a-45d3-b71f-c5e8f692d434"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("98f4f7df-15bb-4547-8495-f098a753536f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("551de0bd-f8bf-4fa4-9523-f19b7c6dd95b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5d2b2a64-0fa7-46af-bf1c-aadf1d7fb120"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9650f7f3-333b-4a77-b992-9a55179bfa12"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c75e1cf0-84c0-4f9e-a608-e9a9b0e7d62f"));

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SuperAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserProfileId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "RoleUsers",
                columns: table => new
                {
                    RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUsers", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUsers_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleUsers_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUsers_UsersId",
                table: "RoleUsers",
                column: "UsersId");
        }
    }
}
