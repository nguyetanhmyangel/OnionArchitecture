namespace OnionArchitecture.Infrastructure.CacheKeys
{
    public static class KnowledgeBaseCacheKeys
    {
        public static string ListKey => "KnowledgeBaseList";

        public static string SelectListKey => "KnowledgeBaseSelectList";

        public static string GetKey(int knowledgeBaseId) => $"KnowledgeBase-{knowledgeBaseId}";

        public static string GetDetailsKey(int knowledgeBaseId) => $"KnowledgeBaseDetails-{knowledgeBaseId}";
    }
}