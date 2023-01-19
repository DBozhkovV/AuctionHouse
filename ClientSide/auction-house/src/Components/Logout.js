import React from "react";
import Modal from "react-bootstrap/Modal";
import { Button, ModalBody, ModalTitle} from 'react-bootstrap';
import axios from "axios";
import { useNavigate } from "react-router-dom";

const Logout = (props) => {
    const navigate = useNavigate();

    const routeChange = () =>{ 
        navigate(`/`);
        window.location.reload();
    }

    const handleLogout = async () => {
        axios.post(`https://localhost:7153/logout`, {}, { withCredentials: true })
        .catch(error => {
            console.log(error);
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
                <Button onClick={() => {handleLogout(); routeChange();}} variant="primary">Yes</Button>
                <Button onClick={props.onHide}>Close</Button>
            </Modal.Footer>
        </Modal>
    );
}

export default Logout;