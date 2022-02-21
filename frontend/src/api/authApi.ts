import { Axios } from "axios";
import { RegisterDto, Result } from "../models";
import { constants } from "../util/constants";


const userClient = new Axios({
    baseURL: `${constants.API_BASE}/User`,
    headers: {
        "content-type": "application/json"
    },
});

export const getAccessToken = (username: string, password: string) => {
    const data = { username, password }
    const json_data = JSON.stringify(data);

    return new Promise<{ token: string, status: number }>((resolve, reject) => userClient.post<string>("/getAccessToken", json_data).then(res => resolve({ token: res.data, status: res.status })));
}

export const register = async (dto: RegisterDto): Promise<Result<string>> => {
    const data = JSON.stringify(dto);
    const result = await userClient.post<string>("/", data, {headers: { "content-type": "application/json" }});
    
    return JSON.parse(result.data);
}
