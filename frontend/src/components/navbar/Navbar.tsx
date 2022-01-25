import { useState } from "react";
import { Link as RouterLink } from "react-router-dom";
import {
  Box,
  Button,
  Center,
  Flex,
  Link,
  Stack,
  Text,
} from "@chakra-ui/react";
import { CloseIcon, HamburgerIcon } from "@chakra-ui/icons";
import useAuth from "../../context/AuthContext";

const Logo = () => {
  return (
    <Box display={{ base: "block" }}>
      <Link as={RouterLink} to="/" style={{textDecoration: "none"}}>
        <Text
          fontFamily={"Caveat"}
          fontWeight={"bold"}
          fontSize={"5xl"}
          color={"yellow"}
        >
          Inventum
        </Text>
      </Link>
    </Box>
  );
};

const NavItemStack = ({ isOpen }: { isOpen: boolean }) => {
  const { isLoading, user } = useAuth();
  return (
    <Stack
      direction={{ base: "column", md: "row" }}
      display={
        isOpen ? { base: "block", md: "flex" } : { base: "none", md: "flex" }
      }
      spacing={8}
      w={{ base: "100%", md: "initial" }}
      mr={5}
    >
      {!isLoading && user && (
        <>
          <Center>
            <Text color={"white"} fontWeight={"bold"}>
              Welcome {user.username}!
            </Text>
          </Center>
          <NavItem name="Boards" to="/boards" />
          <NavItem name="Profile" to="onedayyouwillfindyourpath" />
        </>
      )}
    </Stack>
  );
};

const NavItem = ({ name, to }: { name: string, to: string }) => {
  return (
    <Center>
      <Link as={RouterLink} to={to}>
        <Text
          color={"white"}
          fontFamily={"Poppins"}
          fontSize={"large"}
          fontWeight={"extrabold"}
        >
          {name}
        </Text>
      </Link>
    </Center>
  );
};

const Hamburger = ({
  isOpen,
  toggle,
}: {
  isOpen: boolean;
  toggle: () => void;
}) => {
  return (
    <Center display={{ base: "flex", md: "none" }} onClick={toggle}>
      <Button>
        {isOpen ? (
          <CloseIcon color={"blue.400"} />
        ) : (
          <HamburgerIcon color={"blue.400"} w={5} h={5} />
        )}
      </Button>
    </Center>
  );
};

const Navbar = () => {
  const [isOpen, setIsOpen] = useState<boolean>(false);

  return (
    <Center>
      <Box
        w={"90%"}
        padding={"3"}
        margin={"5"}
        bgColor={"blue.500"}
        borderRadius={"lg"}
        boxShadow={"xl"}
      >
        <Flex justify={"space-between"} wrap={"wrap"}>
          <Logo />
          <Hamburger
            isOpen={isOpen}
            toggle={() => {
              setIsOpen(!isOpen);
            }}
          />
          <NavItemStack isOpen={isOpen} />
        </Flex>
      </Box>
    </Center>
  );
};

export default Navbar;
