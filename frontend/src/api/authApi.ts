import { Axios } from "axios";


const userClient = new Axios({
    baseURL: "https://localhost:5001/api/User",
    headers: {
        "content-type": "application/json"
    },
});

export const getToken = (username: string, password: string, callback: ((token: string, status: number) => any)) => {
    username = "testuser1";
    password = "123456Aa*";
    const data = { username, password }
    const json_data = JSON.stringify(data);

    userClient.post<string>("/getAccessToken", json_data).then((res) => callback(res.data, res.status));
}
