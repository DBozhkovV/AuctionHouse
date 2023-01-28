import React, { useState }from "react";
import axios from "axios";
import Form from 'react-bootstrap/Form';
import "../css/Post.css";
import Alert from 'react-bootstrap/Alert';

const Post = () => {
    const [name, setName] = useState(null);
    const [description, setDescription] = useState(null);
    const [buyPrice, setBuyPrice] = useState(null);
    const [startingPrice, setStartingPrice] = useState(null);
    const [endDate, setEndDate] = useState(null);
    const [startDate, setStartDate] = useState(null);
    const [mainImage, setMainImage] = useState(null);
    const [images, setImages] = useState([]);
    const [numberOfImages, setNumberOfImages] = useState([]);
    const [firstImageAdded, setFirstImageAdded] = useState(false);
    const [showSuccess, setShowSuccess] = useState(false);
    const [showException, setShowException] = useState(false);

    const handleSubmit = (e) => {
        e.preventDefault(); // da procheta kak raboti
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
        formData.append("MainImage", mainImage);
        
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
            showSuccessAlert();
        })
        .catch(() => {
            setShowException(true);
            
        })
    }
    
    const handleChangeForMainImage = (event) => {
        setMainImage(event.target.files[0]);
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

    const showExceptionAlert = () => {
        if (showException) {
            setTimeout(() => {
                setShowException(false);
            }, 3000);
        }
    }

    const showSuccessAlert = () => {
        if (showSuccess) {
            setTimeout(() => {
                setShowSuccess(false);
            }, 3000);
        }
    }

    return (
        <div>
            {showSuccess && (
                <Alert variant="success">Post request successful!</Alert>
            )}
            {showException && (
                <Alert variant="danger">Post request exception!</Alert>
            )}
            <div className="form-frame">
                <h1 className="post-header">Post Item</h1>
                <form>
                <hr/>
                    <div className="row my-3">
                        <div className="col-md-12">
                            <div className="form-outline w-100">
                                <input 
                                    type="text" 
                                    className="form-control" 
                                    id="name" 
                                    placeholder="Name"
                                    onChange={(e) => { setName(e.target.value) }}
                                />
                                <label className="form-label">Name</label>
                            </div>
                        </div>
                        <div className="col-md-12">
                            <div className="form-outline w-100">
                                <input
                                    type="text"
                                    className="form-control"
                                    id="description" 
                                    placeholder="Description"
                                    onChange={(e) => { setDescription(e.target.value) }}  
                                />
                                <label className="form-label">Description</label>
                            </div>
                        </div>
                    </div>
                    <div className="row my-3">
                        <div className="col-md-6">
                            <div className="form-outline">
                                <input 
                                    type="number"
                                    min="0" 
                                    className="form-control" 
                                    id="buyPrice" 
                                    placeholder="Enter buy price" 
                                    onChange={(e) => { setBuyPrice(e.target.value) }}
                                />
                                <label className="form-label">buyPrice</label>
                            </div>
                        </div>
                        <div className="col-md-6">
                            <div className="form-outline">
                                <input 
                                    type="number"
                                    min="0" 
                                    className="form-control" 
                                    id="startingPrice" 
                                    placeholder="Enter starting price" 
                                    onChange={(e) => { setStartingPrice(e.target.value) }}
                                />
                                <label className="form-label">startingPrice</label>
                            </div>
                        </div>
                    </div>
                    <div className="row my-3">
                    <div className="col-md-6">
                            <div className="form-outline w-100">
                                <input 
                                    type="datetime-local"
                                    className="form-control" 
                                    id="endDate" 
                                    onChange={(e) => { setStartDate(e.target.value) }}
                                />
                                <label className="form-label">StartDate</label>
                            </div>
                        </div>
                        <div className="col-md-6">
                            <div className="form-outline w-100">
                                <input 
                                    type="datetime-local"
                                    className="form-control" 
                                    id="endDate" 
                                    placeholder="Enter end date" 
                                    onChange={(e) => { setEndDate(e.target.value) }}
                                />
                                <label className="form-label">EndDate</label>
                            </div>
                        </div>
                    </div>
                </form>
                <div>
                    <Form.Group controlId="formFile" className="mb-3">
                        <Form.Label>Upload main image</Form.Label>
                        <Form.Control type="file" onChange={handleChangeForMainImage} />
                    </Form.Group>
                </div>
                <div>
                    <Form.Group controlId="formFile" className="mb-3">
                        <Form.Label>Upload item images</Form.Label>
                        <Form.Control type="file" onChange={(e) => handleChangeForImages(e)} />
                        {numberOfImages.map((e, index) => (
                            <Form.Control key={index} type="file" onChange={(e) => handleChangeForImages(e)} />
                        ))}
                    </Form.Group>
                </div>
                <div className="d-flex justify-content-center">
                    <button type="submit" className="btn btn-primary" onClick={handleSubmit}>Post</button>
                </div>
            </div>
        </div>
    );
}

export default Post;