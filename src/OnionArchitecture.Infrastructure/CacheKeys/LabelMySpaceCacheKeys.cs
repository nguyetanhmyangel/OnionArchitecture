namespace OnionArchitecture.Infrastructure.CacheKeys
{
    public static class LabelMySpaceCacheKeys
    {
        public static string ListKey => "LabelMySpaceList";

        public static string SelectListKey => "LabelMySpaceSelectList";

        public static string GetKey(int labelMySpaceId) => $"LabelMyBase-{labelMySpaceId}";

        public static string GetDetailsKey(int labelMySpaceId) => $"LabelMyBaseDetails-{labelMySpaceId}";
    }
}