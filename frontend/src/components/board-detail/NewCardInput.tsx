import { Box, Flex, Input, Text } from "@chakra-ui/react";
import { FocusableElement } from "@chakra-ui/utils";
import { debounce } from "lodash";
import { useRef, useState } from "react";
import { Draggable } from "react-beautiful-dnd";
import useBoard from "../../context/BoardContext";
import { CreateCardDto } from "../../models";

interface Props {
  cardGroupId: string;
  idx: number;
}

const NewCardInput = ({ cardGroupId, idx }: Props) => {
  const [content, setContent] = useState("");
  const [color, setColor] = useState("#ffffff");

  const inputRef = useRef<HTMLInputElement>(null);

  const { createCard, isLoading } = useBoard();

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const dto: CreateCardDto = {
      content: content,
      bgColor: color,
    };
    await createCard(cardGroupId, dto);
    setContent("");
    setColor("#ffffff");

    // not necessary to set a timeout but it's nice to have in case something prevents the focus.
    setTimeout(() => {
      inputRef.current?.focus();
    }, 100);
  };

  const setColorDebounced = debounce(setColor, 1);

  return (
    <Draggable draggableId={cardGroupId} index={idx} isDragDisabled={true}>
      {(provided) => (
        <Box
          w={"90%"}
          py={"2"}
          px={"2"}
          borderRadius={"md"}
          boxShadow={"md"}
          bgColor={color}
          ref={provided.innerRef}
          {...provided.draggableProps}
          {...provided.dragHandleProps}
        >
          <form onSubmit={(e) => handleSubmit(e)}>
            <Flex direction="row" justifyContent="space-between" alignContent="space-between" w="100%">
              <Input
                ref={inputRef}
                fontFamily={"poppins"}
                fontStyle="italic"
                fontSize="sm"
                variant="unstyled"
                placeholder="create new card..."
                onChange={(e) => setContent(e.target.value)}
                value={content}
                disabled={isLoading}
              />
              <Input p={0}  w="8" type="color" variant="unstyled" value={color} onChange={(e) => setColorDebounced(e.currentTarget.value)} />
            </Flex>
          </form>
        </Box>
      )}
    </Draggable>
  );
};

export default NewCardInput;
