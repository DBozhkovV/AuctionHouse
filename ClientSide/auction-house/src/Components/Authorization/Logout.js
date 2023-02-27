import React from "react";
import Modal from "react-bootstrap/Modal";
import { Button, ModalBody, ModalTitle} from 'react-bootstrap';
import axios from "axios";
import { useNavigate } from "react-router-dom";

const Logout = (props) => {
    const navigate = useNavigate();

    const handleLogout = async () => {
        await axios.post(`${process.env.REACT_APP_API}/logout`, {}, { withCredentials: true })
        .then(() => {
            sessionStorage.removeItem("isUser");
            sessionStorage.removeItem("isAdmin");
            navigate(`/`);
            window.location.reload();
        })
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
                <Button onClick={() => handleLogout()} variant="outline-success">Yes</Button>
                <Button onClick={props.onHide} variant="outline-danger">No</Button>
            </Modal.Footer>
        </Modal>
    );
}

export default Logout;