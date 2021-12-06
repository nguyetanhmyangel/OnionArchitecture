namespace OnionArchitecture.Infrastructure.CacheKeys
{
    public static class CommandFunctionCacheKeys
    {
        public static string ListKey => "CommandFunctionList";

        public static string SelectListKey => "CommandFunctionSelectList";

        public static string GetKey(int commandFunctionId) => $"CommandFunction-{commandFunctionId}";

        public static string GetDetailsKey(int commandFunctionId) => $"CommandFunctionDetails-{commandFunctionId}";
    }
}