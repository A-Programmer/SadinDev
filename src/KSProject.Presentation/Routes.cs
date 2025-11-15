namespace KSProject.Presentation;

public static class Routes
{
    public const string ROOT = "api/v1/[controller]";

    public static class Auth
    {
        public const string LOGIN = "login";
        public const string REGISTER = "register";
        public const string CHECK_USER_EXISTENCE = "check-user-existence";
        public const string VALIDATE_USER = "validate-user";
    }
    
    public static class Otp
    {
        public const string LOGIN = "";
        public const string VERIFY = "verify";
        public const string RESEND = "resend";
        public const string EXPIRE_CODE = "expire";
    }

    public static class TestAggregates
    {
        public const string CREATE = "";
        public const string UPDATE = "{id}";
        public const string GET_ALL = "";
        public const string GET_BY_ID = "{id}";
        public const string GET_PAGED = "paged";
        public const string DELETE = "{id}";

    }

    public static class Roles_Admin
    {
        public const string CREATE = "";
        public const string UPDATE = "{id}";
        public const string GET_ALL = "";
        public const string GET_PAGED = "paged";
        public const string GET_BY_ID = "{id}";
        public const string DELETE = "{id}";

        public static class Role_Permissions
        {
            public const string GET_ALL = "{id}/permissions";
            public const string UPDATE = "{id}/permissions";
        }
    }

    public static class Users_Admin
    {
        public const string CREATE = "";
        public const string UPDATE = "{id}";
        public const string GET_ALL = "";
        public const string GET_PAGED = "paged";
        public const string GET_BY_ID = "{id}";
        public const string DELETE = "{id}";

        public static class User_Roles
        {
            public const string UPDATE = "{id}/roles";
        }

        public static class User_Permissions
        {
            public const string GET_ALL = "{id}/permissions";
            public const string UPDATE = "{id}/permissions";
        }

        public static class User_Profiles
        {
            public const string GET = "{id}/profile";
            public const string CREATE = "{id}/profile";
            public const string UPDATE = "{id}/profile";
            public const string UPDATE_PROFILE_IMAGE = "{id}/profile/profile-image";
            public const string DELETE = "{id}/profile";
        }

        public static class User_Settings
        {
            public const string GET = "{id}/settings";
            public const string CREATE = "{id}/settings";
            public const string UPDATE = "{id}/settings";
            public const string NEWSLETTER = "{id}/settings/newsletter";
            public const string UPDATE_NEWSLETTER = "{id}/settings/newsletter";
            public const string USER_LEVEL = "{id}/settings/user-level";
            public const string UPDATE_USER_LEVEL = "{id}/settings/user-level";
        }
        
        public static class ApiKeys_Admin
        {
            public const string GENERATE = "{userId}/apikeys";
            public const string GET_USER_API_KEYS = "{userId}/apikeys";
            public const string REVOKE = "{userId}/apikeys/revoke/{apiKeyId}";
        }
    }
    
    public static class Wallets_Admin
    {
        public const string CREATE = "";
        public const string GET_BALANCE = "";
        public const string CHARGE_WALLET = "";
    }
    
    public static class Billings_Admin
    {
        public const string CALCULATE_COST = "";
    }
    
    public static class ApiKeys_Admin
    {
        public const string GENERATE = "";
        public const string GET_USER_API_KEYS = "";
        public const string REVOKE = "revoke/{apiKeyId}";
    }

    public static class Static_Data
    {
        public const string GET_PAYMENT_GTEWAYS = "";
    }
}
