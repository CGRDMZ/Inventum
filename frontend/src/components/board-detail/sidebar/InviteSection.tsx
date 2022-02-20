import { Box, Input, Text } from "@chakra-ui/react";
import { useState } from "react";
import useBoard from "../../../context/BoardContext";

const InviteSection = () => {
  const [username, setUsername] = useState("");

  const { boardDetails, inviteUser } = useBoard();

  const onSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    if (username.length === 0) return;
    inviteUser({
      invitedUserUsername: username,
      boardId: boardDetails!.boardInfo.boardId,
    });
    setUsername("");
  };

  return (
    <Box p="3">
      <Text fontSize="md" fontFamily="poppins">
        Invite a Friend
      </Text>
      <form onSubmit={onSubmit}>
        <Input
          placeholder="Username of your friend..."
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
      </form>
    </Box>
  );
};

export default InviteSection;
