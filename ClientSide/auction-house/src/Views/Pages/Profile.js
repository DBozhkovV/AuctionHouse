import React, { useState, useEffect } from 'react';
import axios from 'axios';
import '../../css/Profile.css';

const Profile = () => {
    const [profile, setProfile] = useState(null);

    useEffect(() => {
        const getProfile = async () => {
            axios.get(`https://localhost:7153/profile`, { withCredentials: true })
                .then(response => {
                    setProfile(response.data);
                })
                .catch(error => {
                    console.log(error);
                })
        }
        getProfile();
    }, []);
    
    if(!profile) return null;

    return (
        <div className="profile-container">
            <h1>Profile</h1>
            <div className='profile-box'>
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
            </div>
        </div>
    )
}

export default Profile;