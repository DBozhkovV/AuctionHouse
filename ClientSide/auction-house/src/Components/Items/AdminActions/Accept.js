import React from "react";
import Modal from "react-bootstrap/Modal";
import { Button, ModalBody, ModalTitle} from 'react-bootstrap';
import axios from "axios";
import { useNavigate } from "react-router-dom";

const Accept = (props) => {
    const navigate = useNavigate();

    const AcceptItem = () => {
        axios.put(`${process.env.REACT_APP_API}/items/accept/${props.itemID}`, {}, { withCredentials: true })
            .then(() => {
                navigate("/admin");
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
                    Accept Item
                </ModalTitle>
            </Modal.Header>
            <ModalBody>
                <p>Are you sure you want to accept this item?</p>
            </ModalBody>
            <Modal.Footer>
                <Button onClick={() => AcceptItem()} variant="outline-success">Yes</Button>
                <Button onClick={props.onHide} variant="outline-danger">No</Button>
            </Modal.Footer>
        </Modal>
    );
}

export default Accept;