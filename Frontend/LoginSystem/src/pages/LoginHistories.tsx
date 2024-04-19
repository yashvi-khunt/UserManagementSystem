import { useNavigate, useSearchParams } from "react-router-dom";
import {
  GridActionsCellItem,
  GridColDef,
  GridRenderCellParams,
  GridValidRowModel,
} from "@mui/x-data-grid";

import dayjs from "dayjs";
import { Box, Button, Container, Typography } from "@mui/material";
import { URL } from "../utils/constants/URLConstants";
import { useAppSelector } from "../redux/hooks";
import Table from "../components/dynamicTable/DynamicTable";

import { useGetLoginHistoriesQuery } from "../redux/api/loginHistory";
import AutoCompleteField from "../components/dynamicTable/AutoCompleteField";
import DatePickerField from "../components/dynamicTable/DatePickerField";
import { useUsersWithNamesQuery } from "../redux/api/userApi";
import SearchField from "../components/dynamicTable/SearchField";

const LoginHistories = () => {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const { data } = useGetLoginHistoriesQuery({
    ...Object.fromEntries(searchParams.entries()),
  });

  const { data: userDD } = useUsersWithNamesQuery();

  const userRole = useAppSelector((state) => state.auth.userData?.role);

  const columns: GridColDef[] = [
    {
      field: "userName",
      headerName: "User",
      width: 150,
    },
    {
      field: "date",
      headerName: "Date",
      renderCell: ({ row }: GridRenderCellParams) =>
        dayjs(row.dateTime).format("DD/MM/YYYY HH:mm A"),
      width: 200,
    },
    {
      field: "ipAddress",
      headerName: "IP Address",
      renderCell: ({ value }: GridRenderCellParams) =>
        value === "::1" ? "Local Host" : value,
      width: 150,
    },
    {
      field: "browser",
      headerName: "Browser",
      width: 150,
    },
    {
      field: "os",
      headerName: "OS",
      width: 150,
    },
    {
      field: "device",
      headerName: "Device",
      renderCell: ({ value }: GridRenderCellParams) => {
        return value || "-";
      },
      width: 150,
    },
  ];

  const pageInfo: DynamicTable.TableProps = {
    columns: columns,
    rows: data?.data.loginHistories as GridValidRowModel[] | undefined,
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
            Login Histories
          </Typography>
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

export default LoginHistories;
