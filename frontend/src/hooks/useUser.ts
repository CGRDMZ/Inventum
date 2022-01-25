import { SetStateAction, useEffect, useState } from "react";
import { useQuery } from "react-query";
import { authApi } from "../api";
import { decodeJwt } from "../util";

const useUser = () => {
    const [token, setToken] = useState<string>("");
    const [tokenPayload, setTokenPayload] = useState<jwtPayload>();
    const { isLoading, error, data } = useQuery("user", () => {
        return getTokenQuery(setToken);
    });

    useEffect(() => {
        if (data) {
            setToken(data ?? "");
        }
        
    }, [data]);

    useEffect(() => {
        if (token) {
            setTokenPayload(decodeJwt(token));
        }
    }, [token]);
    
    

    return { isLoading, error, data, token, tokenPayload };
};

const getTokenQuery = (setToken: { (value: SetStateAction<string>): void; (arg0: string): void; }) => {
    
    authApi.getToken("", "", (token, status) => {
        if (status !== 200) {
            throw new Error("Couldn't acquire access token.");
        }
        setToken(token);
    });
};

export interface jwtPayload {
    sub: string;
    "given_name": string;
}

export default useUser;
