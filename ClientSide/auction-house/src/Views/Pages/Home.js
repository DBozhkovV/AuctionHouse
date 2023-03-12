import React, { useState, useEffect } from 'react';
import '../../css/Home.css';
import '../../css/Item.css';
import '../../css/Loading.css';
import axios from 'axios';
import Card from 'react-bootstrap/Card';
import { useNavigate } from "react-router-dom";
import { Button } from "react-bootstrap";
import ListGroup from 'react-bootstrap/ListGroup';
import MoonLoader from "react-spinners/MoonLoader";

const Home = () => {
    const [items, setItems] = useState([]);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        const getItems = () => {
            axios.get(`${process.env.REACT_APP_API}/items/lastFiveNewest`)
                .then(response => {
                    setItems(response.data);
                    setLoading(false);
                })
                .catch(error => {
                    setLoading(false);
                    console.log(error);
                })
        }
        getItems();
    }, []);

    const routeChange = (id) => { 
        navigate(`/item/${id}`);
    }

    return (
        <div>
            <h1 className='home-header'>Welcome to the Auction house!</h1>
            <hr />
            <div>
                <h3 className='newest-item-header'>
                    Newest items:
                </h3>
                <div className="loader">
                    {loading ? <MoonLoader
                        color="#642d3c"
                        loading={loading}
                        size={100}
                    /> : null }
                </div>
                <div className="home-frame">
                    {items.map(item => (
                        <Card key={item.id} className="item-card">
                            <Card.Img 
                                variant="top" 
                                src={item.mainImage} 
                                className="card-image"
                            />
                            <Card.Body>
                            <Card.Title>{item.name} </Card.Title>
                            <Card.Text>{item.description} </Card.Text>
                            </Card.Body>
                            <ListGroup className="list-group-flush">
                            <ListGroup.Item>Buy now: {item.buyPrice} $</ListGroup.Item>
                            <ListGroup.Item>End bid date: {new Date(item.endBidDate).toLocaleString()}</ListGroup.Item>
                            </ListGroup>
                            <Card.Body className="card-footer">
                                <Button className="button" onClick={() => routeChange(item.id)}>View</Button>
                                <Card.Text>Bid now: {item.bid} $</Card.Text>
                            </Card.Body>
                        </Card>
                    ))}
                </div> 
                <hr /> 
            </div>
            <h1 className='home-footer'>Here you can buy, bid and sell items.</h1>
        </div>
    );
}

export default Home;