namespace OnionArchitecture.Infrastructure.CacheKeys
{
    public static class MySpaceCacheKeys
    {
        public static string ListKey => "MySpaceList";

        public static string SelectListKey => "MySpaceSelectList";

        public static string GetKey(int mySpaceId) => $"MySpace-{mySpaceId}";

        public static string GetDetailsKey(int mySpaceId) => $"MySpaceDetails-{mySpaceId}";
    }
}