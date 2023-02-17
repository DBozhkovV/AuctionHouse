import axios from "axios";
import React, { useState, useEffect } from "react";
import MoonLoader from "react-spinners/MoonLoader";
import "../../../css/Loading.css";
import { useNavigate } from "react-router-dom";
import Card from 'react-bootstrap/Card';
import ListGroup from 'react-bootstrap/ListGroup';
import { Button } from "react-bootstrap";
import ButtonGroup from 'react-bootstrap/ButtonGroup';
import DropdownButton from 'react-bootstrap/DropdownButton';
import Dropdown from 'react-bootstrap/Dropdown';

const SortByLowToHigh = () => {
    const [items, setItems] = useState([]);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        const getItems = async () => {
            await axios.get(`${process.env.REACT_APP_API}/items/sortByLowToHigh`)
                .then(response => {
                    setItems(response.data);
                    setLoading(false);
                })
                .catch(error => {
                    setLoading(false);
                    console.error(error);
                })
        }
        getItems();
    }, []);

    const routeChange = (id) => { 
        navigate(`/item/${id}`);
    }

    const sortItems = (type) => {
        switch(type) {
            case "newest":
                navigate(`/items/newest`);
                break;
            case "high-low":
                navigate(`/items/sortByHighToLow`);
                break;
            case "low-high":
                navigate(`/items/sortByLowToHigh`);
                break;
            default: // if not match any case
                break;
        }
    }

    return (
        <div>
            <h3 className="items-header">
                Sorted by low to high
            </h3>
            <hr />
            <div className="sort-button">
                <ButtonGroup>
                    <DropdownButton variant="secondary" title="Sort by:">
                        <Dropdown.Item onClick={() => sortItems("newest")}>Newest</Dropdown.Item>
                        <Dropdown.Item onClick={() => sortItems("high-low")}>Price: High-Low</Dropdown.Item>
                        <Dropdown.Item onClick={() => sortItems("low-high")}>Price: Low-High</Dropdown.Item>
                    </DropdownButton>
                </ButtonGroup>
            </div>
            <div className="loader">
                {loading ? <MoonLoader
                    color="#642d3c"
                    loading={loading}
                    size={100}
                /> : null }
            </div>
            <div className="items-frame">
                {items.map(item => (
                    <Card key={item.id} style={{ width: '18rem' }}>
                        <Card.Img 
                            variant="top" 
                            src={item.mainImage} 
                            className="card-image"
                        />
                        <Card.Body>
                            <Card.Title>{item.name}</Card.Title>
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
        </div>
    );
}

export default SortByLowToHigh;