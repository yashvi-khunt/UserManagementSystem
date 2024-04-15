import {
  AddUser,
  EmailConfirmSuccess,
  ForgotPassword,
  Login,
  PasswordEmailSent,
  ProfileEdit,
  ProfilePage,
  Register,
  ResetPassword,
} from "../components";
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
        path: URL.EDIT,
        element: <ProfileEdit />,
        roles: ["Admin"],
      },
    ],
  },
  //   {
  // 	name:"Login History",
  // 	path: URL.LOGINHISTORIES,
  // 	element: <LoginHistories />,
  // 	roles:["Admin","User"]
  //   }
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
        path: "confirm-email",
        element: <EmailConfirmSuccess />,
      },
      {
        path: "sent-password-email/:email",
        element: <PasswordEmailSent />,
      },
    ],
  },
];
