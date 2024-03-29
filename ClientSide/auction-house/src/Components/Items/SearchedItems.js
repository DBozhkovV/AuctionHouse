import React, { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import Card from 'react-bootstrap/Card';
import ListGroup from 'react-bootstrap/ListGroup';
import { Button } from "react-bootstrap";
import "../../css/Item.css";
import "../../css/Loading.css";
import MoonLoader from "react-spinners/MoonLoader";

const SearchedItems = () => {
    const p = useParams();
    const navigate = useNavigate();
    const [items, setItems] = useState([]);
    const [loading, setLoading] = useState(true);
    
    const params = { 
        search: p.search
    };
    
    useEffect(() => {
        const getItems = async () => {
            axios.get(`${process.env.REACT_APP_API}/items/search`, { params })
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

    const routeChange = (id) =>{ 
        navigate(`/item/${id}`);
    }

    return (
        <div>
            <h3 className="items-header">
                Result of search: {p.search}
            </h3>
            <hr />
            <div className="loader">
                {loading ? <MoonLoader
                    color="#642d3c"
                    loading={loading}
                    size={100}
                /> : null }
            </div>
            <div className="items-frame">
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
        </div>
    )
}

export default SearchedItems;