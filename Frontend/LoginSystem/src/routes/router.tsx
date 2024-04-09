import {
  Route,
  createBrowserRouter,
  createRoutesFromElements,
} from "react-router-dom";
import Profile from "../pages/Profile";
import { ForgotPassword, Login, Protected, Register } from "../components";
import ResetPassword from "../components/ResetPassword";
import ProfileEdit from "../components/ProfileEdit";
import ProfilePage from "../components/ProfilePage";
import EmailConfirmSuccess from "../components/EmailConfirmSuccess";

const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/">
      <Route
        path="/"
        element={
          <Protected>
            <Profile />
          </Protected>
        }
      />
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
            <Profile />
          </Protected>
        }
      >
        <Route
          path="/profile"
          element={
            <Protected>
              <ProfilePage />
            </Protected>
          }
        />
        <Route
          path="/profile/edit"
          element={
            <Protected>
              <ProfileEdit />
            </Protected>
          }
        />
      </Route>
    </Route>
  )
);

export default router;
