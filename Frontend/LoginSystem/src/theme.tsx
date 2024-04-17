import { Theme, createTheme } from "@mui/material/styles";

const theme: Theme = createTheme({
  palette: {
    background: {
      default: "#ffffff",
      // paper: "#f9fafb",
    },
    primary: {
      main: "#7d56d4",
      light: "#f5ecfe",
    },
  },
  typography: {
    button: {
      textTransform: "none",
    },
  },
});

export default theme;
