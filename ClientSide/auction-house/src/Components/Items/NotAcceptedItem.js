import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import { Button } from "react-bootstrap";
import "../../css/Item.css";
import Reject from "./AdminActions/Reject";
import Accept from "./AdminActions/Accept";
import Carousel from 'react-bootstrap/Carousel';

const NotAcceptedItem = () => {
    const params = useParams();
    const [item, setItem] = useState(null);
    const imagesToDisplay = [];
    const [showReject, setShowReject] = useState(false);
    const [showAccept, setShowAccept] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        const getItem = async () => {
            axios.get(`${process.env.REACT_APP_API}/items/not-accepted/${params.id}`, { withCredentials: true })
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
        return null 
    };

    imagesToDisplay.push(item.mainImage);
    for (let i = 0; i < item.images.length; i++) {
        imagesToDisplay.push(item.images[i]);
    }

    return (
        <div>
            <h3 className="item-head">{item.name}</h3>
            <hr/>
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
                    <div>Bid: {item.bid} $</div>
                    <hr/>
                    <div className="not-accepted-actions">
                        <Button variant="success" onClick={() => setShowAccept(true)}>Accept</Button>
                        <Accept show={showAccept} itemID={item.id} onHide={() => setShowAccept(false)} />
                        <Button variant="danger" onClick={() => setShowReject(true)}>Reject</Button>
                        <Reject show={showReject} itemID={item.id} onHide={() => setShowReject(false)} />
                    </div>
                </div>
            </div>
            <div className="item-buttons">
                <Button variant="primary" onClick={() => navigate(-1)}>Go back</Button>
            </div>
        </div>
    );
}

export default NotAcceptedItem;