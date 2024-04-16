import { useEffect, useState } from "react";
import {
  Box,
  Divider,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Toolbar,
} from "@mui/material";
import { useNavigate, useLocation } from "react-router-dom";
// import { getIconComponent, pages } from "../../../utils";
import { Logo } from "../index";
import { routerHelper } from "../../routes/RouterHelper";
import { useAppSelector } from "../../redux/hooks";

const SideNav = () => {
  const [selectedItem, setSelectedItem] = useState<number | null>();
  const navigate = useNavigate();
  const location = useLocation();
  const currentPath = location.pathname;
  const userRole = useAppSelector(
    (state) => state.auth.userData?.role
  ) as Global.Role;
  console.log(userRole);
  const handleItemClick = (path: string, index: number) => {
    setSelectedItem(index);
    navigate(path);
  };

  //   useEffect(() => {
  //     setSelectedItem(pages.findIndex((page) => page.path === currentPath));
  //   }, [currentPath]);

  return (
    <Box>
      <Toolbar>
        <Logo />
      </Toolbar>
      <Divider />
      <List>
        {routerHelper
          .filter((route) => route.roles?.includes(userRole))
          .map((page, index) => {
            // const IconComponent = getIconComponent(page.name as string);
            return (
              <ListItem key={page.name} disablePadding>
                <ListItemButton
                  selected={currentPath.includes(page.path.slice(1))}
                  onClick={() => handleItemClick(page.path, index)}
                >
                  {/* <ListItemIcon>{IconComponent}</ListItemIcon> */}
                  <ListItemText primary={page.name} />
                </ListItemButton>
              </ListItem>
            );
          })}
      </List>
    </Box>
  );
};

export default SideNav;
