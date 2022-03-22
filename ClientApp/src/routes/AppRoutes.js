import AppHomePage from "../pages/AppHomePage";
import LoginPage from "../pages/LoginPage";

export const AppRoutes = [
  {
    name: "Home",
    path: "/",
    page: <AppHomePage />,
  },
  {
    name: "Login",
    path: "login",
    page: <LoginPage />,
  },
];
