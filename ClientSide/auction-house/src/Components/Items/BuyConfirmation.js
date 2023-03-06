import React from "react";
import Modal from "react-bootstrap/Modal";
import { Button, ModalBody, ModalTitle} from 'react-bootstrap';
import axios from "axios";
import { useNavigate } from "react-router-dom";

const BuyConfirmation = (props) => {
    const navigate = useNavigate();

    const BuyNow = () => {
        axios.put(`${process.env.REACT_APP_API}/items/buy/${props.itemID}`, 
            {}, 
            { 
                withCredentials: true 
            })
            .then(() => {
                navigate(`/profile`);
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
                    Buy now
                </ModalTitle>
            </Modal.Header>
            <ModalBody>
                <p>Are you sure you want to buy this item?</p>
            </ModalBody>
            <Modal.Footer>
                <Button onClick={() => BuyNow()} variant="outline-success">Yes</Button>
                <Button onClick={props.onHide} variant="outline-danger">No</Button>
            </Modal.Footer>
        </Modal>
    );
}

export default BuyConfirmation;