import { Axios } from "axios";
import { useQuery } from "react-query";
import useUser from "./useUser";

const boardClient = new Axios({ baseURL: 'https://localhost:5001/api/board', headers: {} });

const useBoards = () => {
    const { error: userError, data: user, token, isLoading: isUserLoading } = useUser();

    const getToken = () => {
        return token;
    };

    const { isLoading: isBoardsLoading, error, data } = useQuery("board_list", () => {
        const query = (token: string) => boardClient.get<Array<BoardDto>>("/", { headers: { "Authorization": `Bearer ${token}` } });
        return query(getToken());
    }, { enabled: !!token });

    return { isLoading: isUserLoading || isBoardsLoading, error, boards: data?.data ?? null };
}

export interface BoardDto {
    boardId: string;
    bgColor: string;
    name: string;
}


export default useBoards;