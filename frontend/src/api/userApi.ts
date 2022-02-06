import { Axios } from "axios";
import { InvitationDto } from "../models";

const userClient = new Axios({baseURL: "https://localhost:5001/api/user", headers: {"content-type": "application/json"}});


export const getInvitations = async (token: string) => {
    const result = await userClient.get<InvitationDto[]>("/invitations", {
        headers: {
            "content-type": "application/json",
            Authorization: `Bearer ${token}`,
        },
    });

    return result.data
}

export const handleInvitation = async (invitationId: string, token: string, accept: boolean) => {
    const result = await userClient.post(`/invitations/${invitationId}/handle?accept=${accept}`, {
        invitationId,
        accept,
    }, {
        headers: {
            "content-type": "application/json",
            Authorization: `Bearer ${token}`,
        },
    });

    return result.data
}