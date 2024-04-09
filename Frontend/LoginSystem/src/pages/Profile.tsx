import { Outlet } from "react-router-dom";
import Header from "./Header";

const Profile = () => {
  return (
    <>
      <Header />
      <Outlet />
    </>
  );
};

export default Profile;
