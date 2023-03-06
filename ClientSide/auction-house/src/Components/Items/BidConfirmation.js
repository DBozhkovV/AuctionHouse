import React from "react";
import Modal from "react-bootstrap/Modal";
import { Button, ModalBody, ModalTitle} from 'react-bootstrap';
import axios from "axios";
import { useNavigate } from "react-router-dom";

const BidConfirmation = (props) => {
    const navigate = useNavigate();

    const Bid = () => {
        axios.put(`${process.env.REACT_APP_API}/items/bid`, 
            { 
                itemId: props.itemID,
                money: props.bid 
            }, 
            {
                withCredentials: true
            })
            .then(() => {
                navigate(`/bids`);
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
                    Bid
                </ModalTitle>
            </Modal.Header>
            <ModalBody>
                <p>Are you sure you want to bid for this item?</p>
            </ModalBody>
            <Modal.Footer>
                <Button onClick={() => Bid()} variant="outline-success">Yes</Button>
                <Button onClick={props.onHide} variant="outline-danger">No</Button>
            </Modal.Footer>
        </Modal>
    );
}

export default BidConfirmation;