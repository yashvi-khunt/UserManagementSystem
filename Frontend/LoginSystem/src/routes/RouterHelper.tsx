import {
  AddUser,
  ChangePassword,
  EmailConfirmSuccess,
  ForgotPassword,
  Layout,
  Login,
  ProfileEdit,
  ProfilePage,
  Register,
  ResetPassword,
  SetPassword,
} from "../components";
import EditUser from "../components/EditUser";
import SendEmail from "../components/auth/SendEmail";
import LoginHistories from "../pages/LoginHistories";
import Users from "../pages/Users";
import { URL } from "../utils/constants/URLConstants";

export const routerHelper: Global.RouteConfig = [
  {
    name: "Profile",
    path: URL.PROFILE,
    element: <ProfilePage />,
    roles: ["Admin", "User"],
    children: [
      {
        path: URL.EDIT,
        element: <ProfileEdit />,
        roles: ["Admin", "User"],
      },
      {
        path: "change-password",
        element: <ChangePassword />,
        roles: ["Admin", "User"],
      },
    ],
  },
  {
    name: "Users",
    path: URL.USERS,
    element: <Users />,
    roles: ["Admin"],
    children: [
      { path: URL.ADD, element: <AddUser />, roles: ["Admin"] },
      {
        path: URL.EDIT + "/:email",
        element: <EditUser />,
        roles: ["Admin"],
      },
      {
        path: URL.DETAILS,
        element: <ProfilePage />,
        roles: ["Admin"],
      },
    ],
  },
  {
    name: "Login History",
    path: "/loginHistories",
    element: <LoginHistories />,
    roles: ["Admin", "User"],
  },
];

export const authRoutes: Global.AuthRoutes = [
  {
    path: "/auth",
    element: null,
    children: [
      {
        path: "login",
        element: <Login />,
      },
      {
        path: "register",
        element: <Register />,
      },
      {
        path: "forgot-password",
        element: <ForgotPassword />,
      },
      {
        path: "reset-password",
        element: <ResetPassword />,
      },
      {
        path: "set-password",
        element: <SetPassword />,
      },
      {
        path: "confirm-email",
        element: <EmailConfirmSuccess />,
      },
      {
        path: "sent-confirm-email",
        element: <SendEmail />,
      },
    ],
  },
];
