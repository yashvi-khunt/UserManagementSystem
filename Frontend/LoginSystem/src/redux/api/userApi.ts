import { indexApi } from "./indexApi";

export const userApi = indexApi.injectEndpoints({
  endpoints: (builder) => ({
    userDetails: builder.query<authTypes.userDetails, void>({
      query: () => ({
        url: `User/details`,
        method: "GET",
      }),
      providesTags: ["User"],
    }),
    changePassword: builder.mutation<
      authTypes.apiResponse,
      authTypes.loginRegisterParams
    >({
      query: (data) => ({
        url: "User/change-password",
        method: "PUT",
        body: data,
      }),
    }),
    getUserList: builder.query<
      ApiTypes.GetUsersProps,
      ApiTypes.GetUserQueryParams
    >({
      query: (data) => ({
        url: "User/GetUsers",
        params: data,
      }),
      providesTags: ["User"],
    }),
    editUser: builder.mutation<
      authTypes.apiResponse,
      authTypes.updateUserProps
    >({
      query: (data) => ({
        url: `User/edit`,
        method: "PUT",
        body: { firstName: data.firstName, lastName: data.lastName },
      }),
      invalidatesTags: ["User"],
    }),
    addUser: builder.mutation<
      authTypes.apiResponse,
      authTypes.forgotPasswordParams
    >({
      query: (data) => ({
        url: "User/add-user",
        method: "POST",
        body: data,
      }),
      invalidatesTags: ["User"],
    }),
  }),
});

export const {
  useChangePasswordMutation,
  useUserDetailsQuery,
  useGetUserListQuery,
  useEditUserMutation,
  useAddUserMutation,
} = userApi;
