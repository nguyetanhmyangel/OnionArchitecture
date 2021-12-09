namespace OnionArchitecture.Application.Features.Reports.Queries.Get
{
    public class GetReportResponse
    {
        public int Id { get; set; }
        public int MySpaceId { get; set; }
        public string Content { get; set; }
        public bool IsProcessed { get; set; }
    }
}