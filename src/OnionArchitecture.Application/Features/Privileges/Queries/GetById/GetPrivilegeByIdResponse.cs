namespace OnionArchitecture.Application.Features.Privileges.Queries.GetById
{
    public class GetPrivilegeByIdResponse
    {
        public int Id { get; set; }
        public int FunctionId { get; set; }
        public int RoleId { get; set; }
        public int EnjoinId { get; set; }
    }
}