using System.ComponentModel.DataAnnotations;

namespace Projekt.Models
{
    public class Sites
    {
        //Properties
        public int SitesId { get; set; }
        [Required(ErrorMessage = "Please add a Title")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Please add a description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please add a category")]
        [Display(Name = "Category")]
        public int CategoriesId { get; set; }
        
        public Categories? Categories { get; set; }
        [Required(ErrorMessage = "Please add a link")]
        [Display(Name = "Link")]
        public string? Url { get; set; }
        [Display(Name = "Username")]
        public string? ApplicationUserId { get; set; }
        
        public ApplicationUser? ApplicationUser { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public ICollection<Comments>? Comments { get; set; }

    }


}
