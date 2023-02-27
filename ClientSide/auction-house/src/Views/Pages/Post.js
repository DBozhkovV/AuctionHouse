import React, { useState }from "react";
import axios from "axios";
import Form from 'react-bootstrap/Form';
import "../../css/Post.css";
import InputGroup from 'react-bootstrap/InputGroup';
import Button from 'react-bootstrap/Button';
import Success from "../../Components/Alerts/Success";
import Fail from "../../Components/Alerts/Fail";

const Post = () => {
    const [name, setName] = useState(null);
    const [description, setDescription] = useState(null);
    const [buyPrice, setBuyPrice] = useState(null);
    const [startingPrice, setStartingPrice] = useState(null);
    const [endDate, setEndDate] = useState(null);
    const [startDate, setStartDate] = useState(null);
    const [category, setCategory] = useState(null);
    const [mainImage, setMainImage] = useState([]);
    const [images, setImages] = useState([]);
    const [numberOfImages, setNumberOfImages] = useState([]);
    const [firstImageAdded, setFirstImageAdded] = useState(false);
    const [showSuccess, setShowSuccess] = useState(false);
    const [showFail, setShowFail] = useState(false);
    const [message, setMessage] = useState("");
    const [validated, setValidated] = useState(false);

    const handleSubmit = (e) => {
        e.preventDefault();
        setValidated(true);
        const formData = new FormData();
        const startDateUTC = new Date(startDate);
        const endDateUTC = new Date(endDate);
        const dateAdded = new Date();
        formData.append("Name", name);
        formData.append("Description", description);
        formData.append("BuyPrice", buyPrice);
        formData.append("StartingPrice", startingPrice);
        formData.append("DateAdded", dateAdded.toUTCString());
        formData.append("StartingBidDate", startDateUTC.toUTCString());
        formData.append("EndBidDate", endDateUTC.toUTCString());
        formData.append("Category", category);
        formData.append("MainImage", mainImage[0]);
        
        for (let i = 0; i < images.length; i++) {
            formData.append("Images", images[i]);
        }
        
        axios.post(`${process.env.REACT_APP_API}/items`, formData, 
        { 
            withCredentials: true,
            headers: { "Content-Type": "multipart/form-data" }
        })
        .then(() => {
            setShowSuccess(true);
            setShowFail(false);
            return;
        })
        .catch(error => {
            setShowFail(true);
            setShowSuccess(false);
            //setMessage(error.response.data.title);
        })  
    }
    
    const handleChangeForMainImage = (event) => {
        setMainImage([event.target.files[0]]);
    }

    const handleChangeForImages = (event) => {
        if(firstImageAdded === false) {
            setImages([event.target.files[0]]);
            setFirstImageAdded(true);
        } else {
            setImages([...images, event.target.files[0]]);
        }
        setNumberOfImages([...numberOfImages, event.target.files[0]]);
    }

    return (
        <div>
            {showSuccess ? <Success/> : null}
            {showFail ? <Fail error={message}/> : null}
            <div className="form-frame">
                <h1 className="post-header">Post Item</h1>
                <Form 
                    className="post-form" 
                    id="register-form" 
                    noValidate 
                    validated={validated} 
                    onSubmit={handleSubmit}
                >
                    <hr/>
                    <div className="row my-3">
                        <div className="col-md-12">
                        <Form.Group>
                            <Form.Label>Name</Form.Label>
                            <Form.Control
                                required
                                type="text"
                                placeholder="Name"
                                onChange={(e) => setName(e.target.value)}
                            />
                            <Form.Control.Feedback type="invalid">
                                Please enter a name.
                            </Form.Control.Feedback>
                        </Form.Group>
                        <br />
                        </div>
                        <div className="col-md-12">
                            <Form.Group>
                                <Form.Label>Description</Form.Label>
                                <InputGroup hasValidation>
                                    <InputGroup.Text id="inputGroupPrepend">Text:</InputGroup.Text>
                                    <Form.Control 
                                        as="textarea"
                                        type="text"
                                        placeholder="Description"
                                        aria-describedby="inputGroupPrepend"
                                        required
                                        onChange={(e) => setDescription(e.target.value)}
                                    />
                                    <Form.Control.Feedback type="invalid">
                                        Please enter a description.
                                    </Form.Control.Feedback>
                                </InputGroup>
                            </Form.Group>
                        </div>
                    </div>
                    <div className="row my-3">
                        <div className="col-md-6">
                            <Form.Group>
                                <Form.Label>Buy now price:</Form.Label>
                                <InputGroup hasValidation>
                                    <InputGroup.Text>$</InputGroup.Text>
                                    <Form.Control 
                                        aria-label="Amount"
                                        type="number"
                                        min="1"  
                                        required
                                        onChange={(e) => setBuyPrice(e.target.value)}
                                    />
                                    <InputGroup.Text>.00</InputGroup.Text>
                                    <Form.Control.Feedback type="invalid">
                                        Please enter a valid price.
                                    </Form.Control.Feedback>
                                </InputGroup>
                            </Form.Group>
                        </div>
                        <div className="col-md-6">
                            <Form.Group>
                                <Form.Label>Starting price:</Form.Label>
                                <InputGroup hasValidation>
                                    <InputGroup.Text>$</InputGroup.Text>
                                    <Form.Control 
                                        aria-label="Amount"
                                        type="number"
                                        min="1"  
                                        required
                                        onChange={(e) => setStartingPrice(e.target.value)}
                                    />
                                    <InputGroup.Text>.00</InputGroup.Text>
                                    <Form.Control.Feedback type="invalid">
                                        Please enter a valid price.
                                    </Form.Control.Feedback>
                                </InputGroup>
                            </Form.Group>
                        </div>
                    </div>
                    <div className="row my-3">
                        <div className="col-md-6">
                            <Form.Group>
                                <Form.Label>Starting date:</Form.Label>
                                <InputGroup hasValidation>
                                    <InputGroup.Text>Date:</InputGroup.Text>
                                    <Form.Control 
                                        aria-label="Amount"
                                        type="datetime-local"
                                        className="form-control"
                                        required   
                                        onChange={(e) => setStartDate(e.target.value)}
                                    />
                                    <Form.Control.Feedback type="invalid">
                                        Please enter a valid date.
                                    </Form.Control.Feedback>
                                </InputGroup>
                            </Form.Group>
                        </div>
                        <div className="col-md-6">
                            <Form.Group>
                                <Form.Label>End date:</Form.Label>
                                <InputGroup hasValidation>
                                    <InputGroup.Text>Date:</InputGroup.Text>
                                    <Form.Control 
                                        aria-label="Amount"
                                        type="datetime-local"
                                        className="form-control"
                                        required   
                                        onChange={(e) => setEndDate(e.target.value)}
                                    />
                                    <Form.Control.Feedback type="invalid">
                                        Please enter a valid date.
                                    </Form.Control.Feedback>
                                </InputGroup>
                            </Form.Group>
                        </div>
                    </div>
                    <Form.Group>
                        <Form.Label>Category</Form.Label>   
                        <Form.Select onChange={(e) => setCategory(e.target.value)}>
                            <option>Select category</option>
                            <option value={"Jewellery"}>Jewellery</option>
                            <option value={"Watch"}>Watch</option>
                            <option value={"Car"}>Car</option>
                            <option value={"Alcohol"}>Alcohol</option>
                            <option value={"Painting"}>Painting</option>
                            <option value={"Other"}>Other</option>
                        </Form.Select>
                        <Form.Control.Feedback type="invalid">
                            Please select a valid category.
                        </Form.Control.Feedback>     
                    </Form.Group>
                    <br />
                    <div>
                        <Form.Group className="mb-3">
                            <Form.Label>Upload main image</Form.Label>
                            <Form.Control type="file" required onChange={(e) => handleChangeForMainImage(e)} />
                        </Form.Group>
                    </div>
                    <div>
                        <Form.Group className="mb-3">
                            <Form.Label>Upload item images</Form.Label>
                            <Form.Control type="file" required onChange={(e) => handleChangeForImages(e)} />
                            {numberOfImages.map((e, index) => (
                                <Form.Control key={index} type="file" onChange={(e) => handleChangeForImages(e)} />
                            ))}
                        </Form.Group>
                    </div>
                </Form>
                <div className="d-flex justify-content-center">
                    <Button variant="primary" onClick={(e) => handleSubmit(e)}>Create post</Button>
                </div>
            </div>
        </div>
    );
}

export default Post;