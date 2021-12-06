namespace OnionArchitecture.Infrastructure.CacheKeys
{
    public static class MyBaseCacheKeys
    {
        public static string ListKey => "MyBaseList";

        public static string SelectListKey => "MyBaseSelectList";

        public static string GetKey(int myBaseId) => $"MyBase-{myBaseId}";

        public static string GetDetailsKey(int myBaseId) => $"MyBaseDetails-{myBaseId}";
    }
}