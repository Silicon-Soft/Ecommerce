using System.ComponentModel.DataAnnotations;


    public class ViewCategoryVM
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

