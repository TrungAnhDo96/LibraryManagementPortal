using System.ComponentModel.DataAnnotations;

namespace LibraryManagementPortal.Entities
{
    public class BorrowRequestDetails
    {
        public BorrowRequestDetails() { }

        [Required]
        public int BookId { get; set; }
        public Book? Book { get; set; }

        [Required]
        public int BorrowRequestId { get; set; }
        public BorrowRequest? BorrowRequest { get; set; }
    }
}