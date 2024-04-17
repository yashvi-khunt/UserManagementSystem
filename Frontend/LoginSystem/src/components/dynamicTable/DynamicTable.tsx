import { useState, useEffect, PropsWithChildren } from "react";
import {
  DataGrid,
  GridPaginationModel,
  GridSortDirection,
  GridSortItem,
  GridSortModel,
} from "@mui/x-data-grid";
import { Box } from "@mui/material";
import { useSearchParams } from "react-router-dom";
import CustomPagination from "./CustomPagination";
import { useAppSelector } from "../../redux/hooks";

const Table = ({
  children,
  ...props
}: PropsWithChildren<DynamicTable.TableProps>) => {
  const [searchParams, setSearchParams] = useSearchParams();
  const userRole = useAppSelector((state) => state.auth.userData?.role);
  const [paginationModel, setPaginationModel] = useState<GridPaginationModel>({
    page: 0,
    pageSize: 10,
  });
  const [sortModel, setSortModel] = useState<GridSortModel>();

  const updateSearchParams = (model: GridPaginationModel | GridSortItem) => {
    setSearchParams((prevParams) => {
      return new URLSearchParams({
        ...Object.fromEntries(prevParams.entries()),
        ...model,
      } as unknown as URLSearchParams);
    });
  };

  useEffect(() => {
    setPaginationModel({
      page: parseInt(searchParams.get("page") || "") - 1 || 0,
      pageSize: parseInt(searchParams.get("pageSize") || "") || 10,
    });

    const sortField = searchParams.get("field");
    const sortDirection = searchParams.get("sort") as GridSortDirection;

    if (sortField) {
      setSortModel([{ field: sortField, sort: sortDirection } as GridSortItem]);
    }
  }, [searchParams]);

  const handleOnPaginationModelChange = (model: GridPaginationModel) => {
    const page = model.page + 1;
    updateSearchParams({ ...model, page });
    setPaginationModel(model);
  };

  const handleOnSortModelChange = (model: GridSortModel) => {
    const [newModel] = model;
    updateSearchParams(newModel);
    setSortModel(model);
  };

  return (
    <>
      {children}
      <Box
        sx={{
          width: "100%",
          height: "631px",
        }}
      >
        <DataGrid
          columns={props?.columns || []}
          rows={props?.rows || []}
          rowCount={props?.rowCount || 0}
          autoHeight={false}
          pagination
          pageSizeOptions={[5, 10, 25, 50]}
          slots={{
            pagination: CustomPagination,
          }}
          disableRowSelectionOnClick
          disableColumnMenu
          paginationModel={paginationModel}
          sortModel={sortModel}
          paginationMode="server"
          filterMode="server"
          sortingMode="server"
          onPaginationModelChange={handleOnPaginationModelChange}
          onSortModelChange={handleOnSortModelChange}
          columnVisibilityModel={{
            actions: userRole === "Admin",
            userName: userRole === "Admin",
          }}
        />
      </Box>
    </>
  );
};

export default Table;
