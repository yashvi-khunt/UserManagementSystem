import { RouterProvider } from "react-router-dom";
import router from "./routes/router";
import { Provider } from "react-redux";
import { store } from "./redux/store";
import SnackBarComponent from "./components/SnackBarComponent";
import Header from "./pages/Header";

function App() {
  return (
    <Provider store={store}>
      <RouterProvider router={router} />;
      <SnackBarComponent />
    </Provider>
  );
}

export default App;
