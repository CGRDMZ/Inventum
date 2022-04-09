import {
  Box,
  Center,
  Flex,
  HStack,
  ScaleFade,
  Spinner,
  Text,
  VStack,
} from "@chakra-ui/react";
import {
  DragDropContext,
  DragStart,
  Droppable,
  DropResult,
  ResponderProvided,
} from "react-beautiful-dnd";
import DeleteArea from "../components/board-detail/DeleteArea";
import NewCardGroup from "../components/board-detail/NewCardGroup";
import NewCardInput from "../components/board-detail/NewCardInput";
import SideBar from "../components/board-detail/sidebar/SideBar";
import Card from "../components/card/Card";
import useBoard, { BoardContextProvider } from "../context/BoardContext";
import { CardGroupDto } from "../models";

const CardGroup = ({
  cardGroupDto,
  ...props
}: {
  cardGroupDto: CardGroupDto;
}) => {
  return (
    <Box
      flexShrink={0}
      flexGrow={0}
      w={"64"}
      minH={"360px"}
      boxShadow={"lg"}
      borderRadius={"md"}
      bgColor={"white"}
      {...props}
    >
      <VStack spacing={"3"} pb={"3"}>
        <Center w={"100%"} borderBottom={"1px solid lightgray"} p={"3"}>
          <Text fontFamily={"poppins"} fontWeight={"extrabold"}>
            {cardGroupDto.name}
          </Text>
        </Center>
        {cardGroupDto.cards &&
          cardGroupDto.cards.map((cardDto, idx) => (
            <Card
              cardId={cardDto.cardId}
              key={cardDto.cardId}
              content={cardDto.content}
              bgColor={cardDto.cardColor}
              idx={idx}
            />
          ))}
        <NewCardInput
          cardGroupId={cardGroupDto.cardGroupId}
          idx={cardGroupDto.cards.length}
        />
      </VStack>
    </Box>
  );
};



const CardGroupList = ({
  cardGroupDtos,
}: {
  cardGroupDtos: CardGroupDto[];
}) => {
  const { isLoading } = useBoard();
  return (
    <HStack
      w="100%"
      position="relative"
      align={"start"}
      spacing={"5"}
      pb="5"
      overflowX={"scroll"}
    >
      {cardGroupDtos &&
        cardGroupDtos.map((dto, idx) => (
          <Droppable key={dto.cardGroupId} droppableId={dto.cardGroupId}>
            {(provided) => {
              return (
                <div ref={provided.innerRef} {...provided.droppableProps}>
                  <CardGroup cardGroupDto={dto} />
                  {provided.placeholder}
                </div>
              );
            }}
          </Droppable>
        ))}
      <NewCardGroup />
      {isLoading && (
        <Box position={"absolute"} top={0} right={0}>
          <ScaleFade in={isLoading} unmountOnExit>
            <Spinner />
          </ScaleFade>
        </Box>
      )}
    </HStack>
  );
};

const BoardDetail = () => {
  const { boardDetails, repositionCards, transferCard, setIsDragging, removeCard } =
    useBoard();

  function onDragEnd(result: DropResult, provided: ResponderProvided) {
    setIsDragging(false);
    if (!result.destination) return;

    if (result.destination?.droppableId === "deleteArea") {
      console.log(result);
      removeCard(result.source.droppableId, result.draggableId);
    }

    if (
      result.destination.index === result.source.index &&
      result.destination.droppableId === result.source.droppableId
    )
      return;

    if (result.destination.droppableId === result.source.droppableId) {
      // reposition the list request.
      repositionCards(
        result.draggableId,
        result.source.droppableId,
        result.destination.index
      );
    } else {
      // move the card request and then reposition the list.
      transferCard(
        result.source.droppableId,
        result.destination.droppableId,
        result.draggableId,
        result.destination.index
      );
    }
  }

  const onDragStart = (initial: DragStart, provided: ResponderProvided) => {
    setIsDragging(true);
  };

  return (
    <DragDropContext onDragEnd={onDragEnd} onDragStart={onDragStart}>
      <Center>
        <Flex position="relative" w={"90%"}>
          <DeleteArea />
          <SideBar
            boardInfo={boardDetails?.boardInfo || null}
            activities={boardDetails?.activities || null}
          />
          {boardDetails?.cardGroups && (
            <CardGroupList cardGroupDtos={boardDetails.cardGroups} />
          )}
        </Flex>
      </Center>
    </DragDropContext>
  );
};

const BoardDetailWrapper = () => {
  return (
    <BoardContextProvider>
      <BoardDetail />
    </BoardContextProvider>
  );
};

export default BoardDetailWrapper;
