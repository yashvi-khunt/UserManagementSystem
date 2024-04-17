import { Email, ArrowBack, Mail } from "@mui/icons-material";
import {
  Container,
  CssBaseline,
  Box,
  Avatar,
  Typography,
  Grid,
  Link,
  Button,
  colors,
} from "@mui/material";
import { useEffect, useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";

const SendEmail = () => {
  const [searchParams, setSearchParams] = useSearchParams();
  const [email, setEmail] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    if (searchParams.get("email")) {
      setEmail(searchParams.get("email"));

      searchParams.delete("email");
      setSearchParams(searchParams);
    }
  }, [searchParams]);

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
            <Email htmlColor="#7d56d4" />
          </Avatar>
        </Avatar>
        <Typography component="h1" variant="h5">
          Check your email
        </Typography>
        <Typography
          textAlign="center"
          component="h6"
          variant="body2"
          marginTop={1}
        >
          We have sent a confirmation email to <br />
          {email}
        </Typography>
        <Box
          component="form"
          noValidate
          sx={{ mt: 3 }}
          width="100%"
          textAlign="center"
        >
          <Button
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2, bgcolor: "#7d56d4" }}
          >
            <Mail />
            <Link
              underline="none"
              href="https://mail.google.com"
              sx={{ ml: 1, color: "white" }}
            >
              Open Mail
            </Link>
          </Button>
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

export default SendEmail;
