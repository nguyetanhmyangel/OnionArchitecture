namespace OnionArchitecture.Application.Features.Reports.Queries.GetById
{
    public class GetReportByIdResponse
    {
        public int Id { get; set; }
        public int MySpaceId { get; set; }
        public string Content { get; set; }
        public bool IsProcessed { get; set; }
    }
}