using KSFramework.Utilities;
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
    public static Guid TestRoleId = Guid.Parse("3fd5d547-737a-45d3-b71f-c5e8f692d434");
	// User Ids
	public static Guid SuperAdminUserId = Guid.Parse("551de0bd-f8bf-4fa4-9523-f19b7c6dd95b");
	public static Guid AdminUserId = Guid.Parse("5d2b2a64-0fa7-46af-bf1c-aadf1d7fb120");
	public static Guid UserId1 = Guid.Parse("c75e1cf0-84c0-4f9e-a608-e9a9b0e7d62f");
	public static Guid UserId2 = Guid.Parse("9650f7f3-333b-4a77-b992-9a55179bfa12");
    public static Guid TestUserId = Guid.Parse("2fd5d547-737a-45d3-b71f-c5e8f692d434");
	// Hashed Passwords
	public static string AdminHashedPassword = SecurityHelper.GetSha256Hash("Admin123!");
	public static string SuperAdminHashedPassword = SecurityHelper.GetSha256Hash("SuperAdmin123!");
	public static string UserHashedPassword = SecurityHelper.GetSha256Hash("User123!");
	#endregion
    
    #region Wallet Ids
    public static Guid SuperAdminWalletId = Guid.Parse("c55fb374-3d74-4aa3-b576-d144c49cd184");
    public static Guid AdminWalletId = Guid.Parse("0acc9f75-9201-4ea5-9a16-5be1c30d6f60");
    public static Guid User1WalletId = Guid.Parse("ed12b679-8fd0-4a0c-ade5-fa6aaccf42fd");
    public static Guid User2WalletId = Guid.Parse("17f9e83c-b763-4e38-8902-1d0583adab05");
    public static Guid TestUserWalletId = Guid.Parse("2a5018f6-c8db-490a-9707-221469d20bb7");
    #endregion
    
    #region User Profile Ids
    public static Guid SuperAdminProfileId = Guid.Parse("ec7a3150-c202-4895-8b00-232f28e0eb4f");
    public static Guid AdminProfileId = Guid.Parse("5e46e00a-5162-4417-a240-36dc48793ad5");
    public static Guid User1ProfileId = Guid.Parse("b21013eb-7182-46ef-b543-b9606bc45c83");
    public static Guid User2ProfileId = Guid.Parse("29a0421c-6e4e-4793-bf3d-aad975155381");
    public static Guid TestUserProfileId = Guid.Parse("445819eb-053a-4c13-b8dd-fb736d46739f");
    #endregion

	//#region Permissions
	//// OldPermission Ids
	//public static Guid ViewRolesId = Guid.Parse("769f69a8-f457-4337-a319-4e05631f641e");
	//public static Guid ViewPagedRolesId = Guid.Parse("2b9bb0cc-c022-4ee8-8213-3c4235497d10");
	//public static Guid ViewRoleId = Guid.Parse("e2201ba3-5ae5-4947-b245-234cb1b8355c");
	//public static Guid AddRoleId = Guid.Parse("5c8de111-0623-4f74-bad7-13e22ab84945");
	//public static Guid UpdateRoleId = Guid.Parse("f7208df0-95b9-48ab-b0e9-bb4d1c6671b5");
	//public static Guid DeleteRoleId = Guid.Parse("317632a4-2dc2-4465-a817-874c47d3fa16");

	//public static Guid AddTestAggregateId = Guid.Parse("fc911fd3-8d58-4061-8307-8b0b1dc26bbb");
	//public static Guid UpdateTestAggregateId = Guid.Parse("4f73803c-a22f-46d9-8f0c-fcba00c700e6");
	//public static Guid DeleteTestAggregateId = Guid.Parse("8eae9c79-c301-4717-9e4f-e9a921b640c0");

	//public static Guid ViewUsersId = Guid.Parse("1011ed50-08a7-4983-a3b9-7513e3dfbd3e");
	//public static Guid ViewPagedUsersId = Guid.Parse("018fd5fd-2b38-469e-87b1-5a08ed9a7e74");
	//public static Guid ViewUserId = Guid.Parse("6d7d8938-1e8f-4676-bbe7-265282ad3a5f");
	//public static Guid UpdateUserId = Guid.Parse("1920ad7b-9916-42e1-9d83-af5cc213a722");
	//public static Guid DeleteUserId = Guid.Parse("decd3cdf-9119-4522-9f54-969e2e5a2df6");
	//public static Guid AddUserId = Guid.Parse("32bd2686-c356-445c-969e-20ba1b5be265");

	//public static Guid ViewPermissionsId = Guid.Parse("ccd14a65-90fc-45bf-86bd-36815d2aea4d");
	//public static Guid ViewPagedPermissionsId = Guid.Parse("93532db2-1a40-42a3-8689-8851964a9ecf");
	//public static Guid ViewPermissionId = Guid.Parse("eb737340-dc3e-49a6-8174-9360254372ea");
	//public static Guid UpdatePermissionId = Guid.Parse("1a6975cf-84a6-4cf8-9dcc-5344392141ff");
	//public static Guid DeletePermissionId = Guid.Parse("08303878-6efb-4878-9f1f-c7c891e2a61c");
	//public static Guid AddPermissionId = Guid.Parse("4b6b2561-44f6-4b7b-a1a0-85f9df29445d");

	//public static Guid ViewRolePermissionsId = Guid.Parse("6ea6ddae-c013-4a24-8834-5aad69c6b564");
	//public static Guid UpdateRolePermissionsId = Guid.Parse("7e2c20a0-32e1-4fef-ac87-aeb06a4a8e04");
	//public static Guid ViewUserPermissionsId = Guid.Parse("44c01901-bb14-4c96-8db9-7feef8e3c9d9");
	//public static Guid UpdateUserPermissionsId = Guid.Parse("b12e7de8-5e95-4636-a0f3-323921b15640");
	//#endregion

	#endregion
	public static void SeedData(this ModelBuilder modelBuilder)
	{
		SeedRoles(modelBuilder);
		SeedUsers(modelBuilder);
        SeedWallets(modelBuilder);
        SeedProfiles(modelBuilder);
		//SeedPermissions(modelBuilder);

	}

    private static void SeedProfiles(ModelBuilder modelBuilder)
    {
        var now = DateTime.Parse("2025-11-06T00:00:00Z");
        UserProfile superAdminProfile = UserProfile.Create(SuperAdminProfileId, SuperAdminUserId, "Super", "Admin", "/image.png", "This is SuperAdmin Profile", now);
        superAdminProfile.CreatedAt = now;
        superAdminProfile.ModifiedAt = now;
        superAdminProfile.CreatedBy = "System";
        superAdminProfile.ModifiedBy = "System";
        
        UserProfile adminProfile = UserProfile.Create(AdminProfileId, AdminUserId, "Admin", "User", "/image.png", "This is Admin Profile", now);
        adminProfile.CreatedAt = now;
        adminProfile.ModifiedAt = now;
        adminProfile.CreatedBy = "System";
        adminProfile.ModifiedBy = "System";
        
        UserProfile user1Profile = UserProfile.Create(User1ProfileId, UserId1, "User", "One", "/image.png", "This is User One Profile", now);
        user1Profile.CreatedAt = now;
        user1Profile.ModifiedAt = now;
        user1Profile.CreatedBy = "System";
        user1Profile.ModifiedBy = "System";
        
        UserProfile user2Profile = UserProfile.Create(User2ProfileId, UserId2, "User", "Two", "/image.png", "This is User Two Profile", now);
        user2Profile.CreatedAt = now;
        user2Profile.ModifiedAt = now;
        user2Profile.CreatedBy = "System";
        user2Profile.ModifiedBy = "System";
        
        UserProfile testUserProfile = UserProfile.Create(TestUserProfileId, TestUserId, "Test", "User", "/image.png", "This is User Test Profile", now);
        testUserProfile.CreatedAt = now;
        testUserProfile.ModifiedAt = now;
        testUserProfile.CreatedBy = "System";
        testUserProfile.ModifiedBy = "System";
        
        modelBuilder.Entity<UserProfile>()
            .HasData(
                superAdminProfile,
                adminProfile,
                user1Profile,
                user2Profile,
                testUserProfile
            );
    }

    private static void SeedWallets(ModelBuilder modelBuilder)
    {
        var now = DateTime.Parse("2025-11-06T00:00:00Z");
        Wallet superAdminWallet = Wallet.Create(SuperAdminWalletId, SuperAdminUserId, 0);
        superAdminWallet.CreatedAt = now;
        superAdminWallet.ModifiedAt = now;
        superAdminWallet.CreatedBy = "System";
        superAdminWallet.ModifiedBy = "System";
        
        Wallet adminWallet = Wallet.Create(AdminWalletId, AdminUserId, 0);
        adminWallet.CreatedAt = now;
        adminWallet.ModifiedAt = now;
        adminWallet.CreatedBy = "System";
        adminWallet.ModifiedBy = "System";
        
        Wallet user1Wallet = Wallet.Create(User1WalletId, UserId1, 0);
        user1Wallet.CreatedAt = now;
        user1Wallet.ModifiedAt = now;
        user1Wallet.CreatedBy = "System";
        user1Wallet.ModifiedBy = "System";

        Wallet user2Wallet = Wallet.Create(User2WalletId, UserId2, 0);
        user2Wallet.CreatedAt = now;
        user2Wallet.ModifiedAt = now;
        user2Wallet.CreatedBy = "System";
        user2Wallet.ModifiedBy = "System";

        Wallet testUserWallet = Wallet.Create(TestUserWalletId, TestUserId, 0);
        testUserWallet.CreatedAt = now;
        testUserWallet.ModifiedAt = now;
        testUserWallet.CreatedBy = "System";
        testUserWallet.ModifiedBy = "System";
        
        modelBuilder.Entity<Wallet>()
            .HasData(
                superAdminWallet,
                adminWallet,
                user1Wallet,
                user2Wallet,
                testUserWallet
            );

    }

    // Seeders
	private static void SeedRoles(this ModelBuilder modelBuilder)
	{
        var now = DateTime.Parse("2025-11-06T00:00:00Z");
		Role adminRole = Role.Create(AdminRoleId, "Admin", "Administrator role with all permissions.");
        adminRole.CreatedAt = now;
        adminRole.ModifiedAt = now;
        adminRole.CreatedBy = "System";
        adminRole.ModifiedBy = "System";

        Role userRole = Role.Create(UserRoleId, "User", "Standard user role with limited permissions.");
        userRole.CreatedAt = now;
        userRole.ModifiedAt = now;
        userRole.CreatedBy = "System";
        userRole.ModifiedBy = "System";
        
        Role testRole = Role.Create(TestRoleId, "TestRole", "Test Role to test soft delete");
        testRole.CreatedAt = now;
        testRole.ModifiedAt = now;
        testRole.CreatedBy = "System";
        testRole.ModifiedBy = "System";
        
		modelBuilder.Entity<Role>()
			.HasData(
				adminRole,
				userRole,
                testRole
			);
	}

	private static void SeedUsers(this ModelBuilder modelBuilder)
	{
        var now = DateTime.Parse("2025-11-06T00:00:00Z");
		User superAdminUser = User.Create(SuperAdminUserId, "superadmin", SuperAdminHashedPassword, "superadmin@superadmin.com",
		"09123456780", true, true);
        superAdminUser.CreatedAt = now;
        superAdminUser.ModifiedAt = now;
        superAdminUser.CreatedBy = "System";
        superAdminUser.ModifiedBy = "System";
        // superAdminUser.AddWallet(Wallet.Create(Guid.NewGuid(), SuperAdminUserId, 0));
        // superAdminUser.AddProfile(UserProfile.Create(Guid.NewGuid(), SuperAdminUserId, "", "", "", "", null));

		User adminUser = User.Create(AdminUserId, "admin", AdminHashedPassword, "admin@admin.com",
		"09123456789", true, false);
        adminUser.CreatedAt = now;
        adminUser.ModifiedAt = now;
        adminUser.CreatedBy = "System";
        adminUser.ModifiedBy = "System";

		User userUser1 = User
			.Create(UserId1, "user1", UserHashedPassword, "user1@user.com", "09123456782", true, false);
        userUser1.CreatedAt = now;
        userUser1.ModifiedAt = now;
        userUser1.CreatedBy = "System";
        userUser1.ModifiedBy = "System";

        User userUser2 = User
            .Create(UserId2, "user2", UserHashedPassword, "user2@user.com", "09123456787", true, false);
        userUser2.CreatedAt = now;
        userUser2.ModifiedAt = now;
        userUser2.CreatedBy = "System";
        userUser2.ModifiedBy = "System";

        User testUser = User
            .Create(TestUserId, "test", UserHashedPassword, "test@user.com", "09123456783", true, false);
        testUser.CreatedAt = now;
        testUser.ModifiedAt = now;
        testUser.CreatedBy = "System";
        testUser.ModifiedBy = "System";

		modelBuilder.Entity<User>().HasData(new List<User>()
		{
			superAdminUser,
			adminUser,
			userUser1,
			userUser2,
            testUser
		});

		modelBuilder.Entity<User>()
			.HasMany(u => u.Roles)
			.WithMany(r => r.Users)
			.UsingEntity<Dictionary<string, object>>(
				"UsersRoles",
				ur => ur.HasData(
					new { UsersId = SuperAdminUserId, RolesId = UserRoleId },
					new { UsersId = AdminUserId, RolesId = AdminRoleId },
					new { UsersId = UserId1, RolesId = UserRoleId },
                    new { UsersId = UserId2, RolesId = UserRoleId },
                    new { UsersId = TestUserId, RolesId = TestRoleId }
				)
			);
	}

	//private static void SeedPermissions(this ModelBuilder modelBuilder)
	//{
	//	OldPermission viewPermissionsPermission = OldPermission.Create(ViewPermissionsId, "ViewPermissions", "نمایش دسترسی ها");
	//	OldPermission viewPagedPermissionsPermission = OldPermission.Create(ViewPagedPermissionsId, "ViewPagePermissions", "نمایش دسترسی ها به صورت صفحه بندی");
	//	OldPermission viewPermissionPermission = OldPermission.Create(ViewPermissionId, "ViewPermission", "نمایش جزییات دسترسی");
	//	OldPermission addPermissionPermission = OldPermission.Create(AddPermissionId, "AddPermission", "افزودن دسترسی جدید");
	//	OldPermission updatePermissionPermission = OldPermission.Create(UpdatePermissionId, "UpdatePermission", "ویرایش دسترسی");
	//	OldPermission deletePermissionPermission = OldPermission.Create(DeletePermissionId, "DeletePermission", "حذف دسترسی");

	//	OldPermission viewRolesPermission = OldPermission.Create(ViewRolesId, "ViewRoles", "نمایش نقش ها");
	//	OldPermission viewPagedRolesPermission = OldPermission.Create(ViewPagedRolesId, "ViewPageRoles", "نمایش نقش ها به صورت صفحه بندی");
	//	OldPermission viewRoleByIdPermission = OldPermission.Create(ViewRoleId, "ViewRole", "نمایش جزییات نقش");
	//	OldPermission addRolePermission = OldPermission.Create(AddRoleId, "AddRole", "افزودن نقش جدید");
	//	OldPermission updateRolePermission = OldPermission.Create(UpdateRoleId, "UpdateRole", "ویرایش نقش");
	//	OldPermission deleteRolePermission = OldPermission.Create(DeleteRoleId, "DeleteRole", "حذف نقش");

	//	OldPermission viewRolePermissionsPermission = OldPermission.Create(ViewRolePermissionsId, "ViewRolePermissions", "نمایش دسترسی های یک نقش");
	//	OldPermission updateRolePermissionsPermission = OldPermission.Create(UpdateRolePermissionsId, "UpdateRolePermissions", "ویرایش دسترسی های یک نقش");

	//	OldPermission addTestAggregatePermission = OldPermission.Create(AddTestAggregateId, "AddTestAggregate", "افزودن موجودیت تستی جدید");
	//	OldPermission updateTestAggregatePermission = OldPermission.Create(UpdateTestAggregateId, "UpdateTestAggregate", "ویرایش موجودیت تستی");
	//	OldPermission deleteTestAggregatePermission = OldPermission.Create(DeleteTestAggregateId, "DeleteTestAggregate", "حذف موجودیت تستس");

	//	OldPermission viewUsersPermission = OldPermission.Create(ViewUsersId, "ViewUsers", "نمایش تمام لیست کاربران");
	//	OldPermission viewPagedUsersPermission = OldPermission.Create(ViewPagedUsersId, "ViewPagedUsers", "نمایش لیست کاربران به صورت صفحه بندی");
	//	OldPermission viewUserByIdPermission = OldPermission.Create(ViewUserId, "ViewUser", "نمایش جزییات یک کاربر");
	//	OldPermission updateUserPermission = OldPermission.Create(UpdateUserId, "UpdateUser", "ویرایش کاربر");
	//	OldPermission deleteUserPermission = OldPermission.Create(DeleteUserId, "DeleteUser", "حذف کاربر");
	//	OldPermission addUserPermission = OldPermission.Create(AddUserId, "AddUser", "افزودن کاربر");

	//	OldPermission viewUserPermissionsPermission = OldPermission.Create(ViewUserPermissionsId, "ViewUserPermissions", "نمایش دسترسی های یک کاربر");
	//	OldPermission updateUserPermissionsPermission = OldPermission.Create(UpdateUserPermissionsId, "UpdateUserPermissions", "ویرایش دسترسی های یک کاربر");

	//	modelBuilder.Entity<OldPermission>().HasData(new List<OldPermission>()
	//	{
	//		viewRolesPermission,
	//		viewPagedRolesPermission,
	//		viewRoleByIdPermission,
	//		addRolePermission,
	//		updateRolePermission,
	//		deleteRolePermission,
	//		addTestAggregatePermission,
	//		updateTestAggregatePermission,
	//		deleteTestAggregatePermission,
	//		viewUsersPermission,
	//		viewPagedUsersPermission,
	//		viewUserByIdPermission,
	//		updateUserPermission,
	//		deleteUserPermission,
	//		addUserPermission,
	//		viewPermissionsPermission,
	//		viewPagedPermissionsPermission,
	//		viewPermissionPermission,
	//		addPermissionPermission,
	//		updatePermissionPermission,
	//		deletePermissionPermission,
	//		viewRolePermissionsPermission,
	//		updateRolePermissionsPermission,
	//		viewUserPermissionsPermission,
	//		updateUserPermissionsPermission
	//	});

	//	modelBuilder.Entity<User>()
	//		.HasMany(u => u.Permissions)
	//		.WithMany(r => r.Users)
	//		.UsingEntity<Dictionary<string, object>>(
	//			"UsersPermissions",
	//			ur => ur.HasData(
	//				new { UsersId = UserUserId1, PermissionsId = AddTestAggregateId },
	//				new { UsersId = UserUserId1, PermissionsId = UpdateTestAggregateId },
	//				new { UsersId = UserUserId1, PermissionsId = DeleteTestAggregateId }
	//			)
	//		);

	//	modelBuilder.Entity<Role>()
	//		.HasMany(u => u.Permissions)
	//		.WithMany(r => r.Roles)
	//		.UsingEntity<Dictionary<string, object>>(
	//			"RolesPermissions",
	//			ur => ur.HasData(
	//				new { RolesId = AdminRoleId, PermissionsId = ViewRolesId },
	//				new { RolesId = AdminRoleId, PermissionsId = ViewPagedRolesId },
	//				new { RolesId = AdminRoleId, PermissionsId = ViewRoleId },
	//				new { RolesId = AdminRoleId, PermissionsId = AddRoleId },
	//				new { RolesId = AdminRoleId, PermissionsId = DeleteRoleId },
	//				new { RolesId = AdminRoleId, PermissionsId = UpdateRoleId },

	//				new { RolesId = AdminRoleId, PermissionsId = ViewPermissionsId },
	//				new { RolesId = AdminRoleId, PermissionsId = ViewPagedPermissionsId },
	//				new { RolesId = AdminRoleId, PermissionsId = ViewPermissionId },
	//				new { RolesId = AdminRoleId, PermissionsId = AddPermissionId },
	//				new { RolesId = AdminRoleId, PermissionsId = DeletePermissionId },
	//				new { RolesId = AdminRoleId, PermissionsId = UpdatePermissionId },

	//				new { RolesId = AdminRoleId, PermissionsId = ViewRolePermissionsId },
	//				new { RolesId = AdminRoleId, PermissionsId = UpdateRolePermissionsId },
	//				new { RolesId = AdminRoleId, PermissionsId = ViewUserPermissionsId },
	//				new { RolesId = AdminRoleId, PermissionsId = UpdateUserPermissionsId },

	//				new { RolesId = AdminRoleId, PermissionsId = ViewUsersId },
	//				new { RolesId = AdminRoleId, PermissionsId = ViewPagedUsersId },
	//				new { RolesId = AdminRoleId, PermissionsId = ViewUserId },
	//				new { RolesId = AdminRoleId, PermissionsId = AddUserId },
	//				new { RolesId = AdminRoleId, PermissionsId = DeleteUserId },
	//				new { RolesId = AdminRoleId, PermissionsId = UpdateUserId }
	//			)
	//		);
	//}
}
