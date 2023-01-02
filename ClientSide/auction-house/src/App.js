import './App.css';
import React from 'react';
import Layout from './Layout/Layout';
import Home from './Views/Pages/Home';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Login from './Views/Pages/Login';
import RegistrationForm from './Views/Pages/Register';
import ItemsApi from './Components/ItemsApi';
import SearchedItems from './Components/SearchedItems';
import Admin from './Views/Pages/Admin';
import Item from './Components/Item';
import Post from './Components/Post';

function App() {
  return (
    <BrowserRouter>
      <Layout>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<RegistrationForm />} />
          <Route path="/items" element={<ItemsApi />} />
          <Route path="/items/search/:search" element={<SearchedItems />} />
          <Route path="/admin" element={<Admin />} />
          <Route path="/item/:id" element={<Item />} />
          <Route path="/post" element={<Post />} />
        </Routes>
      </Layout>
    </BrowserRouter>
  );
}

export default App;
