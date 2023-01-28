import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";
import "../css/Item.css";
import { VscArrowRight } from "react-icons/vsc";
import { Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const Item = () => {
    const params = useParams();
    const [item, setItem] = useState(null);
    const [imagesToDisplay, setImagesToDisplay] = useState([[]]);

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

    if(!item) return null;

    const GoBack = () => {
        navigate(`/items`);
    }

    const RejectItem = (id) => {
        axios.put(`${process.env.REACT_APP_API}/items/reject/${id}`, {}, { withCredentials: true })
            .catch(error => {
                console.log(error);
            })
    }

    const displayImage = () => {
        //setImagesToDisplay(item.mainImage);
        //setImagesToDisplay([...item.mainImage, item.images]);
    }

    return (
        <div>
            {displayImage()}
            <h3 className="item-head">{item.name}</h3>
            <div className="item-frame">
                <div className="image-frame">
                    <img 
                        className="not-accepted-img" 
                        src={`data:${item.mainImage.imageType};base64,${item.mainImage.image}`}
                    />
                    <div className="button-img-frame">
                        <Button className="button-next-img" variant="outline-primary">
                            <VscArrowRight className="arrow-next"/>
                        </Button>
                    </div>
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
                </div>
            </div>
            <div className="not-accepted-buttons">
                <Button variant="outline-primary" onClick={() => GoBack()}>Go back</Button>
                <Button variant="outline-danger" onClick={() => RejectItem(item.id)}>Reject</Button>
            </div>
        </div>
    );
}

export default Item;