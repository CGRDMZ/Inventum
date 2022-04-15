import axios from "axios";
import { CardWithComponentsDto, Result } from "../models";
import { constants } from "../util/constants";

const cardClient = axios.create({
    baseURL: `${constants.API_BASE}/card`,
    headers: {
        "content-type": "application/json",
    }
});

export const getCard = async (cardId: string, token: string) => {
    const res = await cardClient.get<CardWithComponentsDto>(`/${cardId}`,
        {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
    return { data: res.data, status: res.status };
}

export const toggleChecklistItem = async (cardId: string, checklistId: string, checklistItemId: string, token: string) => {
    const res = await cardClient.post<Result<{}>>(`/${cardId}/checklist/${checklistId}/item/${checklistItemId}/toggle`, {
        headers: {
            Authorization: `Bearer ${token}`
        }
    });

    return { data: res.data, status: res.status };
};

export const createChecklistItemComponent = async (cardId: string, token: string, name: string) =>{
    const res = await cardClient.post<Result<{}>>(`/${cardId}/addChecklist`, {
        headers: {
            Authorization: `Bearer ${token}`
        }
    });

    return { data: res.data, status: res.status };
}