import {
  Alert,
  AlertIcon,
  Box,
  Button,
  Center,
  FormControl,
  FormLabel,
  Input,
  Spinner,
  VStack,
} from "@chakra-ui/react";
import { FormEvent, useState } from "react";
import { useNavigate } from "react-router-dom";
import useAuth from "../context/AuthContext";

const Login = () => {
  const { login, loading } = useAuth();
  const navigate = useNavigate();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const [error, setError] = useState<string | null>(null);

  const onSubmitLogin = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const res = await login(username, password);
    setUsername("");
    setPassword("");
    if (!res) {
      setError("Something went wrong.");
      return;
    }
    navigate("/");
  };

  return (
    <>
      <Center mt="24">
        <Box p="6" bgColor="white" borderRadius="md" boxShadow="lg">
          <form onSubmit={(e) => !loading && onSubmitLogin(e)}>
            <Center>
              <VStack>
                {error && (
                  <Alert status="error">
                    <AlertIcon />
                    {error}
                  </Alert>
                )}
                <FormControl>
                  <FormLabel htmlFor="username">Username</FormLabel>
                  <Input
                    id="username"
                    type="username"
                    value={username}
                    onChange={(e) => setUsername(e.currentTarget.value)}
                  />
                </FormControl>
                <FormControl>
                  <FormLabel htmlFor="password">Password</FormLabel>
                  <Input
                    id="password"
                    type="password"
                    value={password}
                    onChange={(e) => setPassword(e.currentTarget.value)}
                  />
                </FormControl>
              </VStack>
            </Center>
            <Button
              type={"submit"}
              w={"full"}
              mt={4}
              colorScheme={"yellow"}
              color={"white"}
            >
              {loading ? <Spinner /> : "Login"}
            </Button>
          </form>
        </Box>
      </Center>
    </>
  );
};

export default Login;
