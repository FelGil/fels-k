using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class Sites
    {
        //Properties
        public int SitesId { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoriesId { get; set; }
        
        public virtual Categories? Categories { get; set; }
        [Required]
        [Display(Name = "Link")]
        public string? Url { get; set; }
        //[Display(Name = "Username")]
        //public string? ApplicationUserId { get; set; }
        
        public ApplicationUser? ApplicationUser { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public virtual ICollection<Comments>? Comments { get; set; }

    }


}
