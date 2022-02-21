import { ListItem, UnorderedList, useToast } from "@chakra-ui/react";
import { JwtPayload } from "jwt-decode";
import {
  createContext,
  ReactNode,
  useCallback,
  useContext,
  useEffect,
  useState,
} from "react";
import { useMutation } from "react-query";
import { useNavigate } from "react-router-dom";
import { authApi } from "../api";
import useUser, { jwtPayload } from "../hooks/useUser";
import { LoginDto, RegisterDto, Result } from "../models";
import { decodeJwt } from "../util";

interface UserInfo {
  username: string;
  userId: string;
}

interface AuthContextType {
  user?: UserInfo;
  token?: string;
  loading: boolean;
  register: (dto: RegisterDto) => Promise<Result<string>>;
  // refreshAccessToken: () => void;
  login: (username: string, password: string) => Promise<boolean>;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType>({
  loading: false,
} as AuthContextType);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const navigate = useNavigate();

  const toast = useToast();

  const [user, setUser] = useState<UserInfo>();
  const [token, setToken] = useState<string>();

  const registerMutation = useMutation((dto: RegisterDto) => {
    const res = authApi.register(dto);
    return res;
  });

  const getAccessTokenMutation = useMutation(
    (loginDto: LoginDto) => {
      return authApi.getAccessToken(loginDto.username, loginDto.password);
    },
    { mutationKey: "getAccessToken" }
  );

  const handleToken = (token: string) => {
    const tokenPayload = decodeJwt<jwtPayload>(token);

    setToken(token);
    localStorage.setItem("access_token", token);

    setUser({
      username: tokenPayload.given_name,
      userId: tokenPayload.sub,
    });
  };

  const register = async (dto: RegisterDto) => {
    const result = await registerMutation.mutateAsync(dto);
    if (!result.success) {
      console.log(result);

      const ErrorTitle = ({ errors }: { errors: string[] }) => {
        return (
          <UnorderedList>
            {errors.map((error) => (
              <ListItem>{error}</ListItem>
            ))}
          </UnorderedList>
        );
      };

      toast({
        title: <ErrorTitle errors={result.errors} />,
        status: "error",
        duration: 10000,
        isClosable: true,
      });
    } else {
      toast({
        title: "Successfully registered",
        status: "success",
        duration: 1500,
      });
      navigate("/login");
    }

    return result;
  };

  const login = useCallback(
    async (username: string, password: string) => {
      try {
        const res = await getAccessTokenMutation.mutateAsync({
          username,
          password,
        } as LoginDto);

        if (res.token) {
          handleToken(res.token);
          return true;
        }
      } catch (error) {
        return false;
      }
    },
    [getAccessTokenMutation]
  );

  const logout = useCallback(() => {
    setToken("");
    localStorage.removeItem("access_token");
    setUser(undefined);
    navigate("/");
  }, [navigate]);

  useEffect(() => {
    const local_token = localStorage.getItem("access_token");
    if (local_token) {
      handleToken(local_token);
    }
  }, []);

  useEffect(() => {
    if (token) {
      let tokenPayload = decodeJwt<jwtPayload>(token);
      let local_now = new Date();

      let token_exp = new Date(tokenPayload.exp * 1000);

      // if the token is expired, just remove it.
      // we should have a refresh token to get a new one in the future instead of this.
      if (token_exp < local_now) {
        logout();
        console.log("token expired...");
      }
    }
  }, [logout, navigate, token]);

  // const refreshAccessToken = () => {
  //   authApi.getAccessToken("","").then((res) => {
  //     if (res.status !== 200) {
  //       console.log("user could not login.");
  //       return;
  //     }
  //     if (res.token) {
  //       const tokenPayload = decodeJwt<jwtPayload>(res.token);

  //       setToken(res.token);
  //       setUser({username: tokenPayload.given_name, userId: tokenPayload.sub});
  //     }
  //   });
  // }

  return (
    <AuthContext.Provider
      value={
        {
          user: user,
          token: token,
          loading: getAccessTokenMutation.isLoading,
          register,
          login: login,
          logout: logout,
        } as AuthContextType
      }
    >
      {children}
    </AuthContext.Provider>
  );
};

const useAuth = () => {
  return useContext(AuthContext);
};

export default useAuth;
