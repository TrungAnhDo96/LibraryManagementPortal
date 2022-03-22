namespace LibraryManagementPortal.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Role { get; set; }
        public List<BorrowRequestDTO>? BorrowRequests { get; set; }
    }
}