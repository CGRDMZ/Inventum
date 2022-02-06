import {
  Alert,
  AlertIcon,
  Button,
  Center,
  FormControl,
  FormHelperText,
  FormLabel,
  HStack,
  Input,
  Spinner,
  VStack,
} from "@chakra-ui/react";
import { FormEvent, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import useAuth from "../context/AuthContext";

const Login = () => {
  const { login, loading } = useAuth();
  const navigate = useNavigate();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const [error, setError] = useState<string | null>(null);

  const onSubmitLogin = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    console.log(username, password);
    const res = await login(username, password);
    setUsername("");
    setPassword("");
    if (!res) setError("Something went wrong.");
    navigate("/");
  };

  return (
    <>
      <Center>
        <VStack w={"50%"} maxW={"5xl"}>
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
                  <FormHelperText>We'll never share your email.</FormHelperText>
                </FormControl>
              </VStack>
            </Center>
            <Button
              type={"submit"}
              w={"full"}
              my={1}
              colorScheme={"yellow"}
              color={"white"}
            >
              {loading ? <Spinner /> : "Login"}
            </Button>
          </form>
        </VStack>
      </Center>
    </>
  );
};

export default Login;
