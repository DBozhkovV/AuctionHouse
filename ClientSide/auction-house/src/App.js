import './App.css';
import React from 'react';
import Layout from './Views/Layout/Layout';
import Home from './Views/Pages/Home';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Login from './Views/Pages/Login';
import RegistrationForm from './Views/Pages/Register';
import AllAvailableItems from './Components/Items/AllAvailableItems';
import SearchedItems from './Components/Items/SearchedItems';
import Admin from './Views/Pages/Admin';
import Item from './Components/Items/Item';
import Post from './Views/Pages/Post';
import Profile from './Views/Pages/Profile';
import NotAcceptedItem from './Components/Items/NotAcceptedItem';
import ItemsByCategory from './Components/Items/ItemsByCategory';
import Order from './Components/Orders/Order';
import BidPage from './Components/Bids/BidPage';
import SortByHighToLow from './Components/Items/SortingType/SortByHighToLow';
import SortByLowToHigh from './Components/Items/SortingType/SortByLowToHigh';
import SortByNewest from './Components/Items/SortingType/SortByNewest';
import Verification from './Components/Authorization/Verification';
import ForgotPassword from './Components/Authorization/ForgotPassword';
import ResetPassword from './Components/Authorization/ResetPassword';

function App() {
  return (
    <BrowserRouter>
      <Layout>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<RegistrationForm />} />
          <Route path="/items/:page" element={<AllAvailableItems />} />
          <Route path="/items/sortByHighToLow" element={<SortByHighToLow />} />
          <Route path="/items/sortByLowToHigh" element={<SortByLowToHigh />} />
          <Route path="/items/newest" element={<SortByNewest />} />
          <Route path="/items/search/:search" element={<SearchedItems />} />
          <Route path="/admin" element={<Admin />} />
          <Route path="/item/:id" element={<Item />} />
          <Route path="/post" element={<Post />} />
          <Route path="/order/:id" element={<Order />} />
          <Route path="/profile" element={<Profile />} />
          <Route path="/notaccepteditem/:id" element={<NotAcceptedItem />} />
          <Route path="/category/:category" element={<ItemsByCategory />} />
          <Route path="/bids" element={<BidPage />} />
          <Route path="/verify/:token" element={<Verification />} />
          <Route path="/forgotpassword" element={<ForgotPassword />} />
          <Route path="/reset-password/:token" element={<ResetPassword />} />
        </Routes>
      </Layout>
    </BrowserRouter>
  );
}

export default App;
