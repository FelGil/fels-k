using System.ComponentModel.DataAnnotations;


namespace Projekt.Models
{
    public class Comments
    {
        //properties
        public int CommentsId { get; set; }

        [Display(Name = "Sites")]
        public int SitesId { get; set; }
        public virtual Sites? Sites { get; set; }

        public string? Comment { get; set; }

        public string? UserId { get; set; }
        public virtual ApplicationUser? ApplicationUser { get; set; }




    }
}
