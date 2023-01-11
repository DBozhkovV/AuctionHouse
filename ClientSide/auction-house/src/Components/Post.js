import React, { useState } from "react";
import axios from "axios";

const Post = () => {
    const [name, setName] = useState(null);
    const [description, setDescription] = useState(null);
    const [buyPrice, setBuyPrice] = useState(null);
    const [startingPrice, setStartingPrice] = useState(null);
    const [endDate, setEndDate] = useState(null);
    const [mainImage, setMainImage] = useState(null);
    const [images, setImages] = useState(null);

    const handleSubmit = (e) => {
        e.preventDefault(); // da procheta kak raboti
        // const formData = new FormData();
        // formData.append("file", file);

        axios.post(`https://localhost:7153/items`, {
            name: name,
            description: description,
            buyPrice: buyPrice,
            startingPrice: startingPrice,
            endDate: endDate,
            MainImage: mainImage,
            Images: images
        }, { 
            withCredentials: true,
            headers: { "Content-Type": "multipart/form-data" }
        })
        .catch(error => {
            console.log(error)
        })
    }
    
    const handleChangeForMainImage = (event) => {
        setMainImage(event.target.files[0]);
    }

    const handleChangeForImages = (event) => {
        setImages(event.target.files[0]);
    }

    return (
        <div>
            <div className="form-frame">
                <form>
                    <div className="row my-3">
                        <div className="col-md-6">
                            <div className="form-outline">
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
                        <div className="col-md-6">
                            <div className="form-outline">
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
                        <div className="col-md-12">
                            <div className="form-outline w-100">
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
                    </div>
                    <div className="row my-3">
                        <div className="col-md-6">
                            <div className="form-outline w-10">
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
                    <div className="d-flex justify-content-center">
                        <button type="submit" className="btn btn-primary" onClick={handleSubmit}>Post</button>
                    </div>
                </form>
                <div>
                    Main Image
                    <form>
                        <input type="file" onChange={handleChangeForMainImage} />
                        <button type="submit">Upload</button>
                    </form>
                </div>
                <div>
                    Images
                    <form>
                        <input type="file" onChange={handleChangeForImages} />
                        <button type="submit">Upload</button>
                    </form>
                </div>
            </div>
        </div>
    );
}

export default Post;