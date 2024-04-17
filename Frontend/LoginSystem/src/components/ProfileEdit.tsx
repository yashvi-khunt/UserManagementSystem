import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { FormInputText } from "./index";
import { useForm } from "react-hook-form";
import { useEditUserMutation, useUserDetailsQuery } from "../redux/api/userApi";
import { useNavigate } from "react-router-dom";
import { ArrowBack, Edit } from "@mui/icons-material";
import { useAppDispatch, useAppSelector } from "../redux/hooks";
import { useEffect } from "react";
import { Link } from "@mui/material";
import { openSnackbar } from "../redux/slice/snackbarSlice";

export default function ProfileEdit() {
  const { handleSubmit, register, control } = useForm();
  const navigate = useNavigate();
  const [updateApi, { data: updateResponse, error }] = useEditUserMutation();
  const dispatch = useAppDispatch();

  // const userEmail = useAppSelector((state) => state.auth.userData?.email);
  const { data: userDetails } = useUserDetailsQuery();

  const onSubmit = (data: object) => {
    updateApi({
      ...data,
    } as authTypes.updateUserProps);
  };

  useEffect(() => {
    if (updateResponse?.success) {
      dispatch(
        openSnackbar({
          severity: "success",
          message: updateResponse.message,
        })
      );
      navigate("/profile");
    }
  }, [updateResponse?.data]);

  return (
    <Container maxWidth="xs">
      <CssBaseline />
      <Box
        sx={{
          marginTop: 16,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <Avatar sx={{ m: 1, bgcolor: "#f8f5fe", width: 60, height: 60 }}>
          <Avatar sx={{ m: 1, bgcolor: "#f5ecfe" }}>
            <Edit htmlColor="#7d56d4" />
          </Avatar>
        </Avatar>
        <Typography component="h1" variant="h5">
          Edit Profile
        </Typography>
        <Box
          component="form"
          noValidate
          onSubmit={handleSubmit(onSubmit)}
          sx={{ mt: 3 }}
        >
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <FormInputText
                control={control}
                value={userDetails?.firstName}
                {...register("firstName", {
                  pattern: {
                    value: /^[a-zA-Z]*$/,
                    message: "Name should contain alphabets only.",
                  },
                })}
                label="First name"
              />
            </Grid>
            <Grid item xs={12}>
              <FormInputText
                control={control}
                value={userDetails?.lastName}
                {...register("lastName", {
                  pattern: {
                    value: /^[a-zA-Z]*$/,
                    message: "Name should contain alphabets only.",
                  },
                })}
                label="Last name"
              />
            </Grid>

            {error && (
              <Grid item xs={12} textAlign="center" color="red">
                {error?.data.message}
              </Grid>
            )}
          </Grid>

          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2, bgcolor: "#7d56d4" }}
          >
            Edit Profile
          </Button>
          <Grid container>
            <Grid item xs>
              <Link
                href="/profile"
                sx={{ textDecoration: "none", color: "gray" }}
                variant="body2"
              >
                <Box justifyContent="center" display="flex" gap={0.2}>
                  <ArrowBack fontSize="small" color="inherit" /> Back to profile
                </Box>
              </Link>
            </Grid>
          </Grid>
        </Box>
      </Box>
    </Container>
  );
}
