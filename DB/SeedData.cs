using LibraryManagementPortal.Entities;
using LibraryManagementPortal.Enums;
using LibraryManagementPortal.Utilities;

namespace LibraryManagementPortal.DB
{
    public static class SeedData
    {
        public static IEnumerable<Book> SeedingBooks
        {
            get
            {
                IEnumerable<Book> result = new List<Book>() {
                    new Book {
                        BookId = 1,
                        BookName = "A Book (Vol.1)",
                        BookAuthor = "Someone",
                        CategoryId = 1
                    },
                    new Book {
                        BookId = 2,
                        BookName = "Another Book (Vol.1)",
                        BookAuthor = "Another one",
                        CategoryId = 2
                    },
                    new Book {
                        BookId = 3,
                        BookName = "B for Book",
                        BookAuthor = "A Teacher",
                        CategoryId = 3
                    },
                    new Book {
                        BookId = 4,
                        BookName = "A for Another Book",
                        BookAuthor = "A Teacher",
                        CategoryId = 3
                    },
                    new Book {
                        BookId = 5,
                        BookName = "Another Book (Vol.2)",
                        BookAuthor = "Another one",
                        CategoryId = 2
                    },
                    new Book {
                        BookId = 6,
                        BookName = "A Book (Vol.2)",
                        BookAuthor = "Someone",
                        CategoryId = 1
                    },
                    new Book {
                        BookId = 7,
                        BookName = "A Book (Vol.3)",
                        BookAuthor = "Someone",
                        CategoryId = 1
                    },
                    new Book {
                        BookId = 8,
                        BookName = "Another Book (Vol.3)",
                        BookAuthor = "Another one",
                        CategoryId = 2
                    },
                    new Book {
                        BookId = 9,
                        BookName = "C for Category of Books",
                        BookAuthor = "A Teacher",
                        CategoryId = 3
                    },
                };
                return result;
            }
        }

        public static IEnumerable<Category> SeedingCategories
        {
            get
            {
                IEnumerable<Category> result = new List<Category>() {
                    new Category() {
                        CategoryId = 1,
                        CategoryName = "Philosophy",
                        CategoryDescription = "Books about Philosophy"
                    },
                    new Category() {
                        CategoryId = 2,
                        CategoryName = "Humor",
                        CategoryDescription = "Books containing a collection of jokes"
                    },
                    new Category() {
                        CategoryId = 3,
                        CategoryName = "Education",
                        CategoryDescription = "Books for teaching"
                    },
                };
                return result;
            }
        }

        public static IEnumerable<User> SeedingUsers
        {
            get
            {
                IEnumerable<User> result = new List<User>() {
                    new User() {
                        UserId = 1,
                        UserName = "admin",
                        Password = EncryptionUtils.Base64Encode("password"),
                        FirstName = "Admin",
                        LastName = "Istrator",
                        Role= UserAccessLevel.Super
                    },
                    new User() {
                        UserId = 2,
                        UserName = "trunganhdo",
                        Password = EncryptionUtils.Base64Encode("trunganhdo"),
                        FirstName = "Do",
                        LastName = "Trung Anh"
                    },
                    new User() {
                        UserId = 3,
                        UserName = "user",
                        Password = EncryptionUtils.Base64Encode("user"),
                        FirstName = "User",
                        LastName = "Anon"
                    },
                };
                return result;
            }
        }

        public static IEnumerable<BorrowRequest> SeedingBorrowRequests
        {
            get
            {
                IEnumerable<BorrowRequest> result = new List<BorrowRequest>()
                {
                    new BorrowRequest() {
                        RequestId = 1,
                        RequestDate = new DateTime(2022, 03, 20),
                        RequestStatus = BorrowRequestStatus.Pending,
                        RequestedByUserId = 2
                    },
                    new BorrowRequest() {
                        RequestId = 2,
                        RequestDate = new DateTime(2022, 03, 20),
                        RequestStatus = BorrowRequestStatus.Approved,
                        RequestedByUserId = 3,
                        ProcessedByUserId = 1
                    },
                    new BorrowRequest() {
                        RequestId = 3,
                        RequestDate = new DateTime(2022, 03, 20),
                        RequestStatus = BorrowRequestStatus.Rejected,
                        RequestedByUserId = 3,
                        ProcessedByUserId = 1
                    },
                };
                return result;
            }
        }

        public static IEnumerable<BorrowRequestDetails> SeedingBorrowRequestDetails
        {
            get
            {
                IEnumerable<BorrowRequestDetails> result = new List<BorrowRequestDetails>()
                {
                    new BorrowRequestDetails() {BorrowRequestId = 1, BookId = 1},
                    new BorrowRequestDetails() {BorrowRequestId = 1, BookId = 2},
                    new BorrowRequestDetails() {BorrowRequestId = 1, BookId = 3},
                    new BorrowRequestDetails() {BorrowRequestId = 1, BookId = 4},
                    new BorrowRequestDetails() {BorrowRequestId = 2, BookId = 5},
                    new BorrowRequestDetails() {BorrowRequestId = 3, BookId = 6},
                };
                return result;
            }
        }
    }
}