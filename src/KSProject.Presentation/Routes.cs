namespace KSProject.Presentation;

public static class Routes
{
    public const string Root = "api/v1/[controller]";

    public static class TestAggregates
    {
        public const string CreateTestAggregate = "";
        public const string UpdateTestAggregate = "{id}";
        public const string GetAllTestAggregates = "";
        public const string GetTestAggregateById = "{id}";
        public const string GetPagedTestAggregates = "paged";        
        public const string DeleteTestAggregate = "{id}";

    }
    
    public static class Roles_Admin
    {
        public const string CreateRole = "";
        public const string UpdateRole = "{id}";
        public const string GetAllRoles = "";
        public const string GetPagedRoles = "paged";
        public const string GetRoleById = "{id}";
        public const string DeleteRole = "{id}";
    }

    public static class Users_Admin
    {
        public const string CreateUser = "";
        public const string UpdateUser = "{id}";
        public const string GetAllUsers = "";
        public const string GetPagedUsers = "paged";
        public const string GetUserById = "{id}";
        public const string DeleteUser = "{id}";
        public const string UpdateUserRoles = "{id}/roles";
    }
}