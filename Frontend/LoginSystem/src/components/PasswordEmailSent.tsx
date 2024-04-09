import { ArrowBack, Email } from "@mui/icons-material";
import {
  Avatar,
  Box,
  Container,
  CssBaseline,
  Grid,
  Link,
  Typography,
} from "@mui/material";

type PasswordEmailSentProps = {
  email: string;
};

const PasswordEmailSent = ({ email }: PasswordEmailSentProps) => {
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
          We have sent a password reset link to <br />
          {email}
        </Typography>
        <Box
          component="form"
          noValidate
          sx={{ mt: 3 }}
          width="100%"
          textAlign="center"
        >
          {/* <Grid container>
            <Grid item xs>
              Didn't recieve the email?
              <Link
                onClick={resendEmail}
                sx={{ textDecoration: "none", color: "#7d56d4" }}
                variant="body2"
              >
                Click to resend.
              </Link>
            </Grid>
          </Grid> */}
          <Grid container marginTop={2}>
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
};

export default PasswordEmailSent;
