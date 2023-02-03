import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import Card from 'react-bootstrap/Card';
import ListGroup from 'react-bootstrap/ListGroup';
import { Button } from "react-bootstrap";
import "../../css/Item.css";

const ItemsByCategory = () => {
    const p = useParams();
    const [items, setItems] = useState(null);
    const navigate = useNavigate();
    
    const params = { 
        category: p.category
    };

    useEffect(() => {
        const getItems = async () => {
            axios.get(`${process.env.REACT_APP_API}/items/category`, { params })
                .then(response => {
                    setItems(response.data);
                })
                .catch(error => {
                    console.log(error);
                })
        }
        getItems();
    }, []); // eslint-disable-line react-hooks/exhaustive-deps

    if (!items) {
        return null;
    }

    const routeChange = (id) =>{ 
        navigate(`/item/${id}`);
    }

    return (
        <div>
            <h3 className="items-header">
                Items by category: {p.category}
            </h3>
            <hr/>
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
                            <ListGroup.Item>buyPrice: {item.result.buyPrice} </ListGroup.Item>
                            <ListGroup.Item>startingPrice: {item.result.startingPrice} </ListGroup.Item>
                        </ListGroup>
                        <Card.Body>
                            <Button className="button" onClick={() => routeChange(item.result.id)} >View</Button>
                        </Card.Body>
                    </Card>
                ))}
            </div>
        </div>
    )
}

export default ItemsByCategory;