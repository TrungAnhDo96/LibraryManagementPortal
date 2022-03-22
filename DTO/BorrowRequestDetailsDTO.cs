namespace LibraryManagementPortal.DTO
{
    public class BorrowRequestDetailsDTO
    {
        public int BookId { get; set; }
        public BookDTO? Book { get; set; }
        public int BorrowRequestId { get; set; }
        public BorrowRequestDTO? BorrowRequest { get; set; }
    }
}