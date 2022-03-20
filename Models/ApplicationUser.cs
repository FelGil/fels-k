using Microsoft.AspNetCore.Identity;

namespace Projekt.Models
{
    public class ApplicationUser : IdentityUser
    {   
        public string? Firstname { get; set;}

        public string? Lastname { get; set;}

        public  IEnumerable<Sites>? Sites { get; set; }

        public IEnumerable<Comments>? Comments { get; set; }

    }
}
