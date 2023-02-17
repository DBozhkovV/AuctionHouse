import React, { useState, useEffect } from "react";
import axios from "axios";
import Card from 'react-bootstrap/Card';
import ListGroup from 'react-bootstrap/ListGroup';
import { Button } from "react-bootstrap";
import { useNavigate, useParams } from "react-router-dom";
import Fail from "../Alerts/Fail";
import MoonLoader from "react-spinners/MoonLoader";
import "../../css/Loading.css";
import DropdownButton from 'react-bootstrap/DropdownButton';
import Dropdown from 'react-bootstrap/Dropdown';
import ButtonGroup from 'react-bootstrap/ButtonGroup';
import Pagination from 'react-bootstrap/Pagination';

const AllAvailableItems = () => {
    const params = useParams();
    const [items, setItems] = useState([]);
    const [showFail, setShowFail] = useState(false);
    const [message, setMessage] = useState("");
    const [loading, setLoading] = useState(true);
    const [pages, setPages] = useState(null);
    const currentPage = parseInt(params.page);
    const navigate = useNavigate();

    useEffect(() => {
        const getPages = async () => {
            await axios.get(`${process.env.REACT_APP_API}/items/availableItemsPages`)
                .then(response => {
                    setPages(response.data);
                })
                .catch(error => {
                    console.error(error);
                })
        }
        getPages();
    }, []);

    useEffect(() => {
        const getItems = async () => {
            await axios.get(`${process.env.REACT_APP_API}/items/available?page=${currentPage}`)
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
    }, [currentPage]);

    const routeChange = (id) => { 
        navigate(`/item/${id}`);
    }

    const handleNextPage = () => { 
        navigate(`/items/${currentPage + 1}`);
    }

    const handlePreviousPage = () => { 
        navigate(`/items/${currentPage - 1}`);
    }

    const handlePageChange = (page) => {
        navigate(`/items/${page}`);
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
    
    const pageNumbers = [];
    for (let i = 1; i <= pages; i++) {
        pageNumbers.push(i);
    }

    return (
        <div>
            {showFail ? <Fail error={message}/> : null}
            <h3 className="items-header">
                Items
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
            {loading ? null : 
                <div className="page-navigator">
                    <Pagination>
                        <Pagination.Prev
                            disabled={currentPage === 1}
                            onClick={handlePreviousPage}
                        />
                        {pageNumbers.map((pageNumber) => (
                            <Pagination.Item
                                key={pageNumber}
                                active={pageNumber === currentPage}
                                onClick={() => handlePageChange(pageNumber)}
                            >
                            {pageNumber}
                            </Pagination.Item>
                        ))}
                        <Pagination.Next
                            disabled={currentPage === pages}
                            onClick={handleNextPage}
                        />
                    </Pagination>
                </div>}
        </div>
    );
}

export default AllAvailableItems;