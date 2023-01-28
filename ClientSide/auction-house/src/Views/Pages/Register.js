import axios from "axios";
import React, { useState } from "react";
import BrandLogo from "../../Assets/images/BigBrandLogo.png";

// Da dobavq proverka dali password i confrimed password sa ednakvi
const RegistrationForm = () => {
    
    const [firstName, setFirstName] = useState(null);
    const [lastName, setLastName] = useState(null);
    const [email, setEmail] = useState(null);
    const [username, setUsername] = useState(null);
    const [phoneNumber, setPhoneNumber] = useState(null);
    const [password, setPassword] = useState(null);
    const [confirmPassword, setConfirmPassword] = useState(null);

    const handleSubmit = (e) => {
        e.preventDefault();
        axios.post(`${process.env.REACT_APP_API}/register`, {firstName, lastName, username, email, password, confirmPassword, phoneNumber,})
        .catch(error => {
            console.log(error)
        })
    }

    return (
        <div>
            <header className="register-header">
                {/* eslint-disable-next-line  */} {/*Remove the warning of the next line because in img tag we should have alt */}
                <img src={BrandLogo} />
                <br />
                Welcome to Auction house!
            </header>
            <div className="form-frame">
                <form>
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
                                    // aria-describedby="emailHelp" 
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
                        <button type="submit" className="btn btn-primary" onClick={handleSubmit}>Sign up</button>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default RegistrationForm;