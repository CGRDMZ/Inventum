import { Center } from "@chakra-ui/react";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import useAuth from "../context/AuthContext";

const Boards = () => {
  const { isLoading, user } = useAuth();
  const navigate = useNavigate();
  useEffect(() => {
    if (!user && !isLoading) {
      navigate("/login", { replace: true });
    }
  }, [user, isLoading, navigate]);
  return (
    <>
      <Center>Boards page</Center>
    </>
  );
};

export default Boards;
