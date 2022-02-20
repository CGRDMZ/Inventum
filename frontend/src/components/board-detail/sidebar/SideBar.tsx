import {
  Box,
  Divider,
  Heading,
  HStack,
  Input,
  Stack,
  Text,
  VStack,
} from "@chakra-ui/react";
import { ActivityDto, BoardDetailsDto, BoardInfoDto } from "../../../models";
import ActivityList from "./ActivityList";
import InviteSection from "./InviteSection";

interface Props {
  boardInfo: BoardInfoDto | null;
  activities: ActivityDto[] | null;
}

const SideBar = ({ boardInfo, activities }: Props) => {
  return (
    <Box minW="80" maxWidth="86" mr="3">
      <Stack w="100%" borderRadius="md" bgColor={"white"}>
        <Box p={3}>
          <Heading
            textAlign={"center"}
            fontFamily={"poppins"}
            as={"h1"}
            size="md"
            color={boardInfo?.bgColor || "black"}
            textShadow="1px 1px #000000"
          >
            {boardInfo?.name}
          </Heading>
        </Box>
        <Divider />
        <InviteSection />
        <Divider />
        <Box p={3}>
          <Text fontSize={"md"} fontFamily="poppins">
            Recent Activities
          </Text>
          {<ActivityList activities={activities || []} />}
        </Box>
      </Stack>
    </Box>
  );
};

export default SideBar;
