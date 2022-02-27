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
  isDragging: boolean;
  setIsDragging: (isDragging: boolean) => void;
  createCardGroup: (cardGroup: CreateCardGroupDto) => void;
  createCard: (cardGroupId: string, card: CreateCardDto) => void;
  removeCard: (cardGroupId: string, cardId: string) => void;
  inviteUser: (inviteUser: InviteUserDto) => void;
  refreshBoard: () => void;
  repositionCards: (
    cardId: string,
    cardGroupId: string,
    targetPosition: number
  ) => void;
  transferCard: (
    fromCardGroupId: string,
    toCardGroupId: string,
    cardId: string,
    targetPosition: number
  ) => void;
}

const BoardContext = createContext<BoardContextProps>({} as BoardContextProps);

export const BoardContextProvider = ({ children }: { children: ReactNode }) => {
  const [boardDetails, setBoardDetails] = useState<BoardDetailsDto | null>(
    null
  );
  const [isDragging, setIsDraggingState] = useState(false);

  const { token } = useAuth();
  const { boardId } = useParams();

  const toast = useToast();

  const { data, isFetching, refetch } = useQuery(
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
  }, [data, isFetching]);

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

  const repositionCardsMutation = useMutation(
    "repositionCards",
    async ({
      cardGroupId,
      cardIds,
    }: {
      cardGroupId: string;
      cardIds: string[];
    }) => {
      return await boardApi.repositionCards(
        boardDetails!.boardInfo.boardId,
        cardGroupId,
        cardIds,
        token!
      );
    }
  );

  const transferCardMutation = useMutation(
    "transferCard",
    async ({
      fromCardGroupId,
      cardId,
      targetCardGroupId,
    }: {
      fromCardGroupId: string;
      cardId: string;
      targetCardGroupId: string;
    }) => {
      return await boardApi.transferCard(
        boardDetails!.boardInfo.boardId,
        fromCardGroupId,
        cardId,
        targetCardGroupId,
        token!
      );
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
          toast({
            status: "error",
            title: val,
            duration: 1000,
            isClosable: true,
          })
        );
      }
    }
    refetch();
  };

  const refreshBoard = () => {};

  const repositionCards = async (
    cardId: string,
    cardGroupId: string,
    targetPosition: number
  ) => {
    const newState = JSON.parse(
      JSON.stringify(boardDetails)
    ) as BoardDetailsDto;

    const cardGroup = newState.cardGroups.find(
      (val) => val.cardGroupId === cardGroupId
    );

    if (cardGroup) {
      const card = cardGroup.cards.find((val) => val.cardId === cardId);
      if (card) {
        // remove the dragged card from the list
        const cardIndex = cardGroup.cards.indexOf(card);
        cardGroup.cards.splice(cardIndex, 1);

        // insert the card at the target position
        cardGroup.cards.splice(targetPosition, 0, card);

        setBoardDetails(newState);

        // update the backend
        const cardIds = cardGroup.cards.map((val) => val.cardId);
        await repositionCardsMutation.mutateAsync({
          cardGroupId: cardGroup.cardGroupId,
          cardIds,
        });
      }
    }
  };

  const transferCard = async (
    fromCardGroupId: string,
    toCardGroupId: string,
    cardId: string,
    targetPosition: number
  ) => {
    const newState = JSON.parse(
      JSON.stringify(boardDetails)
    ) as BoardDetailsDto;

    const fromCardGroup = newState.cardGroups.find(
      (val) => val.cardGroupId === fromCardGroupId
    );

    const targetCardGroup = newState.cardGroups.find(
      (val) => val.cardGroupId === toCardGroupId
    );

    if (fromCardGroup && targetCardGroup) {
      const card = fromCardGroup.cards.find((val) => val.cardId === cardId);
      if (card) {
        // remove the dragged card from the list
        const cardIndex = fromCardGroup.cards.indexOf(card);
        fromCardGroup.cards.splice(cardIndex, 1);

        // insert the card at the target position
        targetCardGroup.cards.splice(targetPosition, 0, card);

        setBoardDetails(newState);

        // update the backend
        await transferCardMutation.mutateAsync({
          fromCardGroupId,
          targetCardGroupId: toCardGroupId,
          cardId,
        });

        const cardIds = targetCardGroup.cards.map((val) => val.cardId);
        await repositionCardsMutation.mutateAsync({
          cardGroupId: targetCardGroup.cardGroupId,
          cardIds,
        });
      }
    }
  };

  const setIsDragging = (isDragging: boolean) => {
    setIsDraggingState(isDragging);
  };

  const removeCard = (cardGroupId: string, cardId: string) => {
    const newState = JSON.parse(
      JSON.stringify(boardDetails)
    ) as BoardDetailsDto;

    const cardGroup = newState.cardGroups.find(
      (val) => val.cardGroupId === cardGroupId
    );

    if (cardGroup) {
      const card = cardGroup.cards.find((val) => val.cardId === cardId);
      if (card) {
        // remove the dragged card from the list
        const cardIndex = cardGroup.cards.indexOf(card);
        cardGroup.cards.splice(cardIndex, 1);

        setBoardDetails(newState);

        // update the backend

      }
    }
  }

  return (
    <BoardContext.Provider
      value={{
        boardDetails,
        createCardGroup,
        createCard,
        removeCard,
        inviteUser,
        refreshBoard,
        repositionCards,
        transferCard,
        isLoading:
          createCardGroupMutation.isLoading ||
          createCardMutation.isLoading ||
          inviteUserMutation.isLoading ||
          repositionCardsMutation.isLoading ||
          transferCardMutation.isLoading ||
          isFetching,
        isDragging: isDragging,
        setIsDragging
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
