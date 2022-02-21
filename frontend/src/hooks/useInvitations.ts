import { useEffect, useState } from "react"
import { useMutation, useQuery } from "react-query";
import { userApi } from "../api";
import useAuth from "../context/AuthContext";
import useBoards from "../context/BoardsContext";
import { InvitationDto } from "../models"

const useInvitations = () => {
    const { user, token } = useAuth();
    const [invitations, setInvitations] = useState<InvitationDto[]>([]);
    const {refreshBoards} = useBoards();

    const { data, isFetching, error, refetch } = useQuery("invitations", async () => {
        const invitations = await userApi.getInvitations(token || "");
        return invitations;
    }, { enabled: !!token });

    const handleInvitationMutation = useMutation(({ invitationId, accept }: { invitationId: string, accept: boolean }) => {
        console.log(token);
        
        return userApi.handleInvitation(invitationId, accept, token!);
    }, { mutationKey: "handleInvitation" });

    const handleInvite = async (invitationId: string, accept: boolean) => {
        const result = await handleInvitationMutation.mutateAsync({ invitationId, accept });
        if (result) {
            refetch();
            refreshBoards();
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