import { useToast } from "@chakra-ui/react";
import {
  createContext,
  ReactNode,
  useContext,
  useEffect,
  useState,
} from "react";
import { useMutation, useQuery } from "react-query";
import { useParams } from "react-router-dom";
import { boardApi } from "../api";
import {
  BoardDetailsDto,
  CreateCardDto,
  CreateCardGroupDto,
  InviteUserDto,
  Result,
} from "../models";
import useAuth from "./AuthContext";

interface BoardContextProps {
  boardDetails: BoardDetailsDto | null;
  isLoading: boolean;
  createCardGroup: (cardGroup: CreateCardGroupDto) => void;
  createCard: (cardGroupId: string, card: CreateCardDto) => void;
  inviteUser: (inviteUser: InviteUserDto) => void;
  refreshBoard: () => void;
}

const BoardContext = createContext<BoardContextProps>({} as BoardContextProps);

export const BoardContextProvider = ({ children }: { children: ReactNode }) => {
  const [boardDetails, setBoardDetails] = useState<BoardDetailsDto | null>(
    null
  );
  const { user, token } = useAuth();
  const { boardId } = useParams();

  const toast = useToast();

  const { data, refetch } = useQuery(
    `boardDetail${boardId}`,
    async () => {
      return await boardApi.getBoardDetails(boardId!, token!);
    },
    { enabled: !!token }
  );

  useEffect(() => {
    if (data) {
      const jsonData = JSON.parse(data.toString()) as BoardDetailsDto;

      setBoardDetails(jsonData);
    }
  }, [data]);

  const createCardGroupMutation = useMutation(
    "createCardGroup",
    async (createCardGroupDto: CreateCardGroupDto) => {
      return await boardApi.addCardGroup(
        boardDetails!.boardInfo.boardId,
        createCardGroupDto,
        token!
      );
    }
  );

  const createCardMutation = useMutation(
    "createCard",
    async ({
      dto,
      cardGroupId,
    }: {
      dto: CreateCardDto;
      cardGroupId: string;
    }) => {
      return await boardApi.addCard(
        boardDetails!.boardInfo.boardId,
        cardGroupId,
        dto,
        token!
      );
    }
  );

  const inviteUserMutation = useMutation(
    "inviteUser",
    async ({ dto }: { dto: InviteUserDto }) => {
      return await boardApi.inviteUser(dto, token!);
    }
  );

  const createCardGroup = async (cardGroup: CreateCardGroupDto) => {
    await createCardGroupMutation.mutateAsync(cardGroup);
    refetch();
  };

  const createCard = async (cardGroupId: string, card: CreateCardDto) => {
    await createCardMutation.mutateAsync({ dto: card, cardGroupId });
    refetch();
  };

  const inviteUser = async (dto: InviteUserDto) => {
    const res = await inviteUserMutation.mutateAsync({ dto });
    const resJson = JSON.parse(res.toString()) as Result<void>;

    if (resJson) {
      if (resJson.success) {
        toast({
          status: "success",
          title: "Invited User Successfully",
          duration: 1000,
          isClosable: true,
        });
      } else {
        resJson.errors.map((val) =>
          toast({ status: "error", title: val, duration: 1000, isClosable: true })
        );
      }
    }
    refetch();
  };

  const refreshBoard = () => {};

  return (
    <BoardContext.Provider
      value={{
        boardDetails,
        createCardGroup,
        createCard,
        inviteUser,
        refreshBoard,
        isLoading: false,
      }}
    >
      {children}
    </BoardContext.Provider>
  );
};

const useBoard = () => {
  return useContext(BoardContext);
};

export default useBoard;
