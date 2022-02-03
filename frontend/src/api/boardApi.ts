import { Axios } from "axios";
import { BoardDetailsDto, BoardDto, CreateBoardDto } from "../models";

const boardClient = new Axios({ baseURL: "https://localhost:5001/api/board" });

export const getBoards = (token: string) => {
    return new Promise<BoardDto[]>((resolve, reject) => {
        return boardClient
            .get<BoardDto[]>("/", {
                headers: {
                    "content-type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
            })
            .then((res) => {

                return resolve(res.data);
            });
    });
}

export const getBoardDetails = async (boardId: string, token: string) => {
    const result = await boardClient.get<BoardDetailsDto>(`/${boardId}`, {
        headers: {
            "content-type": "application/json",
            Authorization: `Bearer ${token}`,
        }
    })
    return result.data
}

export const addBoard = async (boardDto: CreateBoardDto, token: string) => {
    const result = await boardClient.post("/createBoard", JSON.stringify(boardDto), {
        headers: {
            "content-type": "application/json",
            Authorization: `Bearer ${token}`,
        }
    });

    return result.status
}

export const removeBoard = async (boardId: string, token: string) => {
    const result = await boardClient.delete(`/${boardId}`, {
        headers: {
            "content-type": "application/json",
            Authorization: `Bearer ${token}`,
        }
    });

    return result.status
}