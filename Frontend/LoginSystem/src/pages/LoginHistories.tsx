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

const LoginHistories = () => {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const { data } = useGetLoginHistoriesQuery({
    ...Object.fromEntries(searchParams.entries()),
  });

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
        dayjs(row.dateTime).format("DD/MM/YYYY"),
      width: 150,
    },
    {
      field: "time",
      headerName: "Time",
      renderCell: ({ row }: GridRenderCellParams) =>
        dayjs(row.dateTime).format("h:mm A"),
      width: 150,
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
            {/* {userRole !== "User" && (
							<Box sx={{ width: "100%" }}>
								<AutoCompleteField
									options={employeeDD?.data || []}
									label='User'
									multiple
								/>
							</Box>
						)} */}
          </Box>
        </Table>
      </Container>
    </>
  );
};

export default LoginHistories;
