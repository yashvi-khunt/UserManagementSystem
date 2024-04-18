import { useNavigate, useSearchParams } from "react-router-dom";
import {
  GridActionsCellItem,
  GridColDef,
  GridRenderCellParams,
  GridRowId,
  GridValidRowModel,
} from "@mui/x-data-grid";
import {
  useGetUserListQuery,
  useToggleUserMutation,
  useUsersWithNamesQuery,
} from "../redux/api/userApi";
import dayjs from "dayjs";
import { Avatar, Box, Button, Container, Typography } from "@mui/material";
import { URL } from "../utils/constants/URLConstants";
import { useAppDispatch, useAppSelector } from "../redux/hooks";
import Table from "../components/dynamicTable/DynamicTable";
import AutoCompleteField from "../components/dynamicTable/AutoCompleteField";
import { Edit, InfoOutlined, Person, PersonOff } from "@mui/icons-material";
import { useEffect } from "react";
import { openSnackbar } from "../redux/slice/snackbarSlice";
import DatePickerField from "../components/dynamicTable/DatePickerField";
import StatusComponent from "../components/dynamicTable/StatusComponent";
import SearchField from "../components/dynamicTable/SearchField";

const Users = () => {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const { data } = useGetUserListQuery({
    ...Object.fromEntries(searchParams.entries()),
  });
  const { data: userDD } = useUsersWithNamesQuery();

  const userRole = useAppSelector((state) => state.auth.userData?.role);

  const columns: GridColDef[] = [
    {
      field: "firstName",
      headerName: "First Name",
      renderCell: ({ value }: GridRenderCellParams) => {
        return value || "-";
      },
      width: 150,
    },
    {
      field: "lastName",
      headerName: "Last Name",
      renderCell: ({ value }: GridRenderCellParams) => {
        return value || "-";
      },
      width: 150,
    },
    {
      field: "userName",
      headerName: "User Name",
      width: 150,
      sortable: false,
    },
    {
      field: "role",
      headerName: "Role",
      width: 150,
      sortable: false,
      renderCell: (params: GridRenderCellParams) => (
        <StatusComponent type="leave" {...params} />
      ),
    },
    {
      field: "createdDate",
      headerName: "Created Date",
      renderCell: ({ value }: GridRenderCellParams) =>
        dayjs(value).format("DD/MM/YYYY"),
      width: 150,
    },
    {
      field: "isActivated",
      headerName: "Status",
      renderCell: ({ value }: GridRenderCellParams) => {
        if (value) {
          return "Active";
        } else {
          return "Deactive";
        }
      },
      sortable: false,
    },
    {
      field: "actions",
      type: "actions",
      headerName: "Actions",
      width: 100,
      renderCell: ({ row }) => {
        // console.log(row);
        return (
          <>
            <GridActionsCellItem
              icon={<Edit color="info" />}
              label="Edit"
              className="textPrimary"
              title={"Edit"}
              onClick={() => navigate(`/users/edit/${row.email}`)}
            />
            <GridActionsCellItem
              icon={
                !row.isActivated ? (
                  <Person color={"primary"} />
                ) : (
                  <PersonOff color={"primary"} />
                )
              }
              label="Edit"
              className="textPrimary"
              title={row.isActivated ? "Dectivate" : "Activate"}
              onClick={() => handleUserStatusChange(row.userId)}
            />
            <GridActionsCellItem
              icon={<InfoOutlined color="info" />}
              label="Info"
              className="textPrimary"
              title={"Info"}
              onClick={() => navigate(`/users/details?email=${row.email}`)}
            />
          </>
        );
      },
    },
  ];

  const [toggleApi, { data: toggleData, error: toggleError }] =
    useToggleUserMutation();

  const handleUserStatusChange = (id: GridRowId) => {
    console.log(id);
    toggleApi(id as string);
  };

  const dispatch = useAppDispatch();
  useEffect(() => {
    toggleData?.success &&
      dispatch(
        openSnackbar({ severity: "success", message: toggleData.message })
      );
  }, [toggleData?.data]);

  const pageInfo: DynamicTable.TableProps = {
    columns: columns,
    rows: data?.data.users as GridValidRowModel[] | undefined,
    rowCount: data?.data.count,
  };

  return (
    <>
      <Container maxWidth="xl">
        <Box
          mb={4}
          display="flex"
          justifyContent="space-between"
          alignItems="center"
        >
          <Typography variant="h5" color="initial">
            Users
          </Typography>
          {userRole === "Admin" ? (
            <Box>
              <Button variant="contained" onClick={() => navigate(URL.ADD)}>
                Add
              </Button>
            </Box>
          ) : null}
        </Box>
        <Table {...pageInfo}>
          <Box
            sx={{
              paddingBottom: 2,
              display: "flex",
              justifyContent: "space-between",
              gap: "10px",
            }}
          >
            {userRole !== "User" && (
              <Box sx={{ width: "100%" }}>
                <AutoCompleteField
                  options={userDD?.data || []}
                  label="User"
                  multiple
                />
              </Box>
            )}
            <Box sx={{ width: "100%" }}>
              <SearchField label="Search Text" placeholder="Enter text" />
            </Box>
            <Box sx={{ width: "100%" }}>
              <DatePickerField label="From" />
            </Box>
            <Box sx={{ width: "100%" }}>
              <DatePickerField to label="To" />
            </Box>
          </Box>
        </Table>
      </Container>
    </>
  );
};

export default Users;
