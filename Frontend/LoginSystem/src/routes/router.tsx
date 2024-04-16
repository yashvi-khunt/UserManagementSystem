import {
  Route,
  createBrowserRouter,
  createRoutesFromElements,
} from "react-router-dom";
import {
  ForgotPassword,
  Layout,
  Login,
  Protected,
  Register,
} from "../components";
import ResetPassword from "../components/auth/ResetPassword";
import ProfileEdit from "../components/ProfileEdit";
import ProfilePage from "../components/ProfilePage";
import EmailConfirmSuccess from "../components/auth/EmailConfirmSuccess";
import ChangePassword from "../components/ChangePassword";

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/">
      <Route path="/" element={<Layout />} />
      <Route path="auth">
        <Route path="register" element={<Register />} />
        <Route path="login" element={<Login />} />
        <Route path="reset-password" element={<ResetPassword />} />
        <Route path="forgot-password" element={<ForgotPassword />} />
        <Route path="confirm-email" element={<EmailConfirmSuccess />} />
      </Route>
      <Route
        path="profile"
        element={
          <Protected>
            <Layout />
          </Protected>
        }
      >
        <Route
          path=""
          element={
            <Protected>
              <ProfilePage />
            </Protected>
          }
        />
        <Route
          path="profile/edit"
          element={
            <Protected>
              <ProfileEdit />
            </Protected>
          }
        />
        <Route path="profile/change-password" element={<ChangePassword />} />
      </Route>
    </Route>
  )
);

export default router;
