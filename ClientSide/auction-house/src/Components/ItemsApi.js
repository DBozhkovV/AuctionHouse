import React, { useState, useEffect } from "react";
import axios from "axios";
import Card from 'react-bootstrap/Card';
import ListGroup from 'react-bootstrap/ListGroup';
import { Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const ItemsApi = () => {
    const [items, setItems] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const getItems = async () => {
            axios.get(`https://localhost:7153/items`)
                .then(response => {
                    setItems(response.data);
                })
                .catch(error => {
                    console.log(error)
                })
        }
        getItems();
    }, []);

    if(!items) return null;

    const routeChange = (id) =>{ 
        navigate(`/item/${id}`);
        window.location.reload();
    }

    return (
        <div>
            <header className="items-header">
                Items
            </header>
            <div className="items-frame">
                {items.map(item => (
                    <Card key={item.id} style={{ width: '18rem' }}>
                        {/* <Card.Img variant="top" src="holder.js/100px180?text=Image cap" /> */}
                        <Card.Body>
                        <Card.Title>{item.name} </Card.Title>
                        <Card.Text>{item.description} </Card.Text>
                        </Card.Body>
                        <ListGroup className="list-group-flush">
                        <ListGroup.Item>buyPrice: {item.buyPrice} </ListGroup.Item>
                        <ListGroup.Item>startingPrice: {item.startingPrice} </ListGroup.Item>
                        {/* <ListGroup.Item>endBidDate: {item.endBidDate} </ListGroup.Item> */}
                        </ListGroup>
                        <Card.Body>
                            <Button className="button" onClick={() => routeChange(item.id)}>View</Button>
                        </Card.Body>
                    </Card>
                ))}
            </div>
        </div>
    );
}

export default ItemsApi;