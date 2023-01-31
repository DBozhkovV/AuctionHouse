import React, { useState, useEffect } from 'react';
import axios from 'axios';
import '../../css/Profile.css';
import Button from 'react-bootstrap/Button';

const Profile = () => {
    const [profile, setProfile] = useState(null);

    useEffect(() => {
        const getProfile = async () => {
            axios.get(`${process.env.REACT_APP_API}/profile`, { withCredentials: true })
                .then(response => {
                    setProfile(response.data);
                })
                .catch(error => {
                    console.log(error);
                })
        }
        getProfile();
    }, []);
    
    if(!profile) {
        return null;
    }

    return (
        <div>
            <h3 className='profile-header'>Profile</h3>
            <hr />
            <div className="profile-container">
                <div className='columns-frame'>
                    <h4 className='columns-header'>Posts</h4>
                    <hr/>
                    {profile.items.map(item => {
                        return (
                            <div key={item.id} className='column-element-frame'>
                                <img src={`data:${item.result.mainImage.imageType};base64,${item.result.mainImage.image}`}
                                    width="100"
                                    height="100"
                                    className='column-element-image'
                                    alt=''
                                />
                                <div className='column-element-info'>
                                    <h5>{item.result.name}</h5>
                                    <p>{item.result.description}</p>
                                    <p>Bid now: {item.result.bid} $</p>
                                    <p>Date added: {new Date(Date.parse(item.result.dateAdded)).toLocaleDateString()}</p>
                                    <div className='column-element-buttons'>
                                        <Button variant="outline-primary">View</Button>
                                        <Button variant="outline-danger">Delete</Button>
                                    </div>
                                </div>
                            </div>
                        );
                    })}
                </div>
                <div className='profile-frame'>
                    <h4 className='columns-header'>Personal Information</h4>
                    <hr />
                </div>
                <div className='columns-frame'>
                    <h4 className='columns-header'>Orders</h4>
                    <hr />
                    {profile.orders.map(order => {
                        return (
                            <div key={order.id} className='column-element-frame'>
                                <img src={`data:${order.itemResponse.result.mainImage.imageType};base64,${order.itemResponse.result.mainImage.image}`}
                                    width="100"
                                    height="100"
                                    className='column-element-image'
                                    alt=''
                                />
                                <div className='column-element-info'>
                                    <h5>{order.itemResponse.result.name}</h5>
                                    <p>{order.itemResponse.result.description}</p>
                                    <p>Bought for: {order.itemResponse.result.boughtFor} $</p>
                                    <p>Date ordered: {new Date(Date.parse(order.dateOrdered)).toLocaleDateString()}</p>
                                    <div className='column-element-buttons'>
                                        <Button variant="outline-primary">View</Button>
                                        <Button variant="outline-danger">Delete</Button>
                                    </div>
                                </div>
                            </div>
                        );
                    })}
                </div>
                {/* <div className='profile-box'>
                    <div className='profile-view'></div>
                    <div className='profile-basic-info'>
                        <h6>Personal Information</h6>
                        <hr />
                        <div className='profile-names'>
                            <h6>Firstname: 
                                <a> {profile.firstName} </a>
                            </h6>
                            <h6>Lastname:
                                <a> {profile.lastName} </a>
                            </h6>
                        </div>
                        <hr />
                        <div className='profile-info'>
                            <h6>Username: {profile.username}</h6>
                            <h6>Email: {profile.email}</h6>
                            <h6>Phone: {profile.phoneNumber}</h6>
                        </div>
                        <hr />
                    </div>
                    <div className='profile-special-info'>
                        <h6>Special Information</h6>
                        <hr />
                        <h6>Balance: {profile.balance}</h6>
                        <br />
                        <hr />
                        <h6>Orders: </h6>
                    </div>
                </div> */}
            </div>
        </div>
    )
}

export default Profile;