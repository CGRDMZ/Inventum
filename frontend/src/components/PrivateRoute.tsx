import { FC } from "react";
import { Navigate, Route, useLocation } from "react-router-dom";
import useAuth from "../context/AuthContext";

interface PropType {
  component: React.FC;
}

const PrivateRoute: FC<PropType> = ({ component: Component }) => {
  const { user } = useAuth();
  const location = useLocation();

  if (user) return <Component />;
  return <Navigate to="/login" state={{from: location}} replace={true} />;
};

export default PrivateRoute;
