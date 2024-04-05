import {
  Route,
  createBrowserRouter,
  createRoutesFromElements,
} from "react-router-dom";
import Profile from "../pages/Profile";
import { Login, Protected, Register } from "../components";

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
        <Route path="reset-password" />
        <Route path="forgot-password" />
        <Route path="confirm-email" />
      </Route>
      <Route path="profile">
        <Route path="edit/:id" />
      </Route>
    </Route>
  )
);

export default router;
