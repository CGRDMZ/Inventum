import { useState } from "react";
import { Link as RouterLink, useLocation } from "react-router-dom";
import { Box, Button, Center, Flex, Link, Stack, Text } from "@chakra-ui/react";
import { CloseIcon, HamburgerIcon } from "@chakra-ui/icons";
import useAuth from "../../context/AuthContext";
import { ReactComponent as LogoSvg } from "../../assets/logo-semibold.svg";

const Logo = () => {
  return (
    <Box display={{ base: "block" }}>
      <Link as={RouterLink} to="/" style={{ textDecoration: "none" }}>
        <LogoSvg />
      </Link>
    </Box>
  );
};

const NavItemStack = ({ isOpen }: { isOpen: boolean }) => {
  const { user, logout } = useAuth();
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
      {
        <>
          <Center>
            <Text color={"white"} fontWeight={"bold"} fontFamily={"poppins"}>
              {user && (
                <Text
                  as="span"
                  color={"white"}
                  fontWeight={"bold"}
                  fontFamily={"caveat"}
                  fontSize={"2xl"}
                >
                  Welcome {user.username}!
                </Text>
              )}
            </Text>
          </Center>
          {user ? (
            <>
              <NavItem name="Boards" to="/boards" />
              <NavItem name="Profile" to="onedayyouwillfindyourpath" />
              <Center>
                <Button onClick={logout}>Logout</Button>
              </Center>
            </>
          ) : (
            <>
              <NavItem name="Login" to="/login" />
              <NavItem name="Register" to="/register" />
            </>
          )}
        </>
      }
    </Stack>
  );
};

const NavItem = ({ name, to }: { name: string; to: string }) => {
  const location = useLocation();
  const isActive = location.pathname === to;

  return (
    <Center>
      <Link as={RouterLink} to={to}>
        <Text
          color={isActive ? "yellow" : "white"}
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
        marginY={"5"}
        bgColor={"blue.500"}
        borderRadius={"lg"}
        boxShadow={"sm"}
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
