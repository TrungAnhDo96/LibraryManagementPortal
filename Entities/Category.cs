using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementPortal.Entities
{
    public class Category
    {
        public Category() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int CategoryId { get; set; }

        [Required]
        public string? CategoryName { get; set; }

        public string? CategoryDescription { get; set; }

        public IEnumerable<Book>? Books { get; set; }
    }
}