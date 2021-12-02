using Microsoft.AspNetCore.Identity;

namespace OnionArchitecture.Infrastructure.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public byte[] ProfilePicture { get; set; }
        public string PictureSrc { get; set; }
        public bool Active { get; set; } = false;
    }
}