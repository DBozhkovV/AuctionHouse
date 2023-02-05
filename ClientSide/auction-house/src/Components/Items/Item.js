import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";
import "../../css/Item.css";
import { VscArrowRight } from "react-icons/vsc";
import { Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import InputGroup from 'react-bootstrap/InputGroup';
import Form from 'react-bootstrap/Form';

const Item = () => {
    const params = useParams();
    const [item, setItem] = useState(null);
    const [imagesToDisplay, setImagesToDisplay] = useState([[]]);
    const [bid, setBid] = useState(0);
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

    if(!item) {
        return null;
    } 

    const BuyNow = () => {
        axios.put(`${process.env.REACT_APP_API}/items/buy/${item.id}`, {}, { withCredentials: true })
            .then(response => {
                console.log(response);
                // navigate(`/items/${item.id}`);
                // window.location.reload();
            })
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
            <h3 className="item-head">{item.name}</h3>
            <div className="item-frame">
                <div className="image-frame">
                    <img 
                        className="not-accepted-img" 
                        src={`data:${item.mainImage.imageType};base64,${item.mainImage.image}`}
                        alt=""
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
                    <div>Buy Price: {item.buyPrice} $</div>
                    <div>Starting Price: {item.startingPrice} $</div>
                    <hr/>
                    <div>End Bid Date: {new Date(item.endBidDate).toLocaleString()}</div>
                    <div>Date Added: {new Date(item.dateAdded).toLocaleString()}</div>
                    <hr/>
                    <div>Bid now: {item.bid} $</div>
                    <hr/>
                    <div className="not-accepted-actions">
                        <Button variant="outline-primary" onClick={() => BuyNow()}>Buy now</Button>
                        <Form.Group className="bid-form">
                            <Button variant="outline-primary">Bid</Button>
                            <InputGroup size="sm">
                                <InputGroup.Text>$</InputGroup.Text>
                                <Form.Control   
                                    aria-label="Amount"
                                    type="number"
                                    min={item.bid + 1}  
                                    required
                                    onChange={(e) => setBid(e.target.value)}
                                />
                                <InputGroup.Text>.00</InputGroup.Text>
                            </InputGroup>
                        </Form.Group>
                    </div>
                </div>
            </div>
            <div className="not-accepted-buttons">
                <Button variant="outline-primary" onClick={() => navigate(-1)}>Go back</Button>
            </div>
        </div>
    );
}

export default Item;