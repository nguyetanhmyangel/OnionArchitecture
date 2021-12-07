namespace OnionArchitecture.Application.Features.Functions.Queries.Get
{
    public class GetFunctionResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public int SortOrder { get; set; }

        public string ParentId { get; set; }

        public string Icon { get; set; }
    }
}