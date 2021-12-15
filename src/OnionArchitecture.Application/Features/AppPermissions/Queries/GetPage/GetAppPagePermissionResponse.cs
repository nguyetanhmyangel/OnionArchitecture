namespace OnionArchitecture.Application.Features.AppPermissions.Queries.GetPage
{
    public class GetAppPagePermissionResponse
    {
        public int Id { get; set; }
        public int FunctionId { get; set; }
        public int RoleId { get; set; }
        public int AppCommandId { get; set; }
    }
}