namespace OnionArchitecture.Infrastructure.CacheKeys
{
    public static class PrivilegeCacheKeys
    {
        public static string ListKey => "PrivilegeList";

        public static string SelectListKey => "PrivilegeSelectList";

        public static string GetKey(int privilegeId) => $"Privilege-{privilegeId}";

        public static string GetDetailsKey(int privilegeId) => $"PrivilegeDetails-{privilegeId}";
    }
}