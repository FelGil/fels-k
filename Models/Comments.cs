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
        [Required(ErrorMessage = "Please add a comment befor you press ADD")]
        public string? Comment { get; set; }

        public string? ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }




    }
}
