import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { useForm } from "react-hook-form";
import { FormInputText } from "./form/FormInputText";
import { FormInputPassword } from "./form/FormPasswordField";
import { useRegisterMutation } from "../redux/authApi";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import { openSnackbar } from "../redux/snackbarSlice";

export default function Register() {
  const { handleSubmit, register, watch, control } = useForm();
  const [registerApi, { data, error }] = useRegisterMutation();
  const dispatch = useDispatch();
  const [email, setEmail] = useState("");
  const navigate = useNavigate();
  const onSubmit = (data: unknown) => {
    //console.log(data);
    setEmail(data.email);
    registerApi(data as authTypes.loginRegisterParams);
  };

  useEffect(() => {
    if (data?.success)
      navigate(`/auth/confirm-email?mailSent=true&email=${email}`);

    // if (error?.data && !error?.data.success) {
    //   console.log("he");
    //   dispatch(
    //     openSnackbar({ severity: "error", message: error?.data.message })
    //   );
    // }
  }, [data?.data, error?.data.data]);

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
          Sign up
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
            Sign Up
          </Button>
          <Grid container justifyContent="flex-end">
            <Grid item>
              <Link href="/auth/login" variant="body2">
                Already have an account? Sign in
              </Link>
            </Grid>
          </Grid>
        </Box>
      </Box>
    </Container>
  );
}
