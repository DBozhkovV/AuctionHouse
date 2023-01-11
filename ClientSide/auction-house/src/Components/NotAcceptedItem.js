import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";
import { Button } from "react-bootstrap";
import "../css/Item.css";

const NotAcceptedItem = () => {
    const params = useParams();
    const [item, setItem] = useState(null);

    useEffect(() => {
        const getItem = async () => {
            axios.get(`https://localhost:7153/items/not-accepted/${params.id}`, { withCredentials: true })
                .then(response => {
                    setItem(response.data);
                })
                .catch(error => {
                    console.log(error);
                })
        }
        getItem();
    }, []);

    if(!item) return null;

    const AcceptItem = () => {
        axios.put(`https://localhost:7153/items/accept/${params.id}`, { withCredentials: true })
            .then(response => {
                console.log(response);
            })
            .catch(error => {
                console.log(error);
            })
    }

    const RejectItem = () => {
        axios.put(`https://localhost:7153/items/reject/${params.id}`, { withCredentials: true })
            .then(response => {
                console.log(response);
            })
            .catch(error => {
                console.log(error);
            })
    }

    return (
        <div>
            <header className="item-head">{item.name}</header>
            <div className="item-frame">
                <div>Image</div>
                <div>
                    <div>{item.description}</div>
                    <div>Buy Price: {item.buyPrice}</div>
                    <div>Starting Price: {item.startingPrice}</div>
                    <div>End Bid Date: {item.endBidDate}</div>
                    <div>Date Added: {item.dateAdded}</div>
                    <div>Bid: {item.bid}</div>
                </div>
            </div>
            <div className="not-accepted-buttons">
                    <Button className="button" onClick={AcceptItem}>Accept</Button>
                    <Button className="button" onClick={RejectItem}>Reject</Button>
            </div>
        </div>
    );
}

export default NotAcceptedItem;