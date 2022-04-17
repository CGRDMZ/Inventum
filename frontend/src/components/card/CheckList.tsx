import { CloseIcon } from "@chakra-ui/icons";
import {
  Box,
  Button,
  Input,
  InputGroup,
  InputRightElement,
  Text,
} from "@chakra-ui/react";
import { FC, KeyboardEvent, useRef, useState } from "react";
import useCard from "../../context/CardContext";
import { CheckListComponentDto } from "../../models";
import CheckListItem from "./CheckListItem";

const AddItemButton = ({ onEnter }: { onEnter: (content: string) => void }) => {
  const [isOpen, setIsOpen] = useState(false);
  const [content, setContent] = useState("");
  const ref = useRef<HTMLInputElement>(null);

  const onEnterPressed = (e: KeyboardEvent<HTMLInputElement>) => {
    if (e.key === "Enter") {
      setIsOpen(false);
      onEnter(content);
    }
  };

  return isOpen ? (
    <InputGroup size="xs">
      <Input
        ref={ref}
        onBlur={() => setIsOpen(false)}
        onKeyDown={(e) => onEnterPressed(e)}
        value={content}
        onChange={(e) => setContent(e.currentTarget.value)}
      />
      <InputRightElement>
        <Button size="xs" variant="ghost" onClick={() => setIsOpen(false)}>
          <CloseIcon />
        </Button>
      </InputRightElement>
    </InputGroup>
  ) : (
    <Button
      size="xs"
      variant="ghost"
      onClick={() => {
        setIsOpen(true);
        setTimeout(() => ref.current?.focus(), 50);
      }}
    >
      Add item
    </Button>
  );
};

const CheckList: FC<{ dto: CheckListComponentDto }> = ({ dto, children }) => {
  const { addItem } = useCard();

  return (
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
        {dto.name}
      </Text>
      {dto.checkListItems.map((item) => (
        <CheckListItem
          key={item.checkListItemId}
          dto={item}
          checkListId={dto.checkListComponentId}
        />
      ))}
      <AddItemButton onEnter={(content) => {addItem(dto.checkListComponentId, content)}} />
    </Box>
  );
};

export default CheckList;
