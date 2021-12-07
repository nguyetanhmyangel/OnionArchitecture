namespace OnionArchitecture.Application.Features.Categories.Queries.Get
{
    public class GetCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SeoAlias { get; set; }
        public string SeoDescription { get; set; }
        public int SortOrder { get; set; }
        public int? ParentId { get; set; }
        public int? NumberOfTickets { get; set; }
    }
}