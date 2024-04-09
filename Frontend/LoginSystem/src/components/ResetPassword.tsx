import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { useForm } from "react-hook-form";
import { FormInputPassword } from "./form/FormPasswordField";
import { useResetPasswordMutation } from "../redux/authApi";
import { useEffect } from "react";
import { ArrowBack, KeyRounded } from "@mui/icons-material";
import { useNavigate, useSearchParams } from "react-router-dom";
import { useAppDispatch } from "../redux/hooks";
import { openSnackbar } from "../redux/snackbarSlice";

export default function ResetPassword() {
  const { handleSubmit, register, watch, control } = useForm();
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const [resetApi, { data, error }] = useResetPasswordMutation();
  const [searchParams] = useSearchParams();

  const onSubmit = (data: unknown) => {
    resetApi({
      newPassword: data.password,
      email: searchParams.get("userEmail"),
      token: searchParams.get("token")?.split(" ").join("+"),
    } as authTypes.resetPasswordParams);
  };

  useEffect(() => {
    if (data?.success) {
      dispatch(
        openSnackbar({
          severity: "success",
          message: data.message,
        })
      );
      navigate("/auth/login");
    }
  }, [data?.data, error?.data]);

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <Box
        sx={{
          marginTop: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <Avatar sx={{ m: 1, bgcolor: "#f8f5fe", width: 60, height: 60 }}>
          <Avatar sx={{ m: 1, bgcolor: "#f5ecfe" }}>
            <KeyRounded htmlColor="#7d56d4" />
          </Avatar>
        </Avatar>
        <Typography component="h1" variant="h5">
          Set new password
        </Typography>
        <Box
          component="form"
          noValidate
          onSubmit={handleSubmit(onSubmit)}
          sx={{ mt: 3 }}
        >
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <FormInputPassword
                control={control}
                {...register("password", {
                  required: {
                    value: true,
                    message: "Password field is required.",
                  },
                  pattern: {
                    value:
                      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@+._-])[a-zA-Z@+._-\d]{8,}$/,
                    message:
                      "Password should have atleast one uppercase,one lowercase, one special character and should be of the minimum length 8.",
                  },
                })}
                label="Password"
              />
            </Grid>
            <Grid item xs={12}>
              <FormInputPassword
                control={control}
                {...register("confirm-password", { required: true })}
                label="Confirm password"
                {...register("confirm-password", {
                  required: {
                    value: true,
                    message: "Confirm Password field is required.",
                  },
                  pattern: {
                    value:
                      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@+._-])[a-zA-Z@+._-\d]{8,}$/,
                    message:
                      "Password should have atleast one uppercase,one lowercase, one special character and should be of the minimum length 8.",
                  },
                  validate: (val: string) => {
                    if (watch("password") != val) {
                      return "Password and Confirm password should be same.";
                    }
                  },
                })}
              />
            </Grid>
          </Grid>
          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2, bgcolor: "#7d56d4" }}
          >
            Reset Password
          </Button>
          <Grid container>
            <Grid item xs>
              <Link
                href="/auth/login"
                sx={{ textDecoration: "none", color: "gray" }}
                variant="body2"
              >
                <Box justifyContent="center" display="flex" gap={0.2}>
                  <ArrowBack fontSize="small" color="inherit" /> Back to login
                </Box>
              </Link>
            </Grid>
          </Grid>
        </Box>
      </Box>
    </Container>
  );
}
