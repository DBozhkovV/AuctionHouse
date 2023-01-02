import React from "react";
import Modal from "react-bootstrap/Modal";
import { Button, ModalBody, ModalTitle} from 'react-bootstrap';
import axios from "axios";

const Logout = (props) => {
    
    const handleLogout = (e) => {
        e.preventDefault();
        axios.post(`${process.env.API_URL}/logout`, {})
        .catch(error => {
            console.log(error)
        })
    }
    
    return (
        <Modal
            {...props}
            size="lg"
            centered
        >
            <Modal.Header closeButton>
                <ModalTitle>
                    Logout
                </ModalTitle>
            </Modal.Header>
            <ModalBody>
                <p>Are you sure you want to logout?</p>
            </ModalBody>
            <Modal.Footer>
                <Button onClick={handleLogout} variant="primary">Yes</Button>
                <Button onClick={props.onHide}>Close</Button>
            </Modal.Footer>
        </Modal>
    );
}

export default Logout;