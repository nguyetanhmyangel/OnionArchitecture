namespace OnionArchitecture.Infrastructure.CacheKeys
{
    public static class AttachmentCacheKeys
    {
        public static string ListKey => "AttachmentList";

        public static string SelectListKey => "AttachmentSelectList";

        public static string GetKey(int attachmentId) => $"Attachment-{attachmentId}";

        public static string GetDetailsKey(int attachmentId) => $"AttachmentDetails-{attachmentId}";
    }
}