using LibraryManagementPortal.DTO;
using LibraryManagementPortal.Entities;
using LibraryManagementPortal.Enums;

namespace LibraryManagementPortal.Utilities
{
    public static class DataConvertUtils
    {
        public static UserDTO UserEntityToDTOComplete(this User entity)
        {
            UserDTO result = new UserDTO
            {
                UserId = entity.UserId,
                UserName = entity.UserName,
                Password = entity.Password,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Role = entity.Role.ToString(),
                BorrowRequests = entity.BorrowRequests != null
                    ? entity.BorrowRequests.Select(br => br.BorrowRequestEntityToDTOComplete()).ToList()
                    : null
            };
            return result;
        }

        public static UserDTO UserEntityToDTOSimple(this User entity)
        {
            UserDTO result = new UserDTO
            {
                UserId = entity.UserId,
                UserName = entity.UserName,
                Password = entity.Password,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Role = entity.Role.ToString(),
            };
            return result;
        }

        public static CategoryDTO CategoryEntityToDTO(this Category entity)
        {
            CategoryDTO result = new CategoryDTO
            {
                CategoryId = entity.CategoryId,
                CategoryName = entity.CategoryName,
                CategoryDescription = entity.CategoryDescription
            };
            return result;
        }

        public static Category CategoryDTOToEntity(this CategoryDTO dto)
        {
            if (dto == null)
                return new Category();
            Category result = new Category
            {
                CategoryId = dto.CategoryId,
                CategoryName = dto.CategoryName,
                CategoryDescription = dto.CategoryDescription
            };
            return result;
        }

        public static BookDTO BookEntityToDTOComplete(this Book entity)
        {
            BookDTO result = new BookDTO
            {
                BookId = entity.BookId,
                BookName = entity.BookName,
                BookAuthor = entity.BookAuthor,
                CategoryId = entity.CategoryId,
                Category = entity.Category != null ? entity.Category.CategoryEntityToDTO() : null
            };
            return result;
        }

        public static BookDTO BookEntityToDTOSimple(this Book entity)
        {
            BookDTO result = new BookDTO
            {
                BookId = entity.BookId,
                BookName = entity.BookName,
                BookAuthor = entity.BookAuthor,
                CategoryId = entity.CategoryId,
            };
            return result;
        }

        public static Book BookDTOToEntity(this BookDTO dto)
        {
            Book result = new Book
            {
                BookId = dto.BookId,
                BookName = dto.BookName,
                BookAuthor = dto.BookAuthor,
                CategoryId = dto.CategoryId
            };
            return result;
        }

        public static BorrowRequestDTO BorrowRequestEntityToDTOComplete(this BorrowRequest entity)
        {
            BorrowRequestDTO result = new BorrowRequestDTO
            {
                RequestId = entity.RequestId,
                RequestStatus = entity.RequestStatus.ToString(),
                RequestDate = entity.RequestDate.ToString("dd/MM/yyyy"),
                RequestedByUserId = entity.RequestedByUserId,
                RequestedUser = entity.RequestedUser != null
                    ? entity.RequestedUser.UserEntityToDTOSimple()
                    : null,
                ProcessedByUserId = entity.ProcessedByUserId != null ? (int)entity.ProcessedByUserId : null,
                ProcessedUser = entity.ProcessedUser != null
                    ? entity.ProcessedUser.UserEntityToDTOSimple()
                    : null,
                RequestDetails = entity.RequestDetails != null
                    ? entity.RequestDetails.Select(rd => rd.BorrowRequestDetailsEntityToDTOComplete()).ToList()
                    : null,
            };
            return result;
        }

        public static BorrowRequestDTO BorrowRequestEntityToDTOSimple(this BorrowRequest entity)
        {
            BorrowRequestDTO result = new BorrowRequestDTO
            {
                RequestId = entity.RequestId,
                RequestStatus = entity.RequestStatus.ToString(),
                RequestDate = entity.RequestDate.ToString("dd/MM/yyyy"),
                RequestedByUserId = entity.RequestedByUserId,
                ProcessedByUserId = entity.ProcessedByUserId,
            };
            return result;
        }

        public static BorrowRequest BorrowRequestDTOToEntity(this BorrowRequestDTO dto)
        {
            BorrowRequestStatus enumParseResult;
            DateTime dateTimeParseResult;
            BorrowRequest result = new BorrowRequest
            {
                RequestId = dto.RequestId,
                RequestStatus = Enum.TryParse(dto.RequestStatus, out enumParseResult)
                    ? enumParseResult
                    : BorrowRequestStatus.Pending,
                RequestDate = DateTime.TryParse(dto.RequestDate, out dateTimeParseResult)
                    ? dateTimeParseResult
                    : DateTime.Now,
                RequestedByUserId = dto.RequestedByUserId,
                ProcessedByUserId = dto.ProcessedByUserId,
            };
            return result;
        }

        public static BorrowRequestDetailsDTO BorrowRequestDetailsEntityToDTOComplete(this BorrowRequestDetails entity)
        {
            BorrowRequestDetailsDTO result = new BorrowRequestDetailsDTO
            {
                BookId = entity.BookId,
                BorrowRequestId = entity.BorrowRequestId,
                Book = entity.Book != null
                    ? entity.Book.BookEntityToDTOSimple()
                    : null,
                BorrowRequest = entity.BorrowRequest != null
                    ? entity.BorrowRequest.BorrowRequestEntityToDTOSimple()
                    : null
            };
            return result;
        }

        public static BorrowRequestDetailsDTO BorrowRequestDetailsEntityToDTOSimple(this BorrowRequestDetails entity)
        {
            BorrowRequestDetailsDTO result = new BorrowRequestDetailsDTO
            {
                BookId = entity.BookId,
                BorrowRequestId = entity.BorrowRequestId,
            };
            return result;
        }

        public static BorrowRequestDetails BorrowRequestDetailsDTOToEntity(this BorrowRequestDetailsDTO dto)
        {
            BorrowRequestDetails result = new BorrowRequestDetails
            {
                BookId = dto.BookId,
                BorrowRequestId = dto.BorrowRequestId
            };
            return result;
        }
    }
}