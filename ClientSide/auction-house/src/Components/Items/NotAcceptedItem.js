import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import { Button } from "react-bootstrap";
import "../../css/Item.css";
import { VscArrowRight } from "react-icons/vsc";

const NotAcceptedItem = () => {
    const params = useParams();
    const [item, setItem] = useState(null);

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
            <header className="item-head">{item.name}</header>
            <div className="item-frame">
                <div className="image-frame">
                    <img className="not-accepted-img" src={`data:${item.mainImage.imageType};base64,${item.mainImage.image}`}/>
                    <div className="button-img-frame">
                        <Button className="button-next-img" variant="outline-primary">
                            <VscArrowRight className="arrow-next"/>
                        </Button>
                    </div>
                </div>
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
                <Button variant="outline-success" onClick={() => {AcceptItem(item.id); navigate(-1);}}>Accept</Button>
                <Button variant="outline-danger" onClick={() => {RejectItem(item.id); navigate(-1);}}>Reject</Button>
            </div>
        </div>
    );
}

export default NotAcceptedItem;