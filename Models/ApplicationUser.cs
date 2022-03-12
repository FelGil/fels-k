using Microsoft.AspNetCore.Identity;

namespace Projekt.Models
{
    public class ApplicationUser : IdentityUser
    {
        //public List<Comments>? Comments { get; set; }

        public  IEnumerable<Sites>? Sites { get; set; }

        public IEnumerable<Comments>? Comments { get; set; }

    }
}
