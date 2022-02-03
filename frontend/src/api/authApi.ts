import { Axios } from "axios";


const userClient = new Axios({
    baseURL: "https://localhost:5001/api/User",
    headers: {
        "content-type": "application/json"
    },
});

export const getAccessToken = (username: string, password: string) => {
    const data = { username, password }
    const json_data = JSON.stringify(data);

    return new Promise<{token: string, status: number}>((resolve, reject) => userClient.post<string>("/getAccessToken", json_data).then(res => resolve({token: res.data, status: res.status})));
}
