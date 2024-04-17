import { ArrowBack, Email, TaskAlt } from "@mui/icons-material";
import {
  Avatar,
  Box,
  Button,
  Container,
  CssBaseline,
  Grid,
  Link,
  Typography,
} from "@mui/material";
import { useEffect, useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";

const EmailConfirmSuccess = () => {
  const [searchParams, setSearchParams] = useSearchParams();
  const navigate = useNavigate();
  const [pwdToken, setPwdToken] = useState("");
  const [email, setEmail] = useState("");
  useEffect(() => {
    if (searchParams.get("pwd")) {
      setPwdToken(searchParams.get("pwd"));
      setEmail(searchParams.get("email"));
      searchParams.delete("email");
      searchParams.delete("pwd");
      setSearchParams(searchParams);
    }
  }, [searchParams]);

  console.log(pwdToken, email);

  return (
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
            <TaskAlt htmlColor="#7d56d4" />
          </Avatar>
        </Avatar>
        <Typography component="h1" variant="h5">
          Confirmation Successful
        </Typography>
        <Typography
          textAlign="center"
          component="h6"
          variant="body2"
          marginTop={1}
        >
          Email confirmation completed successfully. <br />
          {pwdToken === ""
            ? "You can now login to you account."
            : "Please set a password for login."}
        </Typography>
        <Box
          component="form"
          noValidate
          sx={{ mt: 3 }}
          width="100%"
          textAlign="center"
        >
          {pwdToken === "" ? null : (
            <Button
              onClick={() => {
                navigate(`/auth/set-password?email=${email}&pwd=${pwdToken}`);
              }}
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2, bgcolor: "#7d56d4" }}
            >
              Set Password
            </Button>
          )}
          <Grid container marginTop={2}>
            <Grid item xs>
              <Link
                href="/auth/login"
                sx={{ textDecoration: "none", color: "gray" }}
                variant="body2"
              >
                <Box justifyContent="center" display="flex" gap={0.2}>
                  <ArrowBack fontSize="small" color="inherit" /> Go to login
                </Box>
              </Link>
            </Grid>
          </Grid>
        </Box>
      </Box>
    </Container>
  );
};
export default EmailConfirmSuccess;
