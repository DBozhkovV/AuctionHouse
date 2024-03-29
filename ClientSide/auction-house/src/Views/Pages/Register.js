import axios from "axios";
import React, { useState } from "react";
import Fail from "../../Components/Alerts/Fail";
import ConfirmAccount from "../../Components/Alerts/ConfirmAccount";

const RegistrationForm = () => {
    const [firstName, setFirstName] = useState(null);
    const [lastName, setLastName] = useState(null);
    const [email, setEmail] = useState(null);
    const [username, setUsername] = useState(null);
    const [phoneNumber, setPhoneNumber] = useState(null);
    const [password, setPassword] = useState(null);
    const [confirmPassword, setConfirmPassword] = useState(null);
    const [showFail, setShowFail] = useState(false);
    const [showConfirmAccount, setShowConfirmAccount] = useState(false);
    const [message, setMessage] = useState(null);

    const handleSubmit = (e) => {
        e.preventDefault();
        setShowConfirmAccount(false);
        setShowFail(false)
        if (firstName === null || lastName === null || email === null || username === null || phoneNumber === null || password === null || confirmPassword === null) {
            setShowFail(true);
            setMessage("Please fill all fields.");
        }else if (password !== confirmPassword) {
            setShowFail(true);
            setMessage("Password and confirmed password are not the same.");
        }else {
            axios.post(`${process.env.REACT_APP_API}/register`, 
            {
                firstName: firstName, 
                lastName: lastName, 
                username: username, 
                email: email, 
                password: password, 
                confirmPassword: confirmPassword, 
                phoneNumber: phoneNumber
            })
            .then(() => {
                setShowConfirmAccount(true);
            })
            .catch(error => {
                setShowFail(true);
                setMessage(error.response.data.message);
            })
        }
    }

    return (
        <div>
            {showConfirmAccount ? <ConfirmAccount /> : null}
            {showFail ? <Fail error={message} /> : null}
            <h3 className="register-header">
                <br />
                Welcome to Auction house!
            </h3>
            <div className="form-frame">
                <form id="register-form">
                    <hr />
                    <div className="row my-3">
                        <div className="col-md-6">
                            <div className="form-outline">
                                <input 
                                    type="text" 
                                    className="form-control" 
                                    id="firstName" 
                                    placeholder="First Name"
                                    onChange={(e) => { setFirstName(e.target.value) }}
                                />
                                <label className="form-label">First name</label>
                            </div>
                        </div>
                        <div className="col-md-6">
                            <div className="form-outline">
                                <input
                                    type="text"
                                    className="form-control"
                                    id="lastName" 
                                    placeholder="Last Name" 
                                    onChange={(e) => { setLastName(e.target.value) }}    
                                />
                                <label className="form-label">Last name</label>
                            </div>
                        </div>
                    </div>
                    <div className="row my-3">
                        <div className="col-md-12">
                            <div className="form-outline w-100">
                                <input 
                                    type="email" 
                                    className="form-control" 
                                    id="email" 
                                    placeholder="Enter email" 
                                    onChange={(e) => { setEmail(e.target.value) }}
                                />
                                <label className="form-label">Email</label>
                            </div>
                        </div>
                    </div>
                    <div className="row my-3">
                        <div className="col-md-6">
                            <div className="form-outline w-10">
                                <input 
                                    type="text" 
                                    className="form-control" 
                                    id="username" 
                                    placeholder="Username" 
                                    onChange={(e) => { setUsername(e.target.value) }}
                                />
                                <label className="form-label">Username</label>
                            </div>
                        </div>
                        <div className="col-md-6">
                            <div className="form-outline w-100">
                                <input 
                                    type="text" 
                                    className="form-control" 
                                    id="phoneNumber" 
                                    placeholder="Phone Number" 
                                    onChange={(e) => { setPhoneNumber(e.target.value) }}
                                />
                                <label className="form-label">Phone number</label>
                            </div>
                        </div>
                    </div>
                    <div className="row my-3">
                        <div className="col-md-12">
                            <div className="form-outline w-100">
                                <input 
                                    type="password" 
                                    className="form-control" 
                                    id="password" 
                                    placeholder="Password" 
                                    onChange={(e) => { setPassword(e.target.value) }}
                                />
                                <label className="form-label">Password</label>
                            </div>
                        </div>
                    </div>
                    <div className="row my-3">
                        <div className="col-md-12">
                            <div className="form-outline w-100">
                                <input 
                                    type="password" 
                                    className="form-control" 
                                    id="confirmPassword" 
                                    placeholder="Confirm Password" 
                                    onChange={(e) => { setConfirmPassword(e.target.value) }}
                                />
                                <label className="form-label">Confirm Password</label>
                            </div>
                        </div>
                    </div>
                    <div className="d-flex justify-content-center">
                        <button type="submit" className="btn btn-primary" onClick={(e) => handleSubmit(e)}>Sign up</button>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default RegistrationForm;