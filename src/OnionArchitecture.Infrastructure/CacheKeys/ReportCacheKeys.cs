namespace OnionArchitecture.Infrastructure.CacheKeys
{
    public static class ReportCacheKeys
    {
        public static string ListKey => "ReportList";

        public static string SelectListKey => "ReportSelectList";

        public static string GetKey(int reportId) => $"Report-{reportId}";

        public static string GetDetailsKey(int reportId) => $"ReportDetails-{reportId}";
    }
}