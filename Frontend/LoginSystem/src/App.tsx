import { Provider } from "react-redux";
import { store } from "./redux/store";
import SnackBarComponent from "./components/common/SnackBarComponent";
import { CssBaseline, ThemeProvider } from "@mui/material";
import theme from "./theme";

function App() {
  return (
    <Provider store={store}>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        {/* <RouterProvider router={router} />; */}
        <AppRouter />
        <SnackBarComponent />
      </ThemeProvider>
    </Provider>
  );
}

export default App;
