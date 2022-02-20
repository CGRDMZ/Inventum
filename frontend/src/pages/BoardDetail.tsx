import {
  Box,
  Center,
  Container,
  Divider,
  Flex,
  HStack,
  Text,
  VStack,
} from "@chakra-ui/react";
import { useEffect, useState } from "react";
import {
  DragDropContext,
  Draggable,
  Droppable,
  DropResult,
  ResponderProvided,
} from "react-beautiful-dnd";
import { useQuery } from "react-query";
import { useParams } from "react-router-dom";
import { boardApi } from "../api";
import NewCardGroup from "../components/board-detail/NewCardGroup";
import SideBar from "../components/board-detail/sidebar/SideBar";
import useAuth from "../context/AuthContext";
import useBoard, { BoardContextProvider } from "../context/BoardContext";
import { BoardDetailsDto, CardGroupDto } from "../models";

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
      </VStack>
    </Box>
  );
};

const Card = ({
  cardId,
  content,
  bgColor,
  idx,
}: {
  cardId: string;
  content: string;
  bgColor: string;
  idx: number;
}) => {
  return (
    <Draggable draggableId={cardId} index={idx}>
      {(provided) => (
        <Box
          ref={provided.innerRef}
          {...provided.draggableProps}
          {...provided.dragHandleProps}
          w={"90%"}
          py={"2"}
          px={"2"}
          bgColor={bgColor}
          borderRadius={"md"}
          boxShadow={"md"}
        >
          <Text fontFamily={"poppins"} fontWeight={200}>
            {content}
          </Text>
        </Box>
      )}
    </Draggable>
  );
};

const CardGroupList = ({
  cardGroupDtos,
}: {
  cardGroupDtos: CardGroupDto[];
}) => {
  return (
    <HStack align={"start"} spacing={"5"} pb="5" overflowX={"scroll"}>
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
    </HStack>
  );
};

const BoardDetail = () => {
  const { boardDetails } = useBoard();
  return (
    <BoardContextProvider>
      <Center>
        <Flex w={"90%"}>
          <SideBar
            boardInfo={boardDetails?.boardInfo || null}
            activities={boardDetails?.activities || null}
          />
          {boardDetails?.cardGroups && (
            <CardGroupList cardGroupDtos={boardDetails.cardGroups} />
          )}
        </Flex>
      </Center>
    </BoardContextProvider>
  );
};

const BoardDetailWrapper = () => {
  const { boardDetails } = useBoard();

  function onDragEnd(result: DropResult, provided: ResponderProvided) {
    if (!result.destination) return;
    if (
      result.destination.index === result.source.index &&
      result.destination.droppableId === result.source.droppableId
    )
      return;

    if (result.destination.droppableId === result.source.droppableId) {
      // reposition the list request.
      console.log("reposition the list request");
      
    } else {
      // move the card request and then reposition the list.
      console.log("move the card request and then reposition the list");
      
    }

  }

  return (
    <BoardContextProvider>
      <DragDropContext onDragEnd={onDragEnd}>
        <BoardDetail />
      </DragDropContext>
    </BoardContextProvider>
  );
};

export default BoardDetailWrapper;
