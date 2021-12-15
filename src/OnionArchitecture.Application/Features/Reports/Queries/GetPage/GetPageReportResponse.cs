namespace OnionArchitecture.Application.Features.Reports.Queries.GetPage
{
    public class GetPageReportResponse
    {
        public int Id { get; set; }
        public int KnowledgeBaseId { get; set; }
        public string Content { get; set; }
        public bool IsProcessed { get; set; }
    }
}