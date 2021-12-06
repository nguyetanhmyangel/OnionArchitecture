namespace OnionArchitecture.Infrastructure.CacheKeys
{
    public static class LabelMyBaseCacheKeys
    {
        public static string ListKey => "LabelMyBaseList";

        public static string SelectListKey => "LabelMyBaseSelectList";

        public static string GetKey(int labelMyBaseId) => $"LabelMyBase-{labelMyBaseId}";

        public static string GetDetailsKey(int labelMyBaseId) => $"LabelMyBaseDetails-{labelMyBaseId}";
    }
}