import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";
import "../../css/Item.css";
import { Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import InputGroup from 'react-bootstrap/InputGroup';
import Form from 'react-bootstrap/Form';
import Carousel from 'react-bootstrap/Carousel';
import BuyConfirmation from "./BuyConfirmation";
import BidConfirmation from "./BidConfirmation";

const Item = () => {
    const params = useParams();
    const [item, setItem] = useState(null);
    const imagesToDisplay = [];
    const [bid, setBid] = useState(0);
    const [showBid, setShowBid] = useState(false);
    const [showBuy, setShowBuy] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        const getItem = async () => {
            axios.get(`${process.env.REACT_APP_API}/items/${params.id}`)
                .then(response => {
                    setItem(response.data);
                })
                .catch(error => {
                    console.log(error);
                })
        }
        getItem();
    }, []);

    if(!item) {
        return null;
    } 

    imagesToDisplay.push(item.mainImage);
    for (let i = 0; i < item.images.length; i++) {
        imagesToDisplay.push(item.images[i]);
    }

    return (
        <div>
            <h3 className="item-head">
                {item.name}
            </h3>
            <hr />
            <div className="item-frame">
                <div className="image-frame">
                    <Carousel>
                        {imagesToDisplay.map((image, index) => (
                            <Carousel.Item key={index}>
                                <img
                                    className="item-images"
                                    src={image}
                                />
                            </Carousel.Item>
                        ))}
                    </Carousel>
                </div>
                <div>
                    <div>{item.description}</div>
                    <hr/>
                    <div>Buy Price: {item.buyPrice} $</div>
                    <div>Starting Price: {item.startingPrice} $</div>
                    <hr/>
                    <div>End Bid Date: {new Date(item.endBidDate).toLocaleString()}</div>
                    <div>Date Added: {new Date(item.dateAdded).toLocaleString()}</div>
                    <hr/>
                    <div>Bid now: {item.bid} $</div>
                    <hr/>
                    <div className="not-accepted-actions">
                        <Button variant="outline-primary" onClick={() => setShowBuy(true)}>Buy now</Button>
                        <BuyConfirmation show={showBuy} itemID={item.id} onHide={() => setShowBuy(false)} />
                        <Form.Group className="bid-form">
                            <Button variant="outline-primary" onClick={() => setShowBid(true)}>Bid</Button>
                            <BidConfirmation show={showBid} itemID={item.id} bid={bid} onHide={() => setShowBid(false)} />
                            <InputGroup size="sm">
                                <InputGroup.Text>$</InputGroup.Text>
                                <Form.Control   
                                    aria-label="Amount"
                                    type="number"
                                    min={item.bid + 1}  
                                    required
                                    onChange={(e) => setBid(e.target.value)}
                                />
                                <InputGroup.Text>.00</InputGroup.Text>
                            </InputGroup>
                        </Form.Group>
                    </div>
                </div>
            </div>
            <div className="item-buttons">
                <Button variant="primary" onClick={() => navigate(-1)}>Go back</Button>
            </div>
        </div>
    );
}

export default Item;