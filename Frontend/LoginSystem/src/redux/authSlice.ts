import { createSlice } from "@reduxjs/toolkit";

let data = localStorage.getItem("user");
data = data ? JSON.parse(data) : null;

const initialState = {
  status: data === null ? false : true,
  userToken: data,
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    login: (state, action) => {
      console.log(action.payload);
      state.status = true;
      state.userToken = action.payload.token;

      // Save user data to local storage
      localStorage.setItem("user", JSON.stringify(state.userToken));
    },
    logout: (state) => {
      state.status = false;
      state.userToken = null;

      // Clear user data from local storage
      localStorage.removeItem("user");
    },
  },
});

export const { login, logout } = authSlice.actions;
export default authSlice.reducer;
