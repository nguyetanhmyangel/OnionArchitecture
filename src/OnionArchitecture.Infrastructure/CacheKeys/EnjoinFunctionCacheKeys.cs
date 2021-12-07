namespace OnionArchitecture.Infrastructure.CacheKeys
{
    public static class EnjoinFunctionCacheKeys
    {
        public static string ListKey => "EnjoinFunctionList";

        public static string SelectListKey => "EnjoinFunctionSelectList";

        public static string GetKey(int enjoinFunctionId) => $"CommandFunction-{enjoinFunctionId}";

        public static string GetDetailsKey(int enjoinFunctionId) => $"CommandFunctionDetails-{enjoinFunctionId}";
    }
}