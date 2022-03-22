using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryManagementPortal.Enums;

namespace LibraryManagementPortal.Entities
{
    public class User
    {
        public User() { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required]
        public UserAccessLevel Role { get; set; }

        public IEnumerable<BorrowRequest>? BorrowRequests { get; set; }

        public IEnumerable<BorrowRequest>? ProcessedRequests { get; set; }
    }
}