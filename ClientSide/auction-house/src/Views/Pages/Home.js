import React, { useState, useEffect } from 'react';
import '../../css/Home.css';
import '../../css/Item.css';
import axios from 'axios';
import Card from 'react-bootstrap/Card';
import { useNavigate } from "react-router-dom";
import { Button } from "react-bootstrap";
import ListGroup from 'react-bootstrap/ListGroup';

const Home = () => {
    const [items, setItems] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const getItems = () => {
            axios.get(`${process.env.REACT_APP_API}/items/newest`)
                .then(response => {
                    setItems(response.data);
                })
                .catch(error => {
                    console.log(error);
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
            <h1 className='home-header'>Welcome to the Auction house!</h1>
            <hr />
            <div>
                <h3 className='newest-item-header'>
                    Newest items
                </h3>
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
                <hr /> 
                {/* <p className='home-text-p'>This is a website where you can buy and sell items.</p>
                    <p className='home-text-p'>You can create an account and start selling your items.</p>
                    <p className='home-text-p'>You can also buy items from other users.</p>
                    <p className='home-text-p'>You can also bid on items.</p>
                    <p className='home-text-p'>You can also see your profile and your orders.</p>
                    <p className='home-text-p'>You can also see your items.</p>
                    <p className='home-text-p'>You can also see your bids.</p>
                    <p className='home-text-p'>You can also see your notifications.</p>
                    <p className='home-text-p'>You can also see your messages.</p>
                    <p className='home-text-p'>You can also see your friends.</p>
                    <p className='home-text-p'>You can also see your friend requests.</p>
                    <p className='home-text-p'>You can also see your search history.</p> */}
            </div>
        </div>
    );
}

export default Home;