import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { FormInputText } from "./form/FormInputText";
import { useForm } from "react-hook-form";
import { useForgotPasswordMutation } from "../redux/authApi";
import { ArrowBack, KeyRounded } from "@mui/icons-material";
import { useEffect, useState } from "react";
import PasswordEmailSent from "./PasswordEmailSent";
import { useDispatch } from "react-redux";
import { openSnackbar } from "../redux/snackbarSlice";

export default function Login() {
  const { handleSubmit, register, control } = useForm();

  const [mailSent, setMailSent] = useState(false);
  const [edata, setData] = useState<authTypes.forgotPasswordParams>({
    email: "",
  });

  const [forgotPasswordApi, { data, error }] = useForgotPasswordMutation();
  const dispatch = useDispatch();

  const onSubmit = (data: unknown) => {
    //console.log(data as authTypes.forgotPasswordParams);
    setData(data as authTypes.forgotPasswordParams);
    forgotPasswordApi(data as authTypes.forgotPasswordParams);
  };

  useEffect(() => {
    setMailSent(false);
  }, []);

  useEffect(() => {
    if (data?.success) setMailSent(true);
  }, [data?.data]);

  useEffect(() => {
    // console.log(error?.data.message);
    dispatch(
      openSnackbar({
        severity: "error",
        message: error?.data.message,
      })
    );
  }, [error?.data]);

  return !mailSent ? (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <Box
        sx={{
          marginTop: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          width: "100%",
        }}
      >
        <Avatar sx={{ m: 1, bgcolor: "#f8f5fe", width: 60, height: 60 }}>
          <Avatar sx={{ m: 1, bgcolor: "#f5ecfe" }}>
            <KeyRounded htmlColor="#7d56d4" />
          </Avatar>
        </Avatar>
        <Typography component="h1" variant="h5">
          Forgot Password
        </Typography>
        <Typography component="h6" variant="body2" marginTop={1}>
          No worries, we'll send you reset instructions.
        </Typography>
        <Box
          component="form"
          noValidate
          onSubmit={handleSubmit(onSubmit)}
          sx={{ mt: 3 }}
          width="100%"
        >
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <FormInputText
                control={control}
                {...register("email", {
                  required: {
                    value: true,
                    message: "Email field is required.",
                  },
                  pattern: {
                    value: /^\S+@\S+\.\S+$/,
                    message: "Please enter a valid email address.",
                  },
                })}
                label="Email"
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
  ) : (
    <PasswordEmailSent email={edata.email} />
  );
}
