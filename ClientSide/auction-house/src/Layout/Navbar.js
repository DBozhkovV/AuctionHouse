import React, { useState } from 'react';
import { Container, Nav, Navbar, NavDropdown, Form, Button } from 'react-bootstrap';
import Logout from '../Components/Logout';

const NavbarComponent = () => {
    const [search, setSearch] = useState(null);
    const [LogoutShow, setLogoutShow] = useState(false);

    const handleSubmit = () => {
        window.location.href = `/items/search/${search}`;
    }

    return(
        <Navbar bg="light" expand="lg">
            <Container>
            <Navbar.Brand href="/"> Auction House </Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
                <Nav className="me-auto" >
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
                    <Button variant="outline-success" onClick={handleSubmit}>Search</Button>
                </Form>
                </Navbar.Collapse>
                <Navbar.Collapse className="justify-content-end">
                    <Nav.Link href="register">Register</Nav.Link>
                    <Nav.Link href="login">Login</Nav.Link>
                    <Nav.Link onClick={() => setLogoutShow(true)}>Logout</Nav.Link>
                        <Logout show = {LogoutShow} onHide={() => setLogoutShow(false)}/>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}

export default NavbarComponent;