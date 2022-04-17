import { createContext, FC, useContext, useEffect, useState } from "react";
import { useMutation, useQuery } from "react-query";
import { cardApi } from "../api";
import { CardWithComponentsDto } from "../models";
import useAuth from "./AuthContext";

interface CardContextProps {
  cardId: string;
  cardDto?: CardWithComponentsDto;
  isLoading: boolean;
  isFetching: boolean;
  changeCard: (cardId: string) => void;
  toggleItem: (checkListId: string, itemId: string) => void;
  addItem:  (checkListId: string, content: string) => void;
  addCheckList: () => void;
}

const CardContext = createContext<CardContextProps>({
  isLoading: true,
  isFetching: true,
} as CardContextProps);

export const CardContextProvider: FC = ({ children }) => {
  const { token } = useAuth();
  const [cardId, setCardId] = useState<string>("");
  const { data, isLoading, isFetching, refetch } =
    useQuery<CardWithComponentsDto>(
      cardId,
      async () => {
        const { data, status } = await cardApi.getCard(cardId, token || "");
        return data;
      },
      { enabled: !!token && cardId !== "" }
    );

  const toggleItemMutation = useMutation(
    "toggleItem",
    ({ checkListId, itemId }: { checkListId: string; itemId: string }) => {
      return cardApi.toggleChecklistItem(
        data!.cardId,
        checkListId,
        itemId,
        token!
      );
    }
  );

  const addCheckListComponentMutation = useMutation("addclcomponent", () => {
    return cardApi.createChecklistItemComponent(
      data!.cardId,
      token!,
      "Check List"
    );
  })

  const addItemMutation = useMutation("addItem", ({checkListId, content}: {checkListId: string, content: string}) => {
    return cardApi.createChecklistItem(data!.cardId, checkListId, content, token!);
  });

  useEffect(() => {
    refetch();
  }, [cardId, refetch]);

  const changeCard = (cardId: string) => {
    setCardId(cardId);
  };

  const toggleItem = async (checkListId: string, itemId: string) => {
    await toggleItemMutation.mutateAsync({checkListId, itemId})
    refetch();
  }

  const addItem = async (checkListId: string, content: string) => {
    await addItemMutation.mutateAsync({checkListId, content});
    refetch();
  }

  const addCheckList = async () => {
    await addCheckListComponentMutation.mutateAsync();
    refetch();
  }

  return (
    <CardContext.Provider
      value={{ cardId, isLoading, isFetching, cardDto: data, changeCard, toggleItem, addItem, addCheckList }}
    >
      {children}
    </CardContext.Provider>
  );
};

const useCard = () => {
  return useContext(CardContext);
};

export default useCard;
