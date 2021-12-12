namespace OnionArchitecture.Infrastructure.CacheKeys
{
    public static class AppPermissionCacheKeys
    {
        public static string ListKey => "AppPermissionList";

        public static string SelectListKey => "AppPermissionSelectList";

        public static string GetKey(int appPermissionId) => $"Privilege-{appPermissionId}";

        public static string GetDetailsKey(int appPermissionId) => $"PrivilegeDetails-{appPermissionId}";
    }
}