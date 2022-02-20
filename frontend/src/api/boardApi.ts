import { Axios } from "axios";
import { BoardDetailsDto, BoardDto, CreateBoardDto, CreateCardDto, CreateCardGroupDto, InviteUserDto, Result } from "../models";
import { constants } from "../util/constants";

const boardClient = new Axios({ baseURL: `${constants.API_BASE}/board` });

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

export const addCardGroup = async (boardId: string, cardGroupDto: CreateCardGroupDto, token: string) => {
    const result = await boardClient.post(`/${boardId}/addCardGroup`, JSON.stringify(cardGroupDto), {
        headers: {
            "content-type": "application/json",
            Authorization: `Bearer ${token}`,
        }
    });

    return result.status
}

export const addCard = async (boardId: string, cardGroupId: string, cardDto: CreateCardDto, token: string) => {
    const result = await boardClient.post(`/${boardId}/${cardGroupId}/addCard`, JSON.stringify(cardDto), {
        headers: {
            "content-type": "application/json",
            Authorization: `Bearer ${token}`,
        }
    });

    return result.status
}

export const inviteUser = async (dto: InviteUserDto, token: string) => {
    const result = await boardClient.post<Result<void>>(`/invite`, JSON.stringify(dto), {
        headers: {
            "content-type": "application/json",
            Authorization: `Bearer ${token}`,
        }
    });

    return result.data
}