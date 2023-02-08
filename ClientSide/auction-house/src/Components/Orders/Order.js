import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";
import "../../css/Order.css";
import "../../css/Item.css";
import { VscArrowRight } from "react-icons/vsc";
import { VscArrowLeft } from "react-icons/vsc"; 
import { Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const Order = () => {
    const params = useParams();
    const [order, setOrder] = useState(null);
    const imagesToDisplay = [];
    const [imageIndex, setImageIndex] = useState(0);
    const navigate = useNavigate();

    useEffect(() => {
        const getItem = async () => {
            axios.get(`${process.env.REACT_APP_API}/orders/${params.id}`, { withCredentials: true })
                .then(response => {
                    setOrder(response.data);
                })
                .catch(error => {
                    console.log(error);
                })
        }
        getItem();
    }, []);

    useEffect(() => {
        if (order) {
            return;
        }
    }, [imageIndex]);

    if(!order) {
        return null;
    }

    imagesToDisplay.push(order.itemResponse.result.mainImage);
    for (let i = 0; i < order.itemResponse.result.images.length; i++) {
        imagesToDisplay.push(order.itemResponse.result.images[i]);
    }

    return (
        <div>
            <h3 className="order-header">Your order for item: {order.itemResponse.result.name}</h3>
            <hr/>
            <div className="order-info-header">
                <h6>
                    Date ordered: {new Date(Date.parse(order.dateOrdered)).toLocaleString()}
                </h6>
                <h6>
                    Bought for: {order.itemResponse.result.boughtFor} $
                </h6>
            </div>
            <div className="order-frame">
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
                        src={`data:${imagesToDisplay[imageIndex].imageType};base64,${imagesToDisplay[imageIndex].image}`}
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
                    <div>{order.itemResponse.result.description}</div>
                    <hr/>
                    <div>Buy Price: {order.itemResponse.result.buyPrice} $</div>
                    <div>Starting Price: {order.itemResponse.result.startingPrice} $</div>
                    <hr/>
                    <div>End Bid Date: {new Date(Date.parse(order.itemResponse.result.endBidDate)).toLocaleString()}</div>
                    <div>Date Added: {new Date(Date.parse(order.itemResponse.result.dateAdded)).toLocaleString()}</div>
                    <hr/>
                    <div>Bid: {order.itemResponse.result.bid} $</div>
                </div>
            </div>
            <div className="order-goback-button">
                <Button variant="outline-primary" onClick={() => navigate(-1)}>Go back</Button>
            </div>
        </div>
    );
}

export default Order;