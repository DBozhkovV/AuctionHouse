import React, { useState, useEffect } from "react";
import axios from "axios";
import Card from 'react-bootstrap/Card';
import ListGroup from 'react-bootstrap/ListGroup';
import { Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import Fail from "../Alerts/Fail";
import MoonLoader from "react-spinners/MoonLoader";
import "../../css/Loading.css";

const ItemsApi = () => {
    const [items, setItems] = useState([]);
    const [showFail, setShowFail] = useState(false);
    const [message, setMessage] = useState("");
    const [loading, setLoading] = useState(true);

    const navigate = useNavigate();

    useEffect(() => {
        const getItems = async () => {
            axios.get(`${process.env.REACT_APP_API}/items`)
                .then(response => {
                    setItems(response.data);
                    setLoading(false);
                })
                .catch(error => {
                    setShowFail(true);
                    setMessage(error.response.data);
                    setLoading(false);
                })
        }
        getItems();
    }, []);
    
    const routeChange = (id) => { 
        navigate(`/item/${id}`);
    }

    return (
        <div>
            {showFail ? <Fail error={message}/> : null}
            <h3 className="items-header">
                Items
            </h3>
            <hr />
            <div>Sort by:</div>
            <div className="loader">
                {loading ? <MoonLoader
                    color="#642d3c"
                    loading={loading}
                    size={100}
                    /> : null }
            </div>
            <div className="items-frame">
                {items.map(item => (
                    <Card key={item.result.id} className="item-card">
                        <Card.Img 
                            variant="top" 
                            src={`data:${item.result.mainImage.imageType};base64,${item.result.mainImage.image}`} 
                            className="card-image"
                        />
                        <Card.Body>
                        <Card.Title>{item.result.name} </Card.Title>
                        <Card.Text>{item.result.description} </Card.Text>
                        </Card.Body>
                        <ListGroup className="list-group-flush">
                        <ListGroup.Item>Buy now: {item.result.buyPrice} $</ListGroup.Item>
                        <ListGroup.Item>End bid date: {new Date(item.result.endBidDate).toLocaleString()}</ListGroup.Item>
                        </ListGroup>
                        <Card.Body className="card-footer">
                            <Button className="button" onClick={() => routeChange(item.result.id)}>View</Button>
                            <Card.Text>Bid now: {item.result.bid} $</Card.Text>
                        </Card.Body>
                    </Card>
                ))}
            </div>
        </div>
    );
}

export default ItemsApi;