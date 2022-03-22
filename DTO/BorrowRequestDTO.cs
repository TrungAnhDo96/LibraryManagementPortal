namespace LibraryManagementPortal.DTO
{
    public class BorrowRequestDTO
    {
        public int RequestId { get; set; }
        public string? RequestStatus { get; set; }
        public string? RequestDate { get; set; }
        public int RequestedByUserId { get; set; }
        public UserDTO? RequestedUser { get; set; }
        public int? ProcessedByUserId { get; set; }
        public UserDTO? ProcessedUser { get; set; }
        public List<BorrowRequestDetailsDTO>? RequestDetails { get; set; }
    }
}