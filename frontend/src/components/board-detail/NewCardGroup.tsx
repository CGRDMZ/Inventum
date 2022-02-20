import { AddIcon, CloseIcon } from "@chakra-ui/icons";
import { Box, Button, Center, HStack, Input, VStack } from "@chakra-ui/react";
import { useState } from "react";
import { JsxElement } from "typescript";
import useBoard from "../../context/BoardContext";

const NewCardGroup = () => {
  const {createCardGroup} = useBoard();
  const [isOpen, setIsOpen] = useState(false);
  const [cardGroupName, setCardGroupName] = useState("");

  const handleSubmit = () => {
    createCardGroup({cardGroupName});
    setIsOpen(false);
    setCardGroupName("");
  };

  return (
    <Box p="1">
      {isOpen ? (
        <Box
          flexShrink={0}
          flexGrow={0}
          w={"64"}
          minH={"360px"}
          boxShadow={"lg"}
          borderRadius={"md"}
          bgColor={"white"}
        >
          <VStack spacing={"3"}>
            <HStack w={"100%"} borderBottom={"1px solid lightgray"}>
              <Button
                w="3"
                variant="ghost"
                colorScheme="blue"
                onClick={() => setIsOpen(!isOpen)}
              >
                <CloseIcon />
              </Button>
              <form onSubmit={handleSubmit}>
              <Input
                variant="unstyled"
                placeholder="name of the card group..."
                value={cardGroupName}
                onChange={(e) => setCardGroupName(e.target.value)}
              />
              </form>
            </HStack>
          </VStack>
        </Box>
      ) : (
        <Button
          w="3"
          variant="outline"
          colorScheme="blue"
          onClick={() => setIsOpen(!isOpen)}
        >
          <AddIcon />
        </Button>
      )}
    </Box>
  );
};

export default NewCardGroup;
