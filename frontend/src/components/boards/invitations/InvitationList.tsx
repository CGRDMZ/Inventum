import { CheckIcon, CloseIcon, EmailIcon } from "@chakra-ui/icons";
import {
  Box,
  Button,
  ButtonGroup,
  Center,
  Divider,
  Flex,
  IconButton,
  Text,
  Wrap,
} from "@chakra-ui/react";
import useInvitations from "../../../hooks/useInvitations";
import { InvitationDto } from "../../../models";

const Invitation = ({
  invitation,
  handleInvite,
}: {
  invitation: InvitationDto;
  handleInvite: (invitationId: string, accept: boolean) => Promise<void>;
}) => {
  return (
    <Box bgColor={"white"} px="3" borderRadius="md">
      <Flex>
        <Center p="1">
          <EmailIcon m="1" />
          <Text>{invitation.invitedTo}</Text>
        </Center>
      </Flex>
      <Divider mt="1" />
      <Flex w="100%" justifyContent="center" p="2" mt="2">
        <ButtonGroup isAttached variant="ghost">
          <IconButton
            aria-label="accept invite"
            icon={<CheckIcon color="green.300" />}
            onClick={() => handleInvite(invitation.invitationId, true)}
          />
          <Button>
            <CloseIcon
              color="red.300"
              onClick={() => handleInvite(invitation.invitationId, false)}
            />
          </Button>
        </ButtonGroup>
      </Flex>
    </Box>
  );
};

const InvitationList = () => {
  const { invitations, handleInvite, loading, error } = useInvitations();

  return (
    <Wrap>
      {invitations?.map((invitation) => (
        <Invitation invitation={invitation} handleInvite={handleInvite} />
      ))}
    </Wrap>
  );
};

export default InvitationList;
