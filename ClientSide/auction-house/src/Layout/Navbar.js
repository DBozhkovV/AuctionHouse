import React, { useState } from 'react';
import { Container, Nav, Navbar, NavDropdown, Form, Button } from 'react-bootstrap';
import Logout from '../Components/Logout';
import IsLoged from '../Components/IsLoged';
import IsAdmin from '../Components/IsAdmin';
import logo from '../Assets/images/user-profile-icon.svg';

const NavbarComponent = () => {
    const [search, setSearch] = useState(null);
    const [LogoutShow, setLogoutShow] = useState(false);

    const handleSubmit = () => {
        window.location.href = `/items/search/${search}`;
    }

    const isAdmin = IsAdmin();

    const ShowAuthNavigation = () => { // async???
        if (IsLoged()) {
            return  (
                <>
                    <Nav.Link href="/post">Post</Nav.Link>
                    <Nav.Link onClick={() => setLogoutShow(true)}>Logout</Nav.Link>
                        <Logout show = {LogoutShow} onHide={() => setLogoutShow(false)}/> 
                    <Navbar.Brand href="/profile">
                    <img
                        src={logo}
                        width="30"
                        height="30"
                        className="d-inline-block align-top"
                    />
                    </Navbar.Brand>
                </>
            );
        }
        if (isAdmin === false) {
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
        <Navbar bg="light" expand="lg">
            <Container>
            <Navbar.Brand href="/"> Auction House </Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
                <Nav variant="pills" className="me-auto" >
                <Nav.Link href="/">Home</Nav.Link>
                <Nav.Link href="/items">Items</Nav.Link>
                <NavDropdown title="Category" id="basic-nav-dropdown">
                    <NavDropdown.Item href="#action/3.1">Pictures</NavDropdown.Item>
                    <NavDropdown.Item href="#action/3.2">Jewellery   </NavDropdown.Item>
                    <NavDropdown.Item href="#action/3.3">Books</NavDropdown.Item>
                    <NavDropdown.Divider />
                    <NavDropdown.Item href="#action/3.4">
                        Something else
                    </NavDropdown.Item>
                </NavDropdown>
                </Nav>
                <Form className="d-flex">
                    <Form.Control
                        type="search"
                        placeholder="Search"
                        className="me-2"
                        aria-label="Search"
                        onChange={(e) => { setSearch(e.target.value) }}
                    />
                    <Button type="button" variant="outline-success" onClick={handleSubmit}>Search</Button>
                </Form>
                </Navbar.Collapse>
                <Navbar.Collapse className="justify-content-end">
                    {ShowAuthNavigation()}
                    {ShowAdminNavigation()}
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}

export default NavbarComponent;