namespace OnionArchitecture.Application.Features.Comments.Queries.Get
{
    public class GetCommentResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int KnowledgeBaseId { get; set; }
    }
}