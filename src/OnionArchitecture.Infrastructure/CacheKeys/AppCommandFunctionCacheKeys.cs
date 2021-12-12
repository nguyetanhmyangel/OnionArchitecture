namespace OnionArchitecture.Infrastructure.CacheKeys
{
    public static class AppCommandFunctionCacheKeys
    {
        public static string ListKey => "AppCommandFunctionList";

        public static string SelectListKey => "AppCommandFunctionSelectList";

        public static string GetKey(int appCommandFunctionId) => $"CommandFunction-{appCommandFunctionId}";

        public static string GetDetailsKey(int appCommandFunctionId) => $"CommandFunctionDetails-{appCommandFunctionId}";
    }
}