namespace OnionArchitecture.Infrastructure.CacheKeys
{
    public static class CommentCacheKeys
    {
        public static string ListKey => "CommentList";

        public static string SelectListKey => "CommentSelectList";

        public static string GetKey(int commentId) => $"Comment-{commentId}";

        public static string GetDetailsKey(int commentId) => $"CommentDetails-{commentId}";
    }
}