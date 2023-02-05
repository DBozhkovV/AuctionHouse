import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";
import "../../css/Order.css";
import { VscArrowRight } from "react-icons/vsc";
import { Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const Order = () => {
    const params = useParams();
    const [order, setOrder] = useState(null);

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
                    <img 
                        className="order-img" 
                        src={`data:${order.itemResponse.result.mainImage.imageType};base64,${order.itemResponse.result.mainImage.image}`}
                        alt=""
                    />
                    <div className="button-img-frame">
                        <Button className="button-next-img" variant="outline-primary">
                            <VscArrowRight className="arrow-next"/>
                        </Button>
                    </div>
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