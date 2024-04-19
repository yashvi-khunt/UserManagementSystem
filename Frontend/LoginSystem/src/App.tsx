import { Provider } from "react-redux";
import { store } from "./redux/store";
import { SnackBarComponent } from "./components/index";
import { CssBaseline, ThemeProvider } from "@mui/material";
import theme from "./theme";
import { RouterProvider } from "react-router-dom";
import router from "./routes/router";
import AppRouter from "./routes/AppRouter";

function App() {
  return (
    <Provider store={store}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        {/* <RouterProvider router={router} /> */}
        <AppRouter />
        <SnackBarComponent />
      </ThemeProvider>
    </Provider>
  );
}

export default App;
