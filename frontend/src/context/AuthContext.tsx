import {
  createContext,
  ReactNode,
  useContext,
  useEffect,
  useState,
} from "react";
import useUser from "../hooks/useUser";

interface UserInfo {
  username: string;
  userId: string;
}

interface AuthContextType {
  user?: UserInfo;
  token?: string;
  isLoading: boolean;
}

const AuthContext = createContext<AuthContextType>({} as AuthContextType);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [user, setUser] = useState<UserInfo>();
  const { isLoading, error, data, token, tokenPayload } = useUser();

  useEffect(() => {
    if (tokenPayload && tokenPayload.given_name && tokenPayload.sub) {
      const user = {
        username: tokenPayload.given_name,
        userId: tokenPayload.sub,
      } as UserInfo;
      setUser(user);
    }
  }, [tokenPayload, token]);

  return (
    <AuthContext.Provider
      value={{ user: user, token: token } as AuthContextType}
    >
      {isLoading && <div>loading...</div>}
      {!isLoading && children}
    </AuthContext.Provider>
  );
};

const useAuth = () => {
    return useContext(AuthContext);
}

export default useAuth;