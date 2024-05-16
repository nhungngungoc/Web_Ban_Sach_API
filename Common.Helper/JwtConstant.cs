using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public static class JwtConstant
    {
        public static int expiresIn { get; } = 20000;
        public static int refresh_expiresIn { get; } = 30000;
    }
    public static class ClaimsConstant
    {
        public const string USER_ID = "UserId";
        public const string ROLE = "Role";
        public const string PERMISSION = "Permission";
    }
}
