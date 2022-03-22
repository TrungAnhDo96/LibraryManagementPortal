import BookManagePage from "../pages/BookManagePage";
import BorrowRequestManagePage from "../pages/BorrowRequestManagePage";
import CategoryManagePage from "../pages/CategoryManagePage";

export const SuperUserRoutes = [
  {
    name: "Borrow Requests",
    path: "/",
    page: <BorrowRequestManagePage />,
  },
  {
    name: "Manage Categories",
    path: "categories",
    page: <CategoryManagePage />,
  },
  {
    name: "Manage Books",
    path: "books",
    page: <BookManagePage />,
  },
];
