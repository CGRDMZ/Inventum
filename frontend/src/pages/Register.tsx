import {
  Box,
  Button,
  Center,
  FormControl,
  FormHelperText,
  FormLabel,
  Input,
} from "@chakra-ui/react";
import { useState } from "react";
import useAuth from "../context/AuthContext";
import { RegisterDto } from "../models";

const Register = () => {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const { register } = useAuth();

  const onSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const dto = { username, email, password } as RegisterDto;
    await register(dto);
  };

  return (
    <Box mt="24">
      <Center>
        <Box p="6" bgColor="white" borderRadius="md" boxShadow="lg">
          <form onSubmit={onSubmit}>
            <FormControl>
              <FormLabel htmlFor="username">Username</FormLabel>
              <Input
                id="username"
                name="username"
                type="text"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
              />
            </FormControl>
            <FormControl>
              <FormLabel htmlFor="email">Email</FormLabel>
              <Input
                id="email"
                name="email"
                type="email"
                value={email}
                onChange={(e) => setEmail(e.currentTarget.value)}
              />
            </FormControl>
            <FormControl>
              <FormLabel htmlFor="password">Password</FormLabel>
              <Input
                id="password"
                name="password"
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </FormControl>
            <Button
              type={"submit"}
              w={"full"}
              mt={4}
              colorScheme={"yellow"}
              color={"white"}
            >
              Register
            </Button>
          </form>
        </Box>
      </Center>
    </Box>
  );
};

export default Register;
