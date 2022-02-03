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
import { loginDto } from "../models";
import { decodeJwt } from "../util";

interface UserInfo {
  username: string;
  userId: string;
}

interface AuthContextType {
  user?: UserInfo;
  token?: string;
  // refreshAccessToken: () => void;
  login: (username: string, password: string) => Promise<boolean>;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType>({} as AuthContextType);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const navigate = useNavigate();

  const [user, setUser] = useState<UserInfo>();
  const [token, setToken] = useState<string>();

  const getAccessTokenMutation = useMutation(
    (loginDto: loginDto) => {
      return authApi.getAccessToken(loginDto.username, loginDto.password);
    },
    { mutationKey: "getAccessToken" }
  );

  const login = useCallback(async (username: string, password: string) => {
    try {
      const res = await getAccessTokenMutation.mutateAsync({
        username,
        password,
      } as loginDto);

      if (res.token) {
        const tokenPayload = decodeJwt<jwtPayload>(res.token);

        setToken(res.token);
        localStorage.setItem("access_token", res.token);
        setUser({
          username: tokenPayload.given_name,
          userId: tokenPayload.sub,
        });
        return true;
      }
    } catch (error) {
      return false;
    }
  }, [getAccessTokenMutation]);

  const logout = useCallback(() => {
    setToken("");
    localStorage.removeItem("access_token");
    setUser(undefined);
    navigate("/");
  }, [navigate]);

  

  useEffect(() => {
    const local_token = localStorage.getItem("access_token");
    if (local_token) {
      const tokenPayload = decodeJwt<jwtPayload>(local_token);
      setToken(local_token);
      setUser({ username: tokenPayload.given_name, userId: tokenPayload.sub });
    }
  }, []);

  useEffect(() => {
    if (token) {
      let tokenPayload = decodeJwt<jwtPayload>(token);
      let local_now = new Date();

      // if the token is expired, just remove it.
      // we should have a refresh token to get a new one in the future instead of this.
      if (new Date(tokenPayload.exp * 1000) < local_now) {
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
