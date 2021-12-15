namespace OnionArchitecture.Application.Features.Comments.Queries.GetById
{
    public class GetCommentByIdResponse
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int KnowledgeBaseId { get; set; }
    }
}