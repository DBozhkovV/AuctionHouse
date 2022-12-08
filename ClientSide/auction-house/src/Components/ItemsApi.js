import React, { useState, useEffect } from "react";
import axios from "axios";
import Card from 'react-bootstrap/Card';
import ListGroup from 'react-bootstrap/ListGroup';

const ItemsApi = () => {
    const [items, setItems] = useState([]);
    
    useEffect(() => {
        const getItems = async () => {
            axios.get(`http://localhost:5153/items`)
                .then(response => {
                    setItems(response.data);
                })
                .catch(error => {
                    console.log(error)
                })
        }
        getItems();
    }, []);

    return (
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
                    <ListGroup.Item>endBidDate: {item.endBidDate} </ListGroup.Item>
                    </ListGroup>
                    <Card.Body>
                    <Card.Link href="#">Card Link</Card.Link>
                    <Card.Link href="#">Another Link</Card.Link>
                    </Card.Body>
                </Card>
            ))}
        </div>);
}

export default ItemsApi;