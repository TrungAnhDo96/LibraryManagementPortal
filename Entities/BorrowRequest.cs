using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagementPortal.Enums;

namespace LibraryManagementPortal.Entities
{
    public class BorrowRequest
    {
        public BorrowRequest() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int RequestId { get; set; }

        public BorrowRequestStatus RequestStatus { get; set; }
        public DateTime RequestDate { get; set; }

        [Required]
        public int RequestedByUserId { get; set; }
        public User? RequestedUser { get; set; }

        public int? ProcessedByUserId { get; set; }
        public User? ProcessedUser { get; set; }

        public IEnumerable<BorrowRequestDetails>? RequestDetails { get; set; }
    }
}