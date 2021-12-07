namespace OnionArchitecture.Infrastructure.CacheKeys
{
    public static class EnjoinCacheKeys
    {
        public static string ListKey => "EnjoinList";

        public static string SelectListKey => "EnjoinSelectList";

        public static string GetKey(int enjoinId) => $"Enjoin-{enjoinId}";

        public static string GetDetailsKey(int enjoinId) => $"EnjoinDetails-{enjoinId}";
    }
}