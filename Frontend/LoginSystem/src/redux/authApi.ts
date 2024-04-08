import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";

export const authApi = createApi({
  reducerPath: "authApi",
  baseQuery: fetchBaseQuery({
    baseUrl: "http://localhost:5041/api/",
  }),
  tagTypes: ["User"],
  endpoints: (builder) => ({
    login: builder.mutation<
      authTypes.loginResponse,
      authTypes.loginRegisterParams
    >({
      query: (data) => ({
        url: "Auth/login",
        method: "POST",
        body: data,
      }),
    }),
    register: builder.mutation<
      authTypes.apiResponse,
      authTypes.loginRegisterParams
    >({
      query: (data) => ({
        url: "Auth/register",
        method: "POST",
        body: data,
      }),
      invalidatesTags: ["User"],
    }),
    confirmEmail: builder.query<
      authTypes.apiResponse,
      authTypes.confirmEmailParams
    >({
      query: ({ id, token }) => ({
        url: `Auth/confirmUserEmail?UserId=${id}&Token=${token}`,
        method: "GET",
      }),
    }),
    forgotPassword: builder.mutation<
      authTypes.apiResponse,
      authTypes.forgotPasswordParams
    >({
      query: (data) => ({
        url: "Auth/forgot-password",
        method: "POST",
        body: data,
      }),
      invalidatesTags: ["User"],
    }),
    resetPassword: builder.mutation<
      authTypes.apiResponse,
      authTypes.resetPasswordParams
    >({
      query: (data) => ({
        url: `Auth/reset-password`,
        method: "POST",
        body: data,
      }),
    }),
  }),
});

export const {
  useLoginMutation,
  useConfirmEmailQuery,
  useForgotPasswordMutation,
  useResetPasswordMutation,
  useRegisterMutation,
} = authApi;
