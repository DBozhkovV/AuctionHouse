import './App.css';
import React from 'react';
import Layout from './Layout/Layout';
import Home from './Views/Pages/Home';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Login from './Views/Pages/Login';
import RegistrationForm from './Views/Pages/Register';
import ItemsApi from './Components/Items/AllAvailableItems';
import SearchedItems from './Components/Items/SearchedItems';
import Admin from './Views/Pages/Admin';
import Item from './Components/Items/Item';
import Post from './Views/Pages/Post';
import Profile from './Views/Pages/Profile';
import NotAcceptedItem from './Components/Items/NotAcceptedItem';

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
          <Route path="/profile" element={<Profile />} />
          <Route path="/notaccepteditem/:id" element={<NotAcceptedItem />} />
        </Routes>
      </Layout>
    </BrowserRouter>
  );
}

export default App;
