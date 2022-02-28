import { Box, Button, Stack, Text } from "@chakra-ui/react";
import moment from "moment";
import { useState } from "react";
import { mockComponent } from "react-dom/test-utils";
import { ActivityDto } from "../../../models";

interface ActivityProps {
  activity: ActivityDto;
}

const Activity = ({ activity }: ActivityProps) => {
  return (
    <Box bgColor={"gray.100"} borderRadius="sm" px="3" py="2">
      <Text fontFamily="poppins" fontSize="sm">
        {activity.message}
      </Text>
      <Text
        fontFamily="poppins"
        fontStyle="italic"
        fontSize="xs"
        color={"gray.400"}
      >
        {activity.doneByUser} - { moment(activity.occuredOn).fromNow()}
      </Text>
    </Box>
  );
};

interface ActivityListProps {
  activities: ActivityDto[];
  limit?: number;
}

const ActivityList = ({ activities, limit = 4 }: ActivityListProps) => {
  const [showMore, setShowMore] = useState<boolean>(true);

  const activitiesToShow = showMore ? activities.slice(0, limit) : activities;
  return (
    <Stack h="sm" overflowY={"auto"} pr="1" pb="2">
      {activitiesToShow &&
        activitiesToShow.map((activity, i) => (
          <Activity
            key={`${activity.message}-${activity.occuredOn}${i}`}
            activity={activity}
          />
        ))}
      {activities.length > limit && (
        <Box px="1" mt="auto">
          <Button
            w="100%"
            bgColor="gray.300"
            onClick={() => setShowMore(!showMore)}
          >
            {showMore ? "Show More" : "Show Less"}
          </Button>
        </Box>
      )}
    </Stack>
  );
};

export default ActivityList;
