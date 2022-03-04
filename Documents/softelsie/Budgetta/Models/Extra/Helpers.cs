using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Extra
{
    public static class Helpers
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "User", Id = "id";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "Squata Owner";
            }
        }
    }
}
