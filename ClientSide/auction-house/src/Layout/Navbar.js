import React from 'react';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';

const NavbarComponent = () => {
    return(
        <Navbar bg="light" expand="lg">
            <Container>
            <Navbar.Brand href="#home"> Auction House </Navbar.Brand>
            <Navbar.Toggle aria-controls="basic-navbar-nav" />
            <Navbar.Collapse id="basic-navbar-nav">
                <Nav className="me-auto">
                <Nav.Link href="/">Home</Nav.Link>
                <Nav.Link href="items">Items</Nav.Link>
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
                </Navbar.Collapse>
                <Navbar.Collapse className="justify-content-end">
                    <Nav.Link href="register">Register</Nav.Link>
                    <Nav.Link href="login">Login</Nav.Link>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    );
}

export default NavbarComponent;