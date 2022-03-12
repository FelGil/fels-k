namespace Projekt.Models
{
    public class Categories
    {
        //Properties
        public int CategoriesId { get; set; }
        public string? Category { get; set; }
        public virtual ICollection<Sites>? Sites { get; set; }
    }
}
