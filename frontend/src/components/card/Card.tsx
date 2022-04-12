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
        <ModalOverlay backdropFilter="blur(2px)" />
        <ModalContent minH="96" bgColor={bgColor}>
          <ModalHeader>{content}</ModalHeader>
          <ModalCloseButton />
          <ModalBody>
            <Grid templateColumns="2fr 1fr" gap={1}>
              <GridItem w="95%">
                <Box minH="48" overflowY="auto">
                  <Box bgColor="white" borderRadius="md" shadow="md" p={1}>
                    <Text fontFamily="poppins" fontSize="sm">
                      Lorem ipsum dolor sit amet consectetur adipisicing elit.
                      Dolore provident adipisci nihil aperiam minima harum et
                      dolorem corporis repudiandae, veniam facere sed cum
                      impedit corrupti dolores maiores exercitationem ullam.
                      Nihil.
                    </Text>
                  </Box>
                </Box>
              </GridItem>
              <GridItem w="100%" minH="48">
                <Box shadow="md" p="3" borderRadius="md" bgColor="white">
                  <Text
                    fontFamily="poppins"
                    fontSize="md"
                    fontWeight="light"
                    fontStyle="italic"
                  >
                    Add Component
                  </Text>
                  <Button
                    variant="solid"
                    mr="1"
                    mb="1"
                    size="sm"
                    title="Add a checklist component!"
                  >
                    <CheckIcon />
                  </Button>
                </Box>
              </GridItem>
            </Grid>
          </ModalBody>
        </ModalContent>
      </Modal>
    </>
  );
};

export default Card;
