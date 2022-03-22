namespace LibraryManagementPortal.DTO
{
    public class BookDTO
    {
        public int BookId { get; set; }
        public string? BookName { get; set; }
        public string? BookAuthor { get; set; }
        public int CategoryId { get; set; }
        public CategoryDTO? Category { get; set; }
    }
}