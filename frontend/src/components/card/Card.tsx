import { CheckIcon } from "@chakra-ui/icons";
import {
  Box,
  Button,
  Grid,
  GridItem,
  Heading,
  Modal,
  ModalBody,
  ModalCloseButton,
  ModalContent,
  ModalHeader,
  ModalOverlay,
  Text,
  useDisclosure,
} from "@chakra-ui/react";
import { Draggable } from "react-beautiful-dnd";
import useBoard from "../../context/BoardContext";

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
  const { isLoading } = useBoard();
  const { isOpen, onOpen, onClose } = useDisclosure();

  return (
    <>
      <Draggable draggableId={cardId} index={idx} isDragDisabled={isLoading}>
        {(provided, snapshot) => {
          return (
            <Box
              ref={provided.innerRef}
              w={"90%"}
              py={"2"}
              px={"2"}
              bgColor={bgColor}
              borderRadius={"md"}
              boxShadow={"md"}
              {...provided.draggableProps}
              {...provided.dragHandleProps}
              onClick={onOpen}
            >
              <Text fontFamily={"poppins"} fontWeight={200}>
                {content}
              </Text>
            </Box>
          );
        }}
      </Draggable>
      <Modal
        isOpen={isOpen}
        size="3xl"
        onClose={onClose}
        scrollBehavior="inside"
      >
        <ModalOverlay />
        <ModalContent minH="48">
          <ModalHeader>{content}</ModalHeader>
          <ModalCloseButton />
          <ModalBody>
              <Grid templateColumns="2fr 1fr" gap={1}>
                <GridItem w="100%" minH="48">
                    
                </GridItem>
                <GridItem w="100%" minH="48">
                    <Text fontFamily="poppins" fontSize="md">Add Component</Text>
                    <Button variant="outline"><CheckIcon /></Button>
                </GridItem>
              </Grid>
          </ModalBody>
        </ModalContent>
      </Modal>
    </>
  );
};

export default Card;
