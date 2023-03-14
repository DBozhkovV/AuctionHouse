import React, { useState } from 'react';
import { MDBCollapse, MDBNavbar, MDBNavbarLink, 
    MDBDropdownMenu, MDBNavbarToggler, MDBDropdownItem, 
    MDBContainer, MDBNavbarBrand, MDBNavbarNav, MDBBtn } from 'mdb-react-ui-kit';
import Logout from '../../Components/Authorization/Logout';
import logo from '../../Assets/images/user-profile-icon.svg';
import "../../css/Navbar.css"

const NavbarComponent = () => {
    const [search, setSearch] = useState(null);
    const [LogoutShow, setLogoutShow] = useState(false);
    const isUser = sessionStorage.getItem("isUser"); // null if is not admin
    const isAdmin = sessionStorage.getItem("isAdmin"); // null if is not user if the both are null then it is not logged in

    const handleSubmit = (e) => {
        e.preventDefault();
        window.location.href = `/items/search/${search}`;
    }

    const ShowAuthNavigation = () => {
        if (isUser) {
            return  (
                <>
                    <MDBNavbarLink href="/post">Post</MDBNavbarLink>
                    <MDBNavbarLink onClick={() => setLogoutShow(true)}>Logout</MDBNavbarLink>
                        <Logout show = {LogoutShow} onHide={() => setLogoutShow(false)} /> 
                    <MDBNavbarLink className='nav-bids' href="/bids">Your bids</MDBNavbarLink>
                    <MDBNavbarBrand href="/profile">
                    <img
                        src={logo}
                        width="30"
                        height="30"
                        className="d-inline-block align-top"
                        alt=""
                    />
                    </MDBNavbarBrand>
                    <MDBNavbarLink>
                        Balance: {isUser} $
                    </MDBNavbarLink>
                </>
            );
        }
        if (isAdmin === null) {
            return (
                <>
                    <MDBNavbarLink href="/login">Login</MDBNavbarLink>
                    <MDBNavbarLink href="/register">Register</MDBNavbarLink>
                </>
            );
        }
    };

    const ShowAdminNavigation = () => {
        if (isAdmin) {
            return (
                <>
                    <MDBNavbarLink href="/admin">Admin</MDBNavbarLink>
                    <MDBNavbarLink onClick={() => setLogoutShow(true)}>Logout</MDBNavbarLink>
                        <Logout show = {LogoutShow} onHide={() => setLogoutShow(false)}/> 
                </>
            );
        }
    };

    return(
        <MDBNavbar  className="nav-color" variant="dark">
            <MDBContainer>
            <MDBNavbarBrand  href="/"> Auction House </MDBNavbarBrand >
            <MDBNavbarToggler aria-controls="basic-navbar-nav" />
            <MDBCollapse  id="responsive-navbar-nav">
                <MDBNavbarNav variant="pills" className="me-auto" >
                <MDBNavbarLink href="/">Home</MDBNavbarLink>
                <MDBNavbarLink href="/items/1">Items</MDBNavbarLink>
                <MDBDropdownMenu title="Category" id="basic-nav-dropdown">
                    <MDBDropdownItem href="/category/Jewellery">Jewellery</MDBDropdownItem>
                    <MDBDropdownItem href="/category/Watch">Watch</MDBDropdownItem>
                    <MDBDropdownItem href="/category/Car">Car</MDBDropdownItem>
                    <MDBDropdownItem href="/category/Alcohol">Alcohol</MDBDropdownItem>
                    <MDBDropdownItem href="/category/Painting">Painting</MDBDropdownItem>
                    <MDBDropdownItem divider />
                    <MDBDropdownItem href="/category/other">
                        Other
                    </MDBDropdownItem>
                </MDBDropdownMenu>
                </MDBNavbarNav>
                <form className="d-flex search-box">
                    <input
                        type="search"
                        placeholder="Search"
                        className="me-2"
                        aria-label="Search"
                        onChange={(e) => { setSearch(e.target.value) }}
                    />
                    <MDBBtn type="submit" variant="outline-info" onClick={(e) => handleSubmit(e)}>Search</MDBBtn>
                </form >
                <MDBNavbarNav className="nav-forms">
                    {ShowAuthNavigation()}
                    {ShowAdminNavigation()}
                </MDBNavbarNav>
                </MDBCollapse >
            </MDBContainer>
        </MDBNavbar >
    );
}

export default NavbarComponent;