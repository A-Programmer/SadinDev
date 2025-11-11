using KSFramework.Utilities;
using KSProject.Common.Constants.Enums;
using KSProject.Domain.Aggregates.Billings;
using KSProject.Domain.Aggregates.Roles;
using KSProject.Domain.Aggregates.Users;
using KSProject.Domain.Aggregates.Wallets;
// اضافه شده برای ServiceRate (فرض فولدر Billing)
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
    
    #region Transaction Ids (جدید: برای تست transactionها در walletها)
    public static Guid SuperAdminTransactionId1 = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-def234567890"); // Charge
    public static Guid SuperAdminTransactionId2 = Guid.Parse("b2c3d4e5-f678-9abc-1def-234567890abc"); // Usage for Blog
    public static Guid AdminTransactionId1 = Guid.Parse("c3d4e5f6-789a-bc1d-ef23-4567890abcde"); // Charge
    public static Guid User1TransactionId1 = Guid.Parse("d4e5f678-9abc-1def-2345-67890abcde12"); // Usage for Notification
    public static Guid User2TransactionId1 = Guid.Parse("e5f6789a-bc1d-ef23-4567-890abcde1234"); // Refund test
    public static Guid TestUserTransactionId1 = Guid.Parse("f6789abc-1def-2345-6789-0abcde123456"); // Adjustment
    #endregion
    
    #region User Profile Ids
    public static Guid SuperAdminProfileId = Guid.Parse("ec7a3150-c202-4895-8b00-232f28e0eb4f");
    public static Guid AdminProfileId = Guid.Parse("5e46e00a-5162-4417-a240-36dc48793ad5");
    public static Guid User1ProfileId = Guid.Parse("b21013eb-7182-46ef-b543-b9606bc45c83");
    public static Guid User2ProfileId = Guid.Parse("29a0421c-6e4e-4793-bf3d-aad975155381");
    public static Guid TestUserProfileId = Guid.Parse("445819eb-053a-4c13-b8dd-fb736d46739f");
    #endregion
    
    #region Api Keys Ids
    public static Guid SuperAdminApiKeyId = Guid.Parse("c55fb374-3d74-4aa3-b576-d144c49cd184");
    public static Guid AdminApiKeyId = Guid.Parse("0acc9f75-9201-4ea5-9a16-5be1c30d6f60");
    public static Guid User1ApiKeyId1 = Guid.Parse("ed12b679-8fd0-4a0c-ade5-fa6aaccf42fd");
    public static Guid User1ApiKeyId2 = Guid.Parse("17f9e83c-b763-4e38-8902-1d0583adab05");
    public static Guid TestUserApiKeyId = Guid.Parse("2a5018f6-c8db-490a-9707-221469d20bb7");
    #endregion
    
    #region Service Rate Ids (جدید: برای تست pricing با variantها)
    public static Guid BlogPostsDefaultRateId = Guid.Parse("11111111-2222-3333-4444-555555555555"); // Default for Blog Posts_Count
    public static Guid BlogPostsPremiumRateId = Guid.Parse("22222222-3333-4444-5555-666666666666"); // Premium variant with discount rule
    public static Guid NotificationSmsDefaultRateId = Guid.Parse("33333333-4444-5555-6666-777777777777"); // Default for Notification SMS_Count
    public static Guid OnlineStoreTransactionsTier1RateId = Guid.Parse("44444444-5555-6666-7777-888888888888"); // Tier1 for OnlineStore Transactions_Count
    #endregion

    #endregion
    public static void SeedData(this ModelBuilder modelBuilder)
    {
       SeedRoles(modelBuilder);
       SeedUsers(modelBuilder);
        SeedWallets(modelBuilder);
        SeedTransactions(modelBuilder); // جدید: Seeder برای Transactionها
        SeedProfiles(modelBuilder);
        SeedApiKeys(modelBuilder);
        SeedServiceRates(modelBuilder); // جدید: Seeder برای ServiceRateها
    }

    private static void SeedApiKeys(this ModelBuilder modelBuilder)
    {
        var now = DateTime.Parse("2025-11-06T00:00:00Z");
        ApiKey superAdminApiKey = ApiKey.Create(SuperAdminApiKeyId, SuperAdminUserId, SuperAdminApiKeyId.ToString().Replace("-", ""), true, now.AddYears(1),"sliders.create,sliders.show-all,sliders.update,users.show-all,users.create,users.update,users.delete" );
        superAdminApiKey.CreatedAt = now;
        superAdminApiKey.ModifiedAt = now;
        superAdminApiKey.CreatedBy = "System";
        superAdminApiKey.ModifiedBy = "System";
        
        ApiKey adminApiKey = ApiKey.Create(AdminApiKeyId, AdminUserId, AdminApiKeyId.ToString().Replace("-", ""), true, now.AddYears(1),"sliders.create,sliders.show-all,sliders.update,users.show-all,users.create,users.update,users.delete" );
        adminApiKey.CreatedAt = now;
        adminApiKey.ModifiedAt = now;
        adminApiKey.CreatedBy = "System";
        adminApiKey.ModifiedBy = "System";
        
        ApiKey user1ApiKey1 = ApiKey.Create(User1ApiKeyId1, UserId1, User1ApiKeyId1.ToString().Replace("-", ""), true, now.AddYears(1),"sliders.show-all" );
        user1ApiKey1.CreatedAt = now;
        user1ApiKey1.ModifiedAt = now;
        user1ApiKey1.CreatedBy = "System";
        user1ApiKey1.ModifiedBy = "System";
        
        ApiKey user1ApiKey2 = ApiKey.Create(User1ApiKeyId2, UserId1, User1ApiKeyId2.ToString().Replace("-", ""), true, now.AddYears(1),"sliders.create,sliders.show-all" );
        user1ApiKey2.CreatedAt = now;
        user1ApiKey2.ModifiedAt = now;
        user1ApiKey2.CreatedBy = "System";
        user1ApiKey2.ModifiedBy = "System";
        
        ApiKey testUserApiKey = ApiKey.Create(TestUserApiKeyId, TestUserId, TestUserApiKeyId.ToString().Replace("-", ""), true, now.AddYears(1),"sliders.create,sliders.show-all" );
        testUserApiKey.CreatedAt = now;
        testUserApiKey.ModifiedAt = now;
        testUserApiKey.CreatedBy = "System";
        testUserApiKey.ModifiedBy = "System";
        
        modelBuilder.Entity<ApiKey>()
            .HasData(
                superAdminApiKey,
                adminApiKey,
                user1ApiKey1,
                user1ApiKey2,
                testUserApiKey
            );
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
        Wallet superAdminWallet = Wallet.Create(SuperAdminWalletId, SuperAdminUserId, 100.0m); // اولیه 100 برای تست
        superAdminWallet.CreatedAt = now;
        superAdminWallet.ModifiedAt = now;
        superAdminWallet.CreatedBy = "System";
        superAdminWallet.ModifiedBy = "System";
        
        Wallet adminWallet = Wallet.Create(AdminWalletId, AdminUserId, 50.0m); // اولیه 50 برای تست
        adminWallet.CreatedAt = now;
        adminWallet.ModifiedAt = now;
        adminWallet.CreatedBy = "System";
        adminWallet.ModifiedBy = "System";
        
        Wallet user1Wallet = Wallet.Create(User1WalletId, UserId1, 20.0m); // اولیه 20 برای تست
        user1Wallet.CreatedAt = now;
        user1Wallet.ModifiedAt = now;
        user1Wallet.CreatedBy = "System";
        user1Wallet.ModifiedBy = "System";

        Wallet user2Wallet = Wallet.Create(User2WalletId, UserId2, 0.0m);
        user2Wallet.CreatedAt = now;
        user2Wallet.ModifiedAt = now;
        user2Wallet.CreatedBy = "System";
        user2Wallet.ModifiedBy = "System";

        Wallet testUserWallet = Wallet.Create(TestUserWalletId, TestUserId, 10.0m); // اولیه 10 برای تست
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

    // Seeder جدید برای Transactionها (child of Wallet)
    private static void SeedTransactions(ModelBuilder modelBuilder)
    {
        var now = DateTime.Parse("2025-11-06T00:00:00Z");

        // Transaction برای SuperAdminWallet: Charge 100, Usage for Blog
        Transaction superAdminTransaction1 = Transaction.Create(SuperAdminTransactionId1, SuperAdminWalletId, 100.0m, TransactionTypes.Charge, null, null, 0);
        superAdminTransaction1.CreatedAt = now;
        superAdminTransaction1.ModifiedAt = now;
        superAdminTransaction1.CreatedBy = "System";
        superAdminTransaction1.ModifiedBy = "System";

        Transaction superAdminTransaction2 = Transaction.Create(SuperAdminTransactionId2, SuperAdminWalletId, -5.0m, TransactionTypes.Usage, "Blog", "Posts_Count", 5.0m);
        superAdminTransaction2.CreatedAt = now;
        superAdminTransaction2.ModifiedAt = now;
        superAdminTransaction2.CreatedBy = "System";
        superAdminTransaction2.ModifiedBy = "System";

        // Transaction برای AdminWallet: Charge 50
        Transaction adminTransaction1 = Transaction.Create(AdminTransactionId1, AdminWalletId, 50.0m, TransactionTypes.Charge, null, null, 0);
        adminTransaction1.CreatedAt = now;
        adminTransaction1.ModifiedAt = now;
        adminTransaction1.CreatedBy = "System";
        adminTransaction1.ModifiedBy = "System";

        // Transaction برای User1Wallet: Usage for Notification
        Transaction user1Transaction1 = Transaction.Create(User1TransactionId1, User1WalletId, -2.0m, TransactionTypes.Usage, "Notification", "SMS_Count", 10.0m);
        user1Transaction1.CreatedAt = now;
        user1Transaction1.ModifiedAt = now;
        user1Transaction1.CreatedBy = "System";
        user1Transaction1.ModifiedBy = "System";

        // Transaction برای User2Wallet: Refund test
        Transaction user2Transaction1 = Transaction.Create(User2TransactionId1, User2WalletId, 10.0m, TransactionTypes.Refund, "OnlineStore", "Transactions_Count", 1.0m);
        user2Transaction1.CreatedAt = now;
        user2Transaction1.ModifiedAt = now;
        user2Transaction1.CreatedBy = "System";
        user2Transaction1.ModifiedBy = "System";

        // Transaction برای TestUserWallet: Adjustment
        Transaction testUserTransaction1 = Transaction.Create(TestUserTransactionId1, TestUserWalletId, -1.0m, TransactionTypes.Adjustment, null, null, 0);
        testUserTransaction1.CreatedAt = now;
        testUserTransaction1.ModifiedAt = now;
        testUserTransaction1.CreatedBy = "System";
        testUserTransaction1.ModifiedBy = "System";

        modelBuilder.Entity<Transaction>()
            .HasData(
                superAdminTransaction1,
                superAdminTransaction2,
                adminTransaction1,
                user1Transaction1,
                user2Transaction1,
                testUserTransaction1
            );
    }

    // Seeder جدید برای ServiceRateها (برای تست pricing با variantها و rules)
    private static void SeedServiceRates(ModelBuilder modelBuilder)
    {
        var now = DateTime.Parse("2025-11-06T00:00:00Z");

        // Rate برای Blog Posts_Count Default
        ServiceRate blogPostsDefaultRate = ServiceRate.Create(BlogPostsDefaultRateId, "Blog", "Posts_Count", "Default", 0.01m, null);
        blogPostsDefaultRate.CreatedAt = now;
        blogPostsDefaultRate.ModifiedAt = now;
        blogPostsDefaultRate.CreatedBy = "System";
        blogPostsDefaultRate.ModifiedBy = "System";

        // Rate برای Blog Posts_Count Premium with rule (discount if >50 posts)
        ServiceRate blogPostsPremiumRate = ServiceRate.Create(BlogPostsPremiumRateId, "Blog", "Posts_Count", "Premium", 0.005m, "{\"minQuantity\": 50, \"discountPercent\": 10}");
        blogPostsPremiumRate.CreatedAt = now;
        blogPostsPremiumRate.ModifiedAt = now;
        blogPostsPremiumRate.CreatedBy = "System";
        blogPostsPremiumRate.ModifiedBy = "System";

        // Rate برای Notification SMS_Count Default
        ServiceRate notificationSmsDefaultRate = ServiceRate.Create(NotificationSmsDefaultRateId, "Notification", "SMS_Count", "Default", 0.02m, null);
        notificationSmsDefaultRate.CreatedAt = now;
        notificationSmsDefaultRate.ModifiedAt = now;
        notificationSmsDefaultRate.CreatedBy = "System";
        notificationSmsDefaultRate.ModifiedBy = "System";

        // Rate برای OnlineStore Transactions_Count Tier1 with rule (discount if >100 transactions)
        ServiceRate onlineStoreTransactionsTier1Rate = ServiceRate.Create(OnlineStoreTransactionsTier1RateId, "OnlineStore", "Transactions_Count", "Tier1", 0.015m, "{\"minQuantity\": 100, \"discountPercent\": 15}");
        onlineStoreTransactionsTier1Rate.CreatedAt = now;
        onlineStoreTransactionsTier1Rate.ModifiedAt = now;
        onlineStoreTransactionsTier1Rate.CreatedBy = "System";
        onlineStoreTransactionsTier1Rate.ModifiedBy = "System";

        modelBuilder.Entity<ServiceRate>()
            .HasData(
                blogPostsDefaultRate,
                blogPostsPremiumRate,
                notificationSmsDefaultRate,
                onlineStoreTransactionsTier1Rate
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
}
