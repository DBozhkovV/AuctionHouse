import React, { useState, useEffect } from 'react';
import axios from 'axios';
import '../../css/Profile.css';
import Button from 'react-bootstrap/Button';
import DeleteOrder from '../../Components/Orders/DeleteOrder';
import { useNavigate } from 'react-router-dom';
import profileLogo from '../../Assets/images/user-profile-icon.svg';
import "../../css/Loading.css";
import MoonLoader from "react-spinners/MoonLoader";

const Profile = () => {
    const [profile, setProfile] = useState([]);
    const [DeleteOrderShow, setDeleteOrderShow] = useState(false);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        const getProfile = async () => {
            axios.get(`${process.env.REACT_APP_API}/profile`, { withCredentials: true })
                .then(response => {
                    setProfile(response.data);
                    setLoading(false);
                })
                .catch(error => {
                    setLoading(false);
                    console.log(error);
                })
        }
        getProfile();
    }, []);

    const routeOrderChange = (id) =>{ 
        navigate(`/order/${id}`);
    }

    const routePostChange = (id) =>{
        navigate(`/item/${id}`);
    }

    const getStatus = (item) => {
        if(item.isAccepted === false) {
            return 'Not accepted';
        }else {
            return 'Accepted';
        }
    }

    return (
        <div>
            <h3 className='profile-header'>Profile</h3>
            <hr />
            <div className="loader">
                {loading ? <MoonLoader
                    color="#642d3c"
                    loading={loading}
                    size={100}
                /> : ( 
                <div className="profile-container">
                    <div className='columns-frame'>
                        <h4 className='columns-header'>Posts</h4>
                        <hr />
                        {profile.items.map(item => {
                            return (
                                <div key={item.id} className='column-element-frame'>
                                    <img 
                                        src={item.mainImage}
                                        width="100"
                                        height="100"
                                        className='column-element-image'
                                        alt=''
                                    />
                                    <div className='column-element-info'>
                                        <h5>Name: {item.name}</h5>
                                        <p>Description: {item.description}</p>
                                        <p>Bid now: {item.bid} $</p>
                                        <p>Status: {getStatus(item)}</p>
                                        <div className='column-element-buttons'>
                                            <Button variant="outline-primary" onClick={() => routePostChange(item.id)}>View</Button>
                                        </div>
                                    </div>
                                </div>
                            );
                        })}
                    </div>
                    <div className='profile-frame'>
                        <h4 className='columns-header'>Personal Information</h4>
                        <hr />
                        <div className='profile-column'>
                            <img 
                                src={profileLogo} 
                                width="70"
                                height="70"
                                className='profile-logo'
                            />
                            <br/>
                            <br/>
                            <div className='profile-info'>
                                <h5>Username: {profile.username}</h5>
                                <hr/>
                                <h5>Email: {profile.email}</h5>
                                <hr/>
                                <h5>First name: {profile.firstName}</h5>
                                <hr/>
                                <h5>Last name: {profile.lastName}</h5>
                                <hr/>
                                <h5>Phone number: {profile.phoneNumber}</h5>
                                <hr/>
                                <h5>Balance: {profile.balance} $</h5>
                                <hr/>
                            </div>
                        </div>
                    </div>
                    <div className='columns-frame'>
                        <h4 className='columns-header'>Orders</h4>
                        <hr />
                        {profile.orders.map(order => {
                            return (
                                <div key={order.id} className='column-element-frame'>
                                    <img 
                                        src={order.itemResponse.mainImage}
                                        width="100"
                                        height="100"
                                        className='column-element-image'
                                        alt=''
                                    />
                                    <div className='column-element-info'>
                                        <h5>{order.itemResponse.name}</h5>
                                        <p>{order.itemResponse.description}</p>
                                        <p>Bought for: {order.itemResponse.boughtFor} $</p>
                                        <p>Date ordered: {new Date(Date.parse(order.dateOrdered)).toLocaleDateString()}</p>
                                        <div className='column-element-buttons'>
                                            <Button variant="outline-primary" onClick={() => routeOrderChange(order.id)}>View</Button>
                                            <Button variant="outline-danger" onClick={() => setDeleteOrderShow(true)}>Delete</Button>
                                            <DeleteOrder show={DeleteOrderShow} onHide={() => setDeleteOrderShow(false)} itemID={order.id} />
                                        </div>
                                    </div>
                                </div>
                            );
                        })}
                    </div>
                </div>
                )}
            </div>
        </div>
    )
}

export default Profile;