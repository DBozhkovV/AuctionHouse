import './App.css';
import React from 'react';
import Layout from './Layout/Layout';
import Home from './Views/Pages/Home';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Login from './Views/Pages/Login';
import Register from './Views/Pages/Register';
import ItemsApi from './Components/ItemsApi';

function App() {
  return (
    <BrowserRouter>
      <Layout>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/items" element={<ItemsApi />} />
        </Routes>
      </Layout>
    </BrowserRouter>
  );
}

export default App;
