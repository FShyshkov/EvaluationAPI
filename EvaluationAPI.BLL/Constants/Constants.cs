

namespace EvaluationAPI.BLL.Constants
{
    public static class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id", UserA = "usera", TestA = "testa";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
                public const string UserAccess = "user_access";
                public const string TestAccess = "test_edit_access";
            }
        }
    }
}
