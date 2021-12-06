namespace OnionArchitecture.Infrastructure.CacheKeys
{
    public static class VoteCacheKeys
    {
        public static string ListKey => "VoteList";

        public static string SelectListKey => "VoteSelectList";

        public static string GetKey(int voteId) => $"Vote-{voteId}";

        public static string GetDetailsKey(int voteId) => $"VoteDetails-{voteId}";
    }
}