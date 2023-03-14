import React from "react";
import Modal from "react-bootstrap/Modal";
import { Button, ModalBody, ModalTitle} from 'react-bootstrap';
import axios from "axios";
import { useNavigate } from "react-router-dom";

const DeleteProfile = (props) => {
    const navigate = useNavigate();

    const Delete = () => {
        axios.delete(`${process.env.REACT_APP_API}/`,  { withCredentials: true })
            .then(() => {
                navigate(`/`);
                sessionStorage.removeItem("isUser");
                sessionStorage.removeItem("isAdmin");
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
                    Delete profile
                </ModalTitle>
            </Modal.Header>
            <ModalBody>
                <p>Are you sure you want to delete your account?</p>
            </ModalBody>
            <Modal.Footer>
                <Button onClick={() => Delete()} variant="outline-success">Yes</Button>
                <Button onClick={props.onHide} variant="outline-danger">No</Button>
            </Modal.Footer>
        </Modal>
    );
}

export default DeleteProfile;