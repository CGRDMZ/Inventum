import { Axios } from "axios";
import { InvitationDto } from "../models";
import { constants } from "../util/constants";

const userClient = new Axios({ baseURL: `${constants.API_BASE}/User`, headers: { "content-type": "application/json" } });


export const getInvitations = async (token: string) => {
    const result = await userClient.get<InvitationDto[]>("/invitations", {
        headers: {
            "content-type": "application/json",
            Authorization: `Bearer ${token}`,
        },
    });

    return result.data
}

export const handleInvitation = async (invitationId: string, accept: boolean, token: string,) => {
    const result = await userClient.post(`/invitations/${invitationId}/handle?accept=${accept}`, JSON.stringify({}), {
        headers: {
            "content-type": "application/json",
            Authorization: `Bearer ${token}`,
        },
    });

    return result.data
}