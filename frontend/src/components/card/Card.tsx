import { CheckCircleIcon, CheckIcon } from "@chakra-ui/icons";
import {
  Box,
  Button,
  Checkbox,
  Flex,
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
import useCard from "../../context/CardContext";
import CheckListItem from "./CheckListItem";

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
  const { cardDto, isLoading: isLoadingCard, changeCard } = useCard();
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
              onClick={() => {
                onOpen();
                changeCard(cardId);
              }}
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
          <ModalHeader>{cardDto?.content}</ModalHeader>
          <ModalCloseButton />
          <ModalBody>
            <Grid templateColumns="2fr 1fr" gap={1}>
              <GridItem w="95%">
                <Box minH="48" overflowY="auto">
                  {cardDto?.description && (
                    <>
                      <Text fontFamily="poppins" fontSize="sm">
                        Description
                      </Text>
                      <Box
                        mb="3"
                        bgColor="white"
                        borderRadius="md"
                        shadow="md"
                        p={1}
                      >
                        <Text fontFamily="poppins" fontSize="sm">
                          {cardDto?.description}
                        </Text>
                      </Box>
                    </>
                  )}

                  {cardDto?.checkListComponents.map((comp) => (
                    <>
                      <Box
                        mb="3"
                        px="2"
                        py="2"
                        mx="2"
                        mt="1"
                        bgColor="white"
                        borderRadius="md"
                        shadow="md"
                      >
                        <Text fontFamily="poppins" fontWeight="bold">
                          {comp.name}
                        </Text>
                        {comp.checkListItems.map((item) => (
                          <CheckListItem content={item.content} isChecked={item.isChecked} onClick={() => {}} />
                        ))}
                      </Box>
                    </>
                  ))}
                </Box>
              </GridItem>
              <GridItem w="100%" minH="48">
                <Text fontFamily="poppins" fontSize="md">
                  Add Component
                </Text>
                <Box shadow="md" p="3" borderRadius="md" bgColor="white">
                  <Button
                    variant="solid"
                    mr="1"
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
