import { CheckCircleIcon } from "@chakra-ui/icons";
import { Checkbox, Flex, Text } from "@chakra-ui/react";
import { ChangeEvent, FC, useEffect, useState } from "react";
import { useMutation } from "react-query";
import { cardApi } from "../../api";
import useCard from "../../context/CardContext";
import { CheckListItemDto } from "../../models";

interface Props {
  dto: CheckListItemDto,
  checkListId: string;
  onClick?: () => void;
}

const CheckListItem: FC<Props> = ({ dto, checkListId, onClick }) => {
  const [localCheckState, setLocalCheckState] = useState(false);
  const [updating, SetUpdating] = useState(false);

  const { toggleItem } = useCard();

  

  useEffect(() => {
      setLocalCheckState(dto.isChecked);
  }, [dto.isChecked]);

  const onItemClick = (e: ChangeEvent<HTMLInputElement>) => {
    setLocalCheckState(!localCheckState);
    console.log("wtf");
    toggleItem(checkListId, dto.checkListItemId);
    
    onClick && onClick();
  };

  return (
    <Flex
      alignItems="center"
      justifyContent="space-between"
      _hover={{ bgColor: "gray.100" }}
    >
      <Text flexGrow="1" as={localCheckState ? "del" : undefined}>
        {dto.content}
      </Text>
      <Checkbox
        size="lg"
        colorScheme="green"
        isChecked={localCheckState}
        onChange={onItemClick}
      />
    </Flex>
  );
};

export default CheckListItem;
