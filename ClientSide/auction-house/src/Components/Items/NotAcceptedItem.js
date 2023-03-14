import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import { Button } from "react-bootstrap";
import "../../css/Item.css";
import { VscArrowRight } from "react-icons/vsc";
import { VscArrowLeft } from "react-icons/vsc";
import Reject from "./AdminActions/Reject";
import Accept from "./AdminActions/Accept";

const NotAcceptedItem = () => {
    const params = useParams();
    const [item, setItem] = useState(null);
    const imagesToDisplay = [];
    const [imageIndex, setImageIndex] = useState(0);
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

    useEffect(() => {
        if (item) {
            return;
        }
    }, [imageIndex]);

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
                    {imageIndex !== 0 && 
                        <div className="button-previous-img-frame">
                            <Button 
                                className="button-img" 
                                variant="outline-primary"
                                onClick={() =>  {
                                    if (imageIndex === 0) {
                                        return;
                                    }else {
                                        setImageIndex(imageIndex - 1);
                                    }
                                }}
                            >
                                <VscArrowLeft className="arrow-next"/>
                            </Button>
                        </div>
                    }
                    <img 
                        className="item-img" 
                        src={imagesToDisplay[imageIndex]}
                        alt=""
                    />
                    {imageIndex !== imagesToDisplay.length - 1 && ( 
                        <div className="button-next-img-frame">
                            <Button 
                                className="button-img" 
                                variant="outline-primary"
                                onClick={() =>  {
                                    if (imageIndex === imagesToDisplay.length - 1) {
                                        return;
                                    }else {
                                        setImageIndex(imageIndex + 1);
                                    }
                                }}
                            >
                                <VscArrowRight className="arrow-next"/>
                            </Button>
                        </div>
                    )}
                </div>
                <div>
                    <div>{item.description}</div>
                    <hr/>
                    <div>Buy Price: {item.buyPrice}</div>
                    <div>Starting Price: {item.startingPrice}</div>
                    <hr/>
                    <div>End Bid Date: {item.endBidDate}</div>
                    <div>Date Added: {item.dateAdded}</div>
                    <hr/>
                    <div>Bid: {item.bid}</div>
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