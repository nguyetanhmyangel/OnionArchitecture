namespace OnionArchitecture.Application.Features.Attachments.Queries.GetAllPaged
{
    public class GetAllCategoriesResponse
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public int KnowledgeBaseId { get; set; }
    }
}