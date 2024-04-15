import { Box, Fab, Fade, Toolbar } from "@mui/material";
import KeyboardArrowUpIcon from "@mui/icons-material/KeyboardArrowUp";
import useScrollToTop from "../../hooks/useScrollToTop";
import { Outlet, useLocation } from "react-router-dom";
import { URL } from "../../utils/constants/URLConstants";
import Header from "../../pages/Header";

const Layout = () => {
  const { isVisible, handleClick } = useScrollToTop();
  const location = useLocation();
  const isAuth = location.pathname === URL.AUTH;
  return (
    <>
      <Box sx={{ display: "flex" }}>
        {isAuth ? (
          <Outlet />
        ) : (
          <>
            <Header />
            <Box sx={{ p: 4, width: "100%" }}>
              <Toolbar />
              <Outlet />
            </Box>
          </>
        )}

        <Fade in={isVisible}>
          <Box
            onClick={handleClick}
            role="presentation"
            sx={{ position: "fixed", bottom: 16, right: 16 }}
          >
            <Fab size="small" aria-label="scroll back to top">
              <KeyboardArrowUpIcon />
            </Fab>
          </Box>
        </Fade>
      </Box>
    </>
  );
};

export default Layout;
