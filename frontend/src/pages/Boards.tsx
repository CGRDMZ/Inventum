import {
  Box,
  Center,
  CloseButton,
  HStack,
  Input,
  Link,
  ScaleFade,
  Slide,
  Spinner,
  Text,
  useToast,
  VStack,
  Wrap,
  WrapItem,
} from "@chakra-ui/react";
import { FormEvent, useEffect, useState } from "react";
import useAuth from "../context/AuthContext";
import useBoards from "../context/BoardsContext";
import { BoardDto } from "../models";
import { Link as RouterLink } from "react-router-dom";
import { debounce } from "lodash";
import InvitationList from "../components/boards/invitations/InvitationList";

const NewBoardCard = () => {
  const { addNewBoard, isCreatingNewBoard } = useBoards();
  const [name, setName] = useState<string>("");
  const [color, setColor] = useState<string>("#ffffff");

  const onClickAddBoard = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    addNewBoard({ name: name, color: color });
  };

  const setColorDebounced = debounce(setColor, 1);

  return (
    <WrapItem
      minW={"56"}
      maxW={"56"}
      minH={"24"}
      maxH={"24"}
      bgColor={color}
      alignItems={"center"}
      p={"1"}
      borderRadius={"md"}
    >
      {!isCreatingNewBoard ? (
        <form onSubmit={(e) => onClickAddBoard(e)}>
          <HStack>
            <Input
              id="boardName"
              type={"text"}
              value={name}
              placeholder="create new board..."
              onChange={(e) => setName(e.currentTarget.value)}
            />
            <Input
              type={"color"}
              w={"0.8"}
              value={color}
              onChange={(e) => setColorDebounced(e.currentTarget.value)}
            />
          </HStack>
        </form>
      ) : (
        <Center w={"100%"}>
          <Spinner />
        </Center>
      )}
    </WrapItem>
  );
};

const Board = ({ board }: { board: BoardDto }) => {
  const { removeBoard } = useBoards();

  return (
    <Box position={"relative"}>
      <Link as={RouterLink} to={`${board.boardId}`}>
        <WrapItem
          minW={"56"}
          maxW={"56"}
          minH={"24"}
          maxH={"24"}
          bgColor={board.bgColor}
          alignItems={"center"}
          p={"1"}
          borderRadius={"md"}
        >
          <Center w={"100%"}>
            <Text fontFamily={"poppins"} w={"100%"} textAlign={"center"}>
              {board.name}
            </Text>
          </Center>
        </WrapItem>
      </Link>
      <CloseButton
        position={"absolute"}
        top={0}
        right={0}
        onClick={() => removeBoard(board.boardId)}
      />
    </Box>
  );
};

const BoardList = ({ boards }: { boards: BoardDto[] }) => {
  const { isLoading } = useBoards();
  return (
    <Wrap position={"relative"}>
      <NewBoardCard />
      {boards.map((b) => (
        <Board key={b.boardId} board={b} />
      ))}
      {
        <Box position={"absolute"} top={0} right={0}>
          <ScaleFade in={isLoading} unmountOnExit>
            <Spinner />
          </ScaleFade>
        </Box>
      }
    </Wrap>
  );
};

const Boards = () => {
  const { user } = useAuth();
  const { isLoading, isCreatingNewBoard, error, boards, addNewBoard } =
    useBoards();

  const shouldShowBoards = () => boards && boards.length !== 0;

  return (
    <Center>
      <VStack w={"90%"} spacing="3">
        <Box w="100%">
          <Text fontFamily={"poppins"}>My Boards</Text>
          <BoardList boards={boards} />
        </Box>
        <Box w="100%">
          <Text fontFamily={"poppins"}>My Invitations</Text>
          <InvitationList />
        </Box>
      </VStack>
    </Center>
  );
};

export default Boards;
