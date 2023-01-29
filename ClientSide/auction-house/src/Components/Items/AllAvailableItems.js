import React, { useState, useEffect } from "react";
import axios from "axios";
import Card from 'react-bootstrap/Card';
import ListGroup from 'react-bootstrap/ListGroup';
import { Button } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';
import Fail from "../Alerts/Fail";

const ItemsApi = () => {
    const [items, setItems] = useState([]);
    const [showFail, setShowFail] = useState(false);
    const [message, setMessage] = useState("");
    const navigate = useNavigate();

    useEffect(() => {
        const getItems = async () => {
            axios.get(`${process.env.REACT_APP_API}/items`)
                .then(response => {
                    setItems(response.data);
                })
                .catch(error => {
                    setShowFail(true);
                    setMessage(error.response.data);
                })
        }
        getItems();
    }, []);

    if(!items) {
        return null; 
    }
    
    const routeChange = (id) => { 
        navigate(`/item/${id}`);
    }

    return (
        <div>
            {showFail ? <Fail error={message}/> : null}
            <h3 className="items-header">
                Items
            </h3>
            <Row xs={1} md={2} className="g-4">
                <div className="items-frame">
                    {items.map(item => (
                        <Col>
                            <Card key={item.result.id} style={{ width: '18rem' }}>
                                <Card.Img 
                                    variant="top" 
                                    src={`data:${item.result.mainImage.imageType};base64,${item.result.mainImage.image}`} 
                                />
                                <Card.Body>
                                <Card.Title>{item.result.name} </Card.Title>
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
                        </Col>
                    ))}
                </div>
            </Row>
        </div>
    );
}

export default ItemsApi;