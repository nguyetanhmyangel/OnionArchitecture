namespace OnionArchitecture.Application.Features.Functions.Queries.GetPage
{
    public class GetPageFunctionResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Url { get; set; }

        public int SortOrder { get; set; }

        public int ParentId { get; set; }

        public string Icon { get; set; }
    }
}