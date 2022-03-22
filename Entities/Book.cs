using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementPortal.Entities
{
    public class Book
    {
        public Book() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int BookId { get; set; }

        [Required]
        public string? BookName { get; set; }
        public string? BookAuthor { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public IEnumerable<BorrowRequestDetails>? RequestDetails { get; set; }
    }
}