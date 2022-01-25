import jwtDecode from "jwt-decode";

export function decodeJwt<PayloadType = unknown>(token: string) {
    return jwtDecode<PayloadType>(token);
}