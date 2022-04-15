import { createContext, FC, useContext, useEffect, useState } from "react";
import { useQuery } from "react-query";
import { cardApi } from "../api";
import { CardWithComponentsDto } from "../models";
import useAuth from "./AuthContext";

interface CardContextProps {
  cardId: string;
  cardDto?: CardWithComponentsDto;
  isLoading: boolean;
  isFetching: boolean;
  changeCard: (cardId: string) => void;
}

const CardContext = createContext<CardContextProps>({
  isLoading: true,
  isFetching: true,
} as CardContextProps);

export const CardContextProvider: FC = ({ children }) => {
  const { token } = useAuth();
  const [cardId, setCardId] = useState<string>("");
  const { data, isLoading, isFetching, refetch } = useQuery<
    unknown,
    unknown,
    CardWithComponentsDto,
    string
  >(
    cardId,
    async () => {
      const { data, status } = await cardApi.getCard(cardId, token || "");
      return data;
    },
    { enabled: !!token && cardId !== "" }
  );

  useEffect(() => {
    refetch();
  }, [cardId, refetch]);

  const changeCard = (cardId: string) => {
    setCardId(cardId);
  };

  return (
    <CardContext.Provider
      value={{ cardId, isLoading, isFetching, cardDto: data, changeCard }}
    >
      {children}
    </CardContext.Provider>
  );
};

const useCard = () => {
  return useContext(CardContext);
};

export default useCard;
