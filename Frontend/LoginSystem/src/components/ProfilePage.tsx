import {
  Avatar,
  Badge,
  Box,
  Card,
  Container,
  CssBaseline,
  Grid,
  Link,
  Typography,
} from "@mui/material";
import { useUserDetailsQuery } from "../redux/authApi";
import { useAppSelector } from "../redux/hooks";
import { Edit } from "@mui/icons-material";
import { useNavigate } from "react-router-dom";

const ProfilePage = () => {
  const navigate = useNavigate();
  const userEmail = useAppSelector((state) => state.auth.userEmail);
  console.log(userEmail);
  const { data: userDetails } = useUserDetailsQuery(userEmail ? userEmail : "");

  const firstNameInitial = userDetails?.firstName
    ? userDetails?.firstName[0]
    : "?";
  const lastNameInitial = userDetails?.lastName
    ? userDetails?.lastName[0]
    : "?";

  const initials = `${firstNameInitial}${lastNameInitial}`;

  return (
    <>
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 16,
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            minWidth: "396px",
          }}
        >
          <Card variant="outlined" sx={{ width: "100%" }}>
            <Grid
              container
              direction="column"
              justifyContent="center"
              alignItems="center"
            >
              {firstNameInitial !== "?" && lastNameInitial !== "?" ? (
                <>
                  <Grid item sx={{ p: "1.5rem 0rem", textAlign: "center" }}>
                    {/* PROFILE PHOTO */}
                    <Badge
                      overlap="circular"
                      anchorOrigin={{ vertical: "bottom", horizontal: "right" }}
                      badgeContent={
                        <Edit
                          sx={{
                            border: "5px solid white",
                            backgroundColor: "#ff558f",
                            borderRadius: "50%",
                            padding: ".2rem",
                            width: 35,
                            height: 35,
                            ":hover": { cursor: "pointer" },
                          }}
                          onClick={() => navigate("/profile/edit")}
                        ></Edit>
                      }
                    >
                      <Avatar
                        sx={{
                          width: 100,
                          height: 100,
                          mb: 1.5,
                        }}
                      >
                        {initials}
                      </Avatar>
                    </Badge>

                    {/* DESCRIPTION */}

                    <>
                      <Typography variant="h6">
                        {userDetails?.firstName}
                      </Typography>
                      <Typography color="text.secondary">
                        {userDetails?.lastName}
                      </Typography>
                    </>
                  </Grid>
                </>
              ) : (
                <Grid item sx={{ p: "1.5rem 0rem", textAlign: "center" }}>
                  <Grid item>No user details present.</Grid>
                  <Grid item>
                    <Link href="/profile/edit">
                      Click here to add profile details.
                    </Link>
                  </Grid>
                </Grid>
              )}
            </Grid>
          </Card>
        </Box>
      </Container>
    </>
  );
};

export default ProfilePage;
