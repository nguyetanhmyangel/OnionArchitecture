namespace OnionArchitecture.Application.Features.AppPermissions.Queries.GetById
{
    public class GetAppPermissionByIdResponse
    {
        public int Id { get; set; }
        public int FunctionId { get; set; }
        public int RoleId { get; set; }
        public int AppCommandId { get; set; }
    }
}