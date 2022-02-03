import { Box } from "@chakra-ui/react";
import React from "react";
import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import "./App.css";
import BoardDetail from "./components/BoardDetail";
import Boards from "./components/Boards";
import Home from "./components/Home";
import Login from "./components/Login";
import Navbar from "./components/navbar/Navbar";
import Register from "./components/Register";
import { AuthProvider } from "./context/AuthContext";
import { BoardsProvider } from "./context/BoardsContext";

function App() {
  return (
    <Box bg={"gray.100"} w={"100%"} minH={"100vh"}>
      <BrowserRouter>
        <AuthProvider>
          <BoardsProvider>
            <Navbar />
            <Routes>
              <Route path="/home" element={<Home />} />
              <Route path="/boards" element={<Boards />} />
              <Route path="/boards/:boardId" element={<BoardDetail />} />
              <Route path="/login" element={<Login />} />
              <Route path="/register" element={<Register />} />
              <Route path="*" element={<Navigate to={"/home"} />} />
            </Routes>
          </BoardsProvider>
        </AuthProvider>
      </BrowserRouter>
    </Box>
  );
}

export default App;
