import React from "react";
import Modal from "react-bootstrap/Modal";
import { Button, ModalBody, ModalTitle} from 'react-bootstrap';
import axios from "axios";
import { useNavigate } from "react-router-dom";

const Reject = (props) => {
    const navigate = useNavigate();

    const RejectItem = () => {
        axios.put(`${process.env.REACT_APP_API}/items/reject/${props.itemID}`, {}, { withCredentials: true })
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
                    Reject Item
                </ModalTitle>
            </Modal.Header>
            <ModalBody>
                <p>Are you sure you want to reject this item?</p>
            </ModalBody>
            <Modal.Footer>
                <Button onClick={() => RejectItem()} variant="outline-success">Yes</Button>
                <Button onClick={props.onHide} variant="outline-danger">No</Button>
            </Modal.Footer>
        </Modal>
    );
}

export default Reject;