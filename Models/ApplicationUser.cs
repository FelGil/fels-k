using Microsoft.AspNetCore.Identity;

namespace Projekt.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        //public List<Comments>? Comments { get; set; }

        public virtual IEnumerable<Sites>? Sites { get; set; }

        public virtual IEnumerable<Comments>? Comments { get; set; }

    }
}
