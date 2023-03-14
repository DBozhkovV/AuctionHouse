import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";
import "../../css/Order.css";
import "../../css/Item.css";
import { Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import Carousel from 'react-bootstrap/Carousel';

const Order = () => {
    const params = useParams();
    const [order, setOrder] = useState(null);
    const imagesToDisplay = [];
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

    if(!order) {
        return null;
    }

    imagesToDisplay.push(order.itemResponse.mainImage);
    for (let i = 0; i < order.itemResponse.images.length; i++) {
        imagesToDisplay.push(order.itemResponse.images[i]);
    }

    return (
        <div>
            <h3 className="order-header">Your order for item: {order.name}</h3>
            <hr/>
            <div className="order-info-header">
                <h6>
                    Date ordered: {new Date(Date.parse(order.dateOrdered)).toLocaleString()}
                </h6>
                <h6>
                    Bought for: {order.itemResponse.boughtFor} $
                </h6>
            </div>
            <div className="order-frame">
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
                    <div>{order.itemResponse.description}</div>
                    <hr/>
                    <div>Buy Price: {order.itemResponse.buyPrice} $</div>
                    <div>Starting Price: {order.itemResponse.startingPrice} $</div>
                    <hr/>
                    <div>End Bid Date: {new Date(Date.parse(order.itemResponse.endBidDate)).toLocaleString()}</div>
                    <div>Date Added: {new Date(Date.parse(order.itemResponse.dateAdded)).toLocaleString()}</div>
                    <hr/>
                </div>
            </div>
            <div className="order-goback-button">
                <Button variant="primary" onClick={() => navigate(-1)}>Go back</Button>
            </div>
        </div>
    );
}

export default Order;