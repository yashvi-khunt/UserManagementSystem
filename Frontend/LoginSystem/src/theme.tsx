import { Theme, createTheme } from "@mui/material/styles";

const theme: Theme = createTheme({
	palette: {
		background: {
			default: "#ffffff",
			// paper: "#f9fafb",
		},
	},
	typography: {
		button: {
			textTransform: "none",
		},
	},
});

export default theme;
