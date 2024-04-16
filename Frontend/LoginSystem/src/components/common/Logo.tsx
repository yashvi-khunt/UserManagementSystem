import { Box } from "@mui/material";
import AppLogo from "../../assets/weyBee.png";

const Logo = () => {
  return (
    <Box
      component="img"
      sx={{
        width: "130px",
        m: "auto",
      }}
      alt="WeyBee"
      src={AppLogo}
    />
  );
};

export default Logo;
