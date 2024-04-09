import { createSlice } from "@reduxjs/toolkit";
import { jwtDecode } from "jwt-decode";

const tokenFields = {
  email: "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress",
};

let data: { token: string | null; email: string | null } =
  localStorage.getItem("user");
data = data ? JSON.parse(data) : null;

const initialState = {
  status: data === null ? false : true,
  userToken: data?.token,
  userEmail: data?.email,
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    login: (state, action) => {
      console.log(action.payload);
      state.status = true;
      state.userToken = action.payload;

      const decode = jwtDecode(action.payload);

      const user = {
        token: action.payload,
        email: decode[tokenFields.email],
      };
      console.log(user);
      state.userEmail = user.email;
      // Save user data to local storage
      localStorage.setItem("user", JSON.stringify(user));
    },
    logout: (state) => {
      state.status = false;
      state.userToken = null;
      state.userEmail = null;
      // Clear user data from local storage
      localStorage.removeItem("user");
    },
  },
});

export const { login, logout } = authSlice.actions;
export default authSlice.reducer;
