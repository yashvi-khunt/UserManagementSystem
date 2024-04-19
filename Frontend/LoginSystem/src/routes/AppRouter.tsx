import { authRoutes, routerHelper } from "./RouterHelper";
import { useEffect, useState } from "react";
import { CircularProgress, Container } from "@mui/material";
import {
  Outlet,
  Route,
  RouterProvider,
  createBrowserRouter,
  createRoutesFromElements,
  useNavigate,
} from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../redux/hooks";
import { login } from "../redux/slice/authSlice";
import { Layout, Login, ProfilePage, Protected } from "../components";
import Profile from "../pages/Profile";

const AppRouter = () => {
  const dispatch = useAppDispatch();
  const userRole: Global.Role = useAppSelector(
    (state) => state.auth.userData?.role
  ) as Global.Role;

  console.log(userRole);
  useEffect(() => {}, []);

  const [isInitialized, setIsInitialized] = useState(false);
  useEffect(() => {
    const localData = localStorage.getItem("userData");
    if (localData) {
      const userData = JSON.parse(localData);
      dispatch(login({ token: userData }));
    }

    setIsInitialized(true);
  }, []);

  if (!isInitialized) {
    return (
      <Container
        maxWidth="lg"
        sx={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          height: "100vh",
        }}
      >
        <CircularProgress />
      </Container>
    );
  }

  const routes = createRoutesFromElements(
    <Route path="/">
      {authRoutes.map((route) => {
        if (route.children && route.children.length > 0) {
          const childRoutes = route.children.map((childRoute) => (
            <Route
              key={childRoute.path}
              path={childRoute.path}
              element={childRoute.element}
            />
          ));
          return (
            <Route key={route.path} path={route.path} element={<Outlet />}>
              <Route path="" element={route.element} />
              {childRoutes}
            </Route>
          );
        } else {
          return (
            <Route key={route.path} path={route.path} element={route.element} />
          );
        }
      })}
      <Route path="/" element={<Layout />}>
        {routerHelper
          .filter((route) => route.roles?.includes(userRole))
          .map((route) => {
            if (route.children && route.children.length > 0) {
              const childRoutes = route.children
                .filter((route) => route.roles.includes(userRole))
                .map((childRoute) => (
                  <Route
                    key={childRoute.path}
                    path={childRoute.path}
                    element={childRoute.element}
                  />
                ));
              return (
                <Route key={route.path} path={route.path} element={<Outlet />}>
                  <Route path="" element={route.element} />
                  {childRoutes}
                </Route>
              );
            } else {
              return (
                <Route
                  key={route.path}
                  path={route.path}
                  element={route.element}
                />
              );
            }
          })}
      </Route>
    </Route>
  );
  const router = createBrowserRouter(routes);

  return <RouterProvider router={router} />;
};

export default AppRouter;
