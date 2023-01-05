import React, { useState, useEffect } from 'react';
import axios from 'axios';

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
        <div>
            <h1>Profile</h1>
            <div>
                <div>firstName: {profile.firstName}</div>
                <div>lastName: {profile.lastName}</div>
                <div>Username: {profile.username}</div>
                <div>Email: {profile.email}</div>
                <div>phone number: {profile.phoneNumber}</div>
                <div>Balance: {profile.balance}</div>
            </div>
        </div>
    )
}

export default Profile;