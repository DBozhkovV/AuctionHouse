import React from "react";
import Modal from "react-bootstrap/Modal";
import { Button, ModalBody, ModalTitle} from 'react-bootstrap';
import axios from "axios";

const DeleteOrder = (props) => {
    const routeChange = () =>{ 
        window.location.reload();
    }
    
    const handleDelete = async () => {
        axios.delete(`${process.env.REACT_APP_API}/orders/${props.itemID}`, { withCredentials: true })
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
                    Delete
                </ModalTitle>
            </Modal.Header>
            <ModalBody>
                <p>Are you sure you want to delete this order?</p>
            </ModalBody>
            <Modal.Footer>
                <Button onClick={() => {handleDelete(); routeChange();}} variant="outline-success">Yes</Button>
                <Button onClick={props.onHide} variant="outline-danger">No</Button>
            </Modal.Footer>
        </Modal>
    );
}

export default DeleteOrder;