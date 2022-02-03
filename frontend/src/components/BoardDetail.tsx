import {
  Box,
  Center,
  Container,
  Divider,
  HStack,
  Text,
  VStack,
} from "@chakra-ui/react";
import { useEffect, useState } from "react";
import { useQuery } from "react-query";
import { useParams } from "react-router-dom";
import { boardApi } from "../api";
import useAuth from "../context/AuthContext";
import { BoardDetailsDto, CardGroupDto } from "../models";

const CardGroup = ({ cardGroupDto }: { cardGroupDto: CardGroupDto }) => {

  return (
    <Box
      flexShrink={0}
      flexGrow={0}
      w={"64"}
      minH={"360px"}
      boxShadow={"lg"}
      borderRadius={"md"}
      bgColor={"white"}
    >
      <VStack spacing={"3"} pb={"3"}>
        <Center w={"100%"} borderBottom={"1px solid lightgray"} p={"3"}>
          <Text fontFamily={"poppins"} fontWeight={"extrabold"}>
            {cardGroupDto.name}
          </Text>
        </Center>
        {cardGroupDto.cards && cardGroupDto.cards.map(cardDto => <Card key={cardDto.cardId} content={cardDto.content} bgColor={cardDto.cardColor} />)}
      </VStack>
    </Box>
  );
};



const Card = ({ content, bgColor }: { content: string; bgColor: string }) => {
  return (
    <Box
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
  );
};

const CardGroupList = ({
  cardGroupDtos,
}: {
  cardGroupDtos: CardGroupDto[];
}) => {
  return (
    <HStack align={"start"} spacing={"5"} py={"8"} overflowX={"scroll"}>
      {cardGroupDtos &&
        cardGroupDtos.map((dto) => <CardGroup key={dto.cardGroupId} cardGroupDto={dto} />)}
    </HStack>
  );
};

const BoardDetail = () => {
  const { user, token } = useAuth();
  const { boardId } = useParams();
  const [boardDetails, setBoardDetails] = useState<BoardDetailsDto | null>(
    null
  );

  const { data } = useQuery(
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

  return (
    <>
      <Center>
        <Box w={"90%"}>
          {boardDetails && (
            <CardGroupList cardGroupDtos={boardDetails.cardGroups} />
          )}
        </Box>
      </Center>
    </>
  );
};

export default BoardDetail;
