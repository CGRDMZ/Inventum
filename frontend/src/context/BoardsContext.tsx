import { useToast } from "@chakra-ui/react";
import {
  createContext,
  ReactNode,
  useContext,
  useEffect,
  useState,
} from "react";
import { useMutation, useQuery } from "react-query";
import { boardApi } from "../api";
import { BoardDto, CreateBoardDto } from "../models";
import useAuth from "./AuthContext";

interface BoardContextType {
  boards: BoardDto[];
  isLoading: boolean;
  isCreatingNewBoard: boolean;
  error: string[];
  addNewBoard: (board: CreateBoardDto) => void;
  removeBoard: (boardId: string) => void;
  refreshBoards: () => void;
}

const defaultValue = {
  boards: new Array<BoardDto>(),
  isLoading: true,
  isCreatingNewBoard: false,
  error: [] as string[],
} as BoardContextType;

const BoardsContext = createContext<BoardContextType>(defaultValue);

export const BoardsProvider = ({ children }: { children: ReactNode }) => {
  const { user, token } = useAuth();
  const toast = useToast();

  const [boards, setBoards] = useState<BoardDto[]>([]);
  const [error, setError] = useState<string[]>([]);
  const [isCreatingNewBoard, setIsCreatingNewBoard] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState<boolean>(true);

  const {
    data,
    isLoading: isLoadingBoards,
    refetch: refetchBoards,
    isFetching,
  } = useQuery(
    "boards_data",
    () => {
      return boardApi.getBoards(token || "");
    },
    { enabled: !!token }
  );

  const newBoardMutation = useMutation(
    (boardDto: CreateBoardDto) => {
      return boardApi.addBoard(boardDto, token ?? "");
    },
    {
      mutationKey: "addBoard",
    }
  );

  const removeBoardMutation = useMutation(
    (boardId: string) => {
      return boardApi.removeBoard(boardId, token ?? "");
    },
    {
      mutationKey: "removeBoard",
    }
  );

  useEffect(() => {
    if (data) {
      const boards = JSON.parse(data.toString()) as BoardDto[];
      console.log("setting the new data...");

      setBoards(boards);
    }
  }, [data]);

  useEffect(() => {
    setIsCreatingNewBoard(newBoardMutation.isLoading);
  }, [newBoardMutation.isLoading]);

  useEffect(() => {
    setIsLoading(
      isFetching || newBoardMutation.isLoading || removeBoardMutation.isLoading
    );
  }, [isFetching, newBoardMutation.isLoading, removeBoardMutation.isLoading]);

  useEffect(() => {
    if (error && error.length > 0) {
      error.forEach((err) => {
        toast({ title: err, status: "error", duration: 3000 });
      });
      setError([]);
    }
  }, [error, toast]);

  const addNewBoard = async (board: CreateBoardDto) => {
    await newBoardMutation.mutateAsync(board);
    refetchBoards();
  };

  const removeBoard = async (boardId: string) => {
    await removeBoardMutation.mutateAsync(boardId);
    refetchBoards();
  };

  const refreshBoards = async () => {
    await refetchBoards();
  };

  return (
    <BoardsContext.Provider
      value={{
        boards,
        isLoading: isLoading,
        isCreatingNewBoard: isCreatingNewBoard,
        error,
        addNewBoard,
        removeBoard,
        refreshBoards
      }}
    >
      {children}
    </BoardsContext.Provider>
  );
};

const useBoards = () => {
  return useContext(BoardsContext);
};

export default useBoards;
