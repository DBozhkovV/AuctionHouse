import React, { useState, useEffect } from "react";
import Card from 'react-bootstrap/Card';
import ListGroup from 'react-bootstrap/ListGroup';
import axios from "axios";
import { Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';

const Admin = () => {
    const [items, setItems] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const getItems = async () => {
            axios.get(`https://localhost:7153/items/not-accepted`, { withCredentials: true })
                .then(response => {
                    setItems(response.data);
                })
                .catch(error => {
                    console.log(error);
                })
        }
        getItems();
    }, []);

    if(!items) return null;

    const routeChange = (id) =>{ 
        navigate(`/notaccepteditem/${id}`);
        window.location.reload();
    }

    return (
        <div>
            <header className="items-header">
                Items
            </header>
            <div className="items-frame">
                {items.map(item => (
                    <Card key={item.result.id} style={{ width: '18rem' }}>
                        <Card.Img variant="top" src={`data:${item.result.mainImage.imageType};base64,${item.result.mainImage.image}`} />
                        <Card.Body>
                            <Card.Title>{item.result.name}</Card.Title>
                            <Card.Text>{item.result.description} </Card.Text>
                        </Card.Body>
                        <ListGroup className="list-group-flush">
                        <ListGroup.Item>buyPrice: {item.result.buyPrice} </ListGroup.Item>
                        <ListGroup.Item>startingPrice: {item.result.startingPrice} </ListGroup.Item>
                        {/* <ListGroup.Item>endBidDate: {item.endBidDate} </ListGroup.Item> */}
                        </ListGroup>
                        <Card.Body>
                            <Button className="button" onClick={() => routeChange(item.result.id)}>View</Button>
                        </Card.Body>
                    </Card>
                ))}
            </div>
        </div>
    );
}

export default Admin;