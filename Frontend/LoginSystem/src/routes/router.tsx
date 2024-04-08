import {
  Route,
  createBrowserRouter,
  createRoutesFromElements,
} from "react-router-dom";
import Profile from "../pages/Profile";
import { ForgotPassword, Login, Protected, Register } from "../components";
import ResetPassword from "../components/ResetPassword";

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
        <Route path="confirm-email" />
      </Route>
      <Route path="profile">
        <Route path="edit/:id" />
      </Route>
    </Route>
  )
);

export default router;
