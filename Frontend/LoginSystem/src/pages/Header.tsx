import React, { useState } from "react";
import {
  AppBar,
  Avatar,
  Box,
  Divider,
  IconButton,
  Toolbar,
  Typography,
  Menu,
  MenuItem,
} from "@mui/material";
import { Menu as MenuIcon } from "@mui/icons-material";

import { useNavigate } from "react-router-dom";
import { logout } from "../redux/authSlice";
import { useAppDispatch } from "../redux/hooks";

const Header = () => {
  // const [mobileOpen, setMobileOpen] = useState(false);
  // const [isClosing, setIsClosing] = useState(false);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  //   const user = useAppSelector((state) => state.auth.userData);
  const dispatch = useAppDispatch();
  const navigate = useNavigate();

  const navigateToProfile = () => {
    navigate("/profile");
  };

  // const handleDrawerToggle = () => {
  //   if (!isClosing) {
  //     setMobileOpen(!mobileOpen);
  //   }
  // };

  const handleAvatarMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleAvatarMenuClose = () => {
    setAnchorEl(null);
  };

  const handleLogout = () => {
    dispatch(logout());
    navigate("/profile");
    location.reload();
  };

  return (
    <>
      <AppBar
        elevation={0}
        position="fixed"
        sx={{
          bgcolor: "common.white",
        }}
      >
        <Toolbar>
          {/* <IconButton
            aria-label="open drawer"
            edge="start"
            // onClick={handleDrawerToggle}
            sx={{ mr: 2, display: { md: "none" } }}
          >
            <MenuIcon />
          </IconButton> */}
          <Typography
            variant="h6"
            color="initial"
            noWrap
            component="div"
            sx={{ flexGrow: 1 }}
          >
            Welcome
          </Typography>
          <Box>
            <Avatar sx={{ ml: 2 }} onClick={handleAvatarMenuOpen}></Avatar>
            <Menu
              anchorEl={anchorEl}
              open={Boolean(anchorEl)}
              onClose={handleAvatarMenuClose}
            >
              <MenuItem onClick={navigateToProfile}>Profile</MenuItem>
              <MenuItem onClick={handleLogout}>Logout</MenuItem>
            </Menu>
          </Box>
        </Toolbar>
        <Divider />
      </AppBar>
    </>
  );
};

export default Header;
