namespace OnionArchitecture.Infrastructure.CacheKeys
{
    public static class LabelKnowledgeBaseCacheKeys
    {
        public static string ListKey => "LabelKnowledgeBaseList";

        public static string SelectListKey => "LabelKnowledgeBaseSelectList";

        public static string GetKey(int labelKnowledgeBaseId) => $"LabelKnowledgeBase-{labelKnowledgeBaseId}";

        public static string GetDetailsKey(int labelKnowledgeBaseId) => $"LabelMyBaseDetails-{labelKnowledgeBaseId}";
    }
}