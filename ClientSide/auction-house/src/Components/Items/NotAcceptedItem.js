import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import { Button } from "react-bootstrap";
import "../../css/Item.css";
import { VscArrowRight } from "react-icons/vsc";
import { VscArrowLeft } from "react-icons/vsc";

const NotAcceptedItem = () => {
    const params = useParams();
    const [item, setItem] = useState(null);
    const imagesToDisplay = [];
    const [imageIndex, setImageIndex] = useState(0);
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

    const AcceptItem = (id) => {
        axios.put(`${process.env.REACT_APP_API}/items/accept/${id}`, {}, { withCredentials: true })
            .catch(error => {
                console.log(error);
            })
    }

    const RejectItem = (id) => {
        axios.put(`${process.env.REACT_APP_API}/items/reject/${id}`, {}, { withCredentials: true })
            .catch(error => {
                console.log(error);
            })
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
                        <Button variant="outline-success" onClick={() => {AcceptItem(item.id); navigate(-1);}}>Accept</Button>
                        <Button variant="outline-danger" onClick={() => {RejectItem(item.id); navigate(-1);}}>Reject</Button>
                    </div>
                </div>
            </div>
            <div className="item-buttons">
                <Button variant="outline-primary" onClick={() => navigate(-1)}>Go back</Button>
            </div>
        </div>
    );
}

export default NotAcceptedItem;