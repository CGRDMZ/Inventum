import { CheckCircleIcon } from "@chakra-ui/icons";
import { Checkbox, Flex, Text } from "@chakra-ui/react";
import { FC, MouseEvent } from "react";

interface Props {
  isChecked: boolean;
  content: string;
  onClick: () => void;
}

const CheckListItem: FC<Props> = ({ isChecked, content, onClick }) => {
  const onItemClick = (e: MouseEvent<HTMLDivElement>) => {
    e.preventDefault();
    onClick();
  };
  return (
    <Flex
      alignItems="center"
      justifyContent="space-between"
      _hover={{ bgColor: "gray.100" }}
    >
      <Text flexGrow="1" as={isChecked ? "del" : undefined}>
        {content}
      </Text>
      <Checkbox
        size="lg"
        colorScheme="green"
        isChecked={isChecked}
        onClick={onItemClick}
        icon={<CheckCircleIcon w="4" h="4" />}
      />
    </Flex>
  );
};

export default CheckListItem;
