import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { FormInputText, FormInputPassword } from "../index";
import { useForm } from "react-hook-form";
import { useLoginMutation } from "../../redux/api/authApi";
import { login } from "../../redux/slice/authSlice";
import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";

export default function Login() {
  const { handleSubmit, register, control } = useForm();
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [loginApi, { data: loginResponse, error: loginError }] =
    useLoginMutation();
  const [error, setError] = useState(null);

  const onSubmit = (data: unknown) => {
    loginApi(data as authTypes.loginRegisterParams);
  };

  useEffect(() => {
    setError(loginError?.data.message);
  }, [loginError]);

  useEffect(() => {
    if (loginResponse?.success) {
      dispatch(login(loginResponse.data));
      navigate("/");
    }
  }, [loginResponse]);

  const clearError = () => {
    setError(null);
  };

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
            <LockOutlinedIcon htmlColor="#7d56d4" />
          </Avatar>
        </Avatar>
        <Typography component="h1" variant="h5">
          Sign in
        </Typography>
        <Box
          component="form"
          noValidate
          onSubmit={handleSubmit(onSubmit)}
          sx={{ mt: 3 }}
          onChange={clearError}
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
            {error && (
              <Grid item xs={12} textAlign="center" color="red">
                {error}
              </Grid>
            )}
          </Grid>
          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2, bgcolor: "#7d56d4" }}
          >
            Sign in
          </Button>
          <Grid container>
            <Grid item xs>
              <Link href="/auth/forgot-password" variant="body2">
                Forgot password?
              </Link>
            </Grid>
            <Grid item>
              <Link href="/auth/register" variant="body2">
                {"Don't have an account? Sign Up"}
              </Link>
            </Grid>
          </Grid>
        </Box>
      </Box>
    </Container>
  );
}
