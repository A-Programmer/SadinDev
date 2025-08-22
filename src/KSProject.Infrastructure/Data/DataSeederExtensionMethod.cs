using KSFramework.Utilities;
using KSProject.Domain.Aggregates.Permissions;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Aggregates.Users;
using Microsoft.EntityFrameworkCore;

namespace KSProject.Infrastructure.Data;

public static class DataSeederExtensionMethod
{
	#region Shared Data

	#region Users and Roles
	// Role Ids
	public static Guid AdminRoleId = Guid.Parse("98f4f7df-15bb-4547-8495-f098a753536f");
	public static Guid UserRoleId = Guid.Parse("1fd5d547-737a-45d3-b71f-c5e8f692d434");
	// User Ids
	public static Guid SuperAdminUserId = Guid.Parse("551de0bd-f8bf-4fa4-9523-f19b7c6dd95b");
	public static Guid AdminUserId = Guid.Parse("5d2b2a64-0fa7-46af-bf1c-aadf1d7fb120");
	public static Guid UserUserId1 = Guid.Parse("c75e1cf0-84c0-4f9e-a608-e9a9b0e7d62f");
	public static Guid UserUserId2 = Guid.Parse("9650f7f3-333b-4a77-b992-9a55179bfa12");
	// Hashed Passwords
	public static string AdminHashedPassword = SecurityHelper.GetSha256Hash("Admin123!");
	public static string SuperAdminHashedPassword = SecurityHelper.GetSha256Hash("SuperAdmin123!");
	public static string UserHashedPassword = SecurityHelper.GetSha256Hash("User123!");
	#endregion

	#region Permissions
	// Permission Ids
	public static Guid ViewRolesId = Guid.Parse("769f69a8-f457-4337-a319-4e05631f641e");
	public static Guid ViewPagedRolesId = Guid.Parse("2b9bb0cc-c022-4ee8-8213-3c4235497d10");
	public static Guid ViewRoleId = Guid.Parse("e2201ba3-5ae5-4947-b245-234cb1b8355c");
	public static Guid AddRoleId = Guid.Parse("5c8de111-0623-4f74-bad7-13e22ab84945");
	public static Guid UpdateRoleId = Guid.Parse("f7208df0-95b9-48ab-b0e9-bb4d1c6671b5");
	public static Guid DeleteRoleId = Guid.Parse("317632a4-2dc2-4465-a817-874c47d3fa16");

	public static Guid AddTestAggregateId = Guid.Parse("fc911fd3-8d58-4061-8307-8b0b1dc26bbb");
	public static Guid UpdateTestAggregateId = Guid.Parse("4f73803c-a22f-46d9-8f0c-fcba00c700e6");
	public static Guid DeleteTestAggregateId = Guid.Parse("8eae9c79-c301-4717-9e4f-e9a921b640c0");

	public static Guid ViewUsersId = Guid.Parse("1011ed50-08a7-4983-a3b9-7513e3dfbd3e");
	public static Guid ViewPagedUsersId = Guid.Parse("018fd5fd-2b38-469e-87b1-5a08ed9a7e74");
	public static Guid ViewUserId = Guid.Parse("6d7d8938-1e8f-4676-bbe7-265282ad3a5f");
	public static Guid UpdateUserId = Guid.Parse("1920ad7b-9916-42e1-9d83-af5cc213a722");
	public static Guid DeleteUserId = Guid.Parse("decd3cdf-9119-4522-9f54-969e2e5a2df6");
	public static Guid AddUserId = Guid.Parse("32bd2686-c356-445c-969e-20ba1b5be265");

	public static Guid ViewPermissionsId = Guid.Parse("ccd14a65-90fc-45bf-86bd-36815d2aea4d");
	public static Guid ViewPagedPermissionsId = Guid.Parse("93532db2-1a40-42a3-8689-8851964a9ecf");
	public static Guid ViewPermissionId = Guid.Parse("eb737340-dc3e-49a6-8174-9360254372ea");
	public static Guid UpdatePermissionId = Guid.Parse("1a6975cf-84a6-4cf8-9dcc-5344392141ff");
	public static Guid DeletePermissionId = Guid.Parse("08303878-6efb-4878-9f1f-c7c891e2a61c");
	public static Guid AddPermissionId = Guid.Parse("4b6b2561-44f6-4b7b-a1a0-85f9df29445d");

	public static Guid ViewRolePermissionsId = Guid.Parse("6ea6ddae-c013-4a24-8834-5aad69c6b564");
	public static Guid UpdateRolePermissionsId = Guid.Parse("7e2c20a0-32e1-4fef-ac87-aeb06a4a8e04");
	public static Guid ViewUserPermissionsId = Guid.Parse("44c01901-bb14-4c96-8db9-7feef8e3c9d9");
	public static Guid UpdateUserPermissionsId = Guid.Parse("b12e7de8-5e95-4636-a0f3-323921b15640");
	#endregion

	#endregion
	public static void SeedData(this ModelBuilder modelBuilder)
	{
		SeedRoles(modelBuilder);
		SeedUsers(modelBuilder);
		SeedPermissions(modelBuilder);

	}

	// Seeders
	private static void SeedRoles(this ModelBuilder modelBuilder)
	{
		Role adminRole = Role.Create(AdminRoleId, "Admin", "Administrator role with all permissions.");
		Role userRole = Role.Create(UserRoleId, "User", "Standard user role with limited permissions.");

		modelBuilder.Entity<Role>()
			.HasData(
				adminRole,
				userRole
			);
	}

	private static void SeedUsers(this ModelBuilder modelBuilder)
	{
		User superAdminUser = User.Create(SuperAdminUserId, "superadmin", SuperAdminHashedPassword, "superadmin@superadmin.com",
		"09123456780", true, true);

		User adminUser = User.Create(AdminUserId, "admin", AdminHashedPassword, "admin@admin.com",
		"09123456789", true, false);

		User userUser1 = User
			.Create(UserUserId1, "user1", UserHashedPassword, "user1@user.com", "09123456782", true, false);

		User userUser2 = User
			.Create(UserUserId2, "user2", UserHashedPassword, "user2@user.com", "09123456787", true, false);

		modelBuilder.Entity<User>().HasData(new List<User>()
		{
			superAdminUser,
			adminUser,
			userUser1,
			userUser2
		});

		modelBuilder.Entity<User>()
			.HasMany(u => u.Roles)
			.WithMany(r => r.Users)
			.UsingEntity<Dictionary<string, object>>(
				"UsersRoles",
				ur => ur.HasData(
					new { UsersId = SuperAdminUserId, RolesId = UserRoleId },
					new { UsersId = AdminUserId, RolesId = AdminRoleId },
					new { UsersId = UserUserId1, RolesId = UserRoleId },
					new { UsersId = UserUserId2, RolesId = UserRoleId }
				)
			);
	}

	private static void SeedPermissions(this ModelBuilder modelBuilder)
	{
		Permission viewPermissionsPermission = Permission.Create(ViewPermissionsId, "ViewPermissions", "نمایش دسترسی ها");
		Permission viewPagedPermissionsPermission = Permission.Create(ViewPagedPermissionsId, "ViewPagePermissions", "نمایش دسترسی ها به صورت صفحه بندی");
		Permission viewPermissionPermission = Permission.Create(ViewPermissionId, "ViewPermission", "نمایش جزییات دسترسی");
		Permission addPermissionPermission = Permission.Create(AddPermissionId, "AddPermission", "افزودن دسترسی جدید");
		Permission updatePermissionPermission = Permission.Create(UpdatePermissionId, "UpdatePermission", "ویرایش دسترسی");
		Permission deletePermissionPermission = Permission.Create(DeletePermissionId, "DeletePermission", "حذف دسترسی");

		Permission viewRolesPermission = Permission.Create(ViewRolesId, "ViewRoles", "نمایش نقش ها");
		Permission viewPagedRolesPermission = Permission.Create(ViewPagedRolesId, "ViewPageRoles", "نمایش نقش ها به صورت صفحه بندی");
		Permission viewRoleByIdPermission = Permission.Create(ViewRoleId, "ViewRole", "نمایش جزییات نقش");
		Permission addRolePermission = Permission.Create(AddRoleId, "AddRole", "افزودن نقش جدید");
		Permission updateRolePermission = Permission.Create(UpdateRoleId, "UpdateRole", "ویرایش نقش");
		Permission deleteRolePermission = Permission.Create(DeleteRoleId, "DeleteRole", "حذف نقش");

		Permission viewRolePermissionsPermission = Permission.Create(ViewRolePermissionsId, "ViewRolePermissions", "نمایش دسترسی های یک نقش");
		Permission updateRolePermissionsPermission = Permission.Create(UpdateRolePermissionsId, "UpdateRolePermissions", "ویرایش دسترسی های یک نقش");

		Permission addTestAggregatePermission = Permission.Create(AddTestAggregateId, "AddTestAggregate", "افزودن موجودیت تستی جدید");
		Permission updateTestAggregatePermission = Permission.Create(UpdateTestAggregateId, "UpdateTestAggregate", "ویرایش موجودیت تستی");
		Permission deleteTestAggregatePermission = Permission.Create(DeleteTestAggregateId, "DeleteTestAggregate", "حذف موجودیت تستس");

		Permission viewUsersPermission = Permission.Create(ViewUsersId, "ViewUsers", "نمایش تمام لیست کاربران");
		Permission viewPagedUsersPermission = Permission.Create(ViewPagedUsersId, "ViewPagedUsers", "نمایش لیست کاربران به صورت صفحه بندی");
		Permission viewUserByIdPermission = Permission.Create(ViewUserId, "ViewUser", "نمایش جزییات یک کاربر");
		Permission updateUserPermission = Permission.Create(UpdateUserId, "UpdateUser", "ویرایش کاربر");
		Permission deleteUserPermission = Permission.Create(DeleteUserId, "DeleteUser", "حذف کاربر");
		Permission addUserPermission = Permission.Create(AddUserId, "AddUser", "افزودن کاربر");

		Permission viewUserPermissionsPermission = Permission.Create(ViewUserPermissionsId, "ViewUserPermissions", "نمایش دسترسی های یک کاربر");
		Permission updateUserPermissionsPermission = Permission.Create(UpdateUserPermissionsId, "UpdateUserPermissions", "ویرایش دسترسی های یک کاربر");

		modelBuilder.Entity<Permission>().HasData(new List<Permission>()
		{
			viewRolesPermission,
			viewPagedRolesPermission,
			viewRoleByIdPermission,
			addRolePermission,
			updateRolePermission,
			deleteRolePermission,
			addTestAggregatePermission,
			updateTestAggregatePermission,
			deleteTestAggregatePermission,
			viewUsersPermission,
			viewPagedUsersPermission,
			viewUserByIdPermission,
			updateUserPermission,
			deleteUserPermission,
			addUserPermission,
			viewPermissionsPermission,
			viewPagedPermissionsPermission,
			viewPermissionPermission,
			addPermissionPermission,
			updatePermissionPermission,
			deletePermissionPermission,
			viewRolePermissionsPermission,
			updateRolePermissionsPermission,
			viewUserPermissionsPermission,
			updateUserPermissionsPermission
		});

		modelBuilder.Entity<User>()
			.HasMany(u => u.Permissions)
			.WithMany(r => r.Users)
			.UsingEntity<Dictionary<string, object>>(
				"UsersPermissions",
				ur => ur.HasData(
					new { UsersId = UserUserId1, PermissionsId = AddTestAggregateId },
					new { UsersId = UserUserId1, PermissionsId = UpdateTestAggregateId },
					new { UsersId = UserUserId1, PermissionsId = DeleteTestAggregateId }
				)
			);

		modelBuilder.Entity<Role>()
			.HasMany(u => u.Permissions)
			.WithMany(r => r.Roles)
			.UsingEntity<Dictionary<string, object>>(
				"RolesPermissions",
				ur => ur.HasData(
					new { RolesId = AdminRoleId, PermissionsId = ViewRolesId },
					new { RolesId = AdminRoleId, PermissionsId = ViewPagedRolesId },
					new { RolesId = AdminRoleId, PermissionsId = ViewRoleId },
					new { RolesId = AdminRoleId, PermissionsId = AddRoleId },
					new { RolesId = AdminRoleId, PermissionsId = DeleteRoleId },
					new { RolesId = AdminRoleId, PermissionsId = UpdateRoleId },

					new { RolesId = AdminRoleId, PermissionsId = ViewPermissionsId },
					new { RolesId = AdminRoleId, PermissionsId = ViewPagedPermissionsId },
					new { RolesId = AdminRoleId, PermissionsId = ViewPermissionId },
					new { RolesId = AdminRoleId, PermissionsId = AddPermissionId },
					new { RolesId = AdminRoleId, PermissionsId = DeletePermissionId },
					new { RolesId = AdminRoleId, PermissionsId = UpdatePermissionId },

					new { RolesId = AdminRoleId, PermissionsId = ViewRolePermissionsId },
					new { RolesId = AdminRoleId, PermissionsId = UpdateRolePermissionsId },
					new { RolesId = AdminRoleId, PermissionsId = ViewUserPermissionsId },
					new { RolesId = AdminRoleId, PermissionsId = UpdateUserPermissionsId },

					new { RolesId = AdminRoleId, PermissionsId = ViewUsersId },
					new { RolesId = AdminRoleId, PermissionsId = ViewPagedUsersId },
					new { RolesId = AdminRoleId, PermissionsId = ViewUserId },
					new { RolesId = AdminRoleId, PermissionsId = AddUserId },
					new { RolesId = AdminRoleId, PermissionsId = DeleteUserId },
					new { RolesId = AdminRoleId, PermissionsId = UpdateUserId }
				)
			);
	}
}
