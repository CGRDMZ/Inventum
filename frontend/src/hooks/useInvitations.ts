import { useEffect, useState } from "react"
import { useMutation, useQuery } from "react-query";
import { userApi } from "../api";
import useAuth from "../context/AuthContext";
import { InvitationDto } from "../models"

const useInvitations = () => {
    const { user, token } = useAuth();
    const [invitations, setInvitations] = useState<InvitationDto[]>([]);

    const { data, isFetching, error, refetch } = useQuery("invitations", async () => {
        const invitations = await userApi.getInvitations(token || "");
        return invitations;
    }, { enabled: !!token });

    const handleInvitationMutation = useMutation(({ invitationId, accept }: { invitationId: string, accept: boolean }) => {
        return userApi.handleInvitation(invitationId, token || "", accept);
    }, { mutationKey: "handleInvitation" });

    const handleInvite = async (invitationId: string, accept: boolean) => {
        const result = await handleInvitationMutation.mutateAsync({ invitationId, accept });
        if (result) {
            refetch();
        }
    };

    useEffect(() => {
        if (data) {
            const invitations = JSON.parse(data.toString()) as InvitationDto[];
            setInvitations(invitations);
        }
    }, [data])

    return { invitations, reload: refetch, handleInvite, loading: isFetching, error };
}

export default useInvitations;