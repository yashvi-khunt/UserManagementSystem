import { indexApi } from "./indexApi";

export const authApi = indexApi.injectEndpoints({
  endpoints: (builder) => ({
    login: builder.mutation<
      authTypes.apiResponse,
      authTypes.loginRegisterParams
    >({
      query: (data) => ({
        url: "Auth/login",
        method: "POST",
        body: data,
      }),
      invalidatesTags: ["User"],
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
    userDetails: builder.query<authTypes.userDetails, string>({
      query: (email) => ({
        url: `User/details/${email}`,
        method: "GET",
      }),
      providesTags: ["User"],
    }),
    editUser: builder.mutation<
      authTypes.apiResponse,
      authTypes.updateUserProps
    >({
      query: (data) => ({
        url: `Auth/edit/${data.email}`,
        method: "PUT",
        body: { firstName: data.firstName, lastName: data.lastName },
      }),
      invalidatesTags: ["User"],
    }),
    changePassword: builder.mutation<
      authTypes.apiResponse,
      authTypes.loginRegisterParams
    >({
      query: (data) => ({
        url: "Auth/change-password",
        method: "PUT",
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
  useUserDetailsQuery,
  useEditUserMutation,
  useChangePasswordMutation,
} = authApi;
