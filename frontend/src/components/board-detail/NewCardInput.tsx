import { Box, Input, Text } from "@chakra-ui/react";
import { useState } from "react";
import { Draggable } from "react-beautiful-dnd";
import useBoard from "../../context/BoardContext";
import { CreateCardDto } from "../../models";

interface Props {
  cardGroupId: string;
  idx: number;
}

const NewCardInput = ({ cardGroupId, idx }: Props) => {
  const [content, setContent] = useState("");
  const { createCard, isLoading } = useBoard();

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const dto: CreateCardDto = {
      content: content,
      bgColor: "#ffffff",
    };
    createCard(cardGroupId, dto);
    setContent("");
  };

  return (
    <Draggable draggableId={cardGroupId} index={idx} isDragDisabled={true}>
      {(provided) => (
        <Box
          w={"90%"}
          py={"2"}
          px={"2"}
          borderRadius={"md"}
          boxShadow={"md"}
          ref={provided.innerRef}
          {...provided.draggableProps}
          {...provided.dragHandleProps}
        >
          <form onSubmit={(e) => handleSubmit(e)}>
            <Input
              fontFamily={"poppins"}
              fontStyle="italic"
              fontSize="sm"
              variant="unstyled"
              placeholder="create new card..."
              onChange={(e) => setContent(e.target.value)}
              value={content}
              disabled={isLoading}
            />
          </form>
        </Box>
      )}
    </Draggable>
  );
};

export default NewCardInput;
