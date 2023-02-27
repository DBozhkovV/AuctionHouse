import React, { useState } from 'react';
import { Container, Nav, Navbar, NavDropdown, Form, Button } from 'react-bootstrap';
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
                    <Nav.Link href="/post">Post</Nav.Link>
                    <Nav.Link onClick={() => setLogoutShow(true)}>Logout</Nav.Link>
                        <Logout show = {LogoutShow} onHide={() => setLogoutShow(false)} /> 
                    <Nav.Link className='nav-bids' href="/bids">Your bids</Nav.Link>
                    <Navbar.Brand href="/profile">
                    <img
                        src={logo}
                        width="30"
                        height="30"
                        className="d-inline-block align-top"
                        alt=""
                    />
                    </Navbar.Brand>
                    <Navbar.Text>
                        Balance: {isUser} $
                    </Navbar.Text>
                </>
            );
        }
        if (isAdmin === null) {
            return (
                <>
                    <Nav.Link href="/login">Login</Nav.Link>
                    <Nav.Link href="/register">Register</Nav.Link>
                </>
            );
        }
    };

    const ShowAdminNavigation = () => {
        if (isAdmin) {
            return (
                <>
                    <Nav.Link href="/admin">Admin</Nav.Link>
                    <Nav.Link onClick={() => setLogoutShow(true)}>Logout</Nav.Link>
                        <Logout show = {LogoutShow} onHide={() => setLogoutShow(false)}/> 
                </>
            );
        }
    };

    return(
        <Navbar className="nav-color" variant="dark">
            <Container>
            <Navbar.Brand href="/"> Auction House </Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="responsive-navbar-nav">
                <Nav variant="pills" className="me-auto" >
                <Nav.Link href="/">Home</Nav.Link>
                <Nav.Link href="/items/1">Items</Nav.Link>
                <NavDropdown title="Category" id="basic-nav-dropdown">
                    <NavDropdown.Item href="/category/Jewellery">Jewellery</NavDropdown.Item>
                    <NavDropdown.Item href="/category/Watch">Watch</NavDropdown.Item>
                    <NavDropdown.Item href="/category/Car">Car</NavDropdown.Item>
                    <NavDropdown.Item href="/category/Alcohol">Alcohol</NavDropdown.Item>
                    <NavDropdown.Item href="/category/Painting">Painting</NavDropdown.Item>
                    <NavDropdown.Divider />
                    <NavDropdown.Item href="/category/other">
                        Other
                    </NavDropdown.Item>
                </NavDropdown>
                </Nav>
                <Form className="d-flex search-box">
                    <Form.Control
                        type="search"
                        placeholder="Search"
                        className="me-2"
                        aria-label="Search"
                        onChange={(e) => { setSearch(e.target.value) }}
                    />
                    <Button type="submit" variant="outline-info" onClick={(e) => handleSubmit(e)}>Search</Button>
                </Form>
                <Nav className="nav-forms">
                    {ShowAuthNavigation()}
                    {ShowAdminNavigation()}
                </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}

export default NavbarComponent;