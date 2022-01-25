import { Box } from "@chakra-ui/react";
import React from "react";
import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import "./App.css";
import Boards from "./components/Boards";
import Home from "./components/Home";
import Login from "./components/Login";
import Navbar from "./components/navbar/Navbar";
import PrivateRoute from "./components/PrivateRoute";
import Register from "./components/Register";
import { AuthProvider } from "./context/AuthContext";

function App() {
  return (
    <Box bg={"gray.100"} w={"100%"} minH={"100vh"}>
      <AuthProvider>
        <BrowserRouter>
        <Navbar />
          <Routes>
            <Route path="/home" element={<Home />} />
            <Route path="/boards" element={<Boards />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
            <Route path="*" element={<Navigate to={"/home"} />} />
          </Routes>
        </BrowserRouter>
      </AuthProvider>
    </Box>
  );
}

export default App;
