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
    
    public static class Roles
    {
        public const string CreateRole = "";
        public const string UpdateRole = "{id}";
        public const string GetAllRoles = "";
        public const string GetPagedRoles = "paged";
        public const string GetRoleById = "{id}";
        public const string DeleteRole = "{id}";
    }
}