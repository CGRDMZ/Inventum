import { DeleteIcon } from "@chakra-ui/icons";
import { Box, Center, position, SlideFade } from "@chakra-ui/react";
import { Droppable } from "react-beautiful-dnd";
import useBoard from "../../context/BoardContext";

const DeleteArea = () => {
  const { isDragging } = useBoard();
  return (
    <Droppable droppableId="deleteArea">
      {(provided, snapshot) => {
        return (
          <div
            ref={provided.innerRef}
            {...provided.droppableProps}
            style={{
              position: "absolute",
              top: "50%",
              left: "16px",
              transform: "translateY(-50%)",
            }}
          >
            <SlideFade offsetX={-40} offsetY={0} in={isDragging}>
              <Box w="28" h="28">
                <Center w="100%" h="100%" bgColor="red.400" boxShadow="xl">
                  <DeleteIcon w="8" h="8" />
                </Center>
              </Box>
            </SlideFade>
            <span style={{ display: "none" }}>{provided.placeholder}</span>
          </div>
        );
      }}
    </Droppable>
  );
};

export default DeleteArea;
