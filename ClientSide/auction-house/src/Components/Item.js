import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";
import "../css/Item.css";

const Item = () => {
    const params = useParams();
    const [item, setItem] = useState(null);

    useEffect(() => {
        const getItem = async () => {
            axios.get(`https://localhost:7153/items/${params.id}`)
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
        </div>
    );
}

export default Item;