using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using OnionArchitecture.Application.Interfaces.Services;

namespace OnionArchitecture.Api.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
        }

        public string UserId { get; }
        public string UserName { get; }
    }
}