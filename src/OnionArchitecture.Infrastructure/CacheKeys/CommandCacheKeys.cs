namespace OnionArchitecture.Infrastructure.CacheKeys
{
    public static class CommandCacheKeys
    {
        public static string ListKey => "CommandList";

        public static string SelectListKey => "CommandSelectList";

        public static string GetKey(int commandId) => $"Command-{commandId}";

        public static string GetDetailsKey(int commandId) => $"CommandDetails-{commandId}";
    }
}