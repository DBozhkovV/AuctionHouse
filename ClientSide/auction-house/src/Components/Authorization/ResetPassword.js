import React, { useState } from "react";
import "../../css/Authorization.css"
import Button from "react-bootstrap/Button";
import axios from "axios";
import { useParams } from "react-router-dom";
import Fail from "../../Components/Alerts/Fail";
import Success from "../../Components/Alerts/Success";

const ResetPassword = () => {
    const [password, setPassword] = useState(null);
    const [confirmPassword, setConfirmPassword] = useState(null);
    const [showFail, setShowFail] = useState(false);
    const [showSuccess, setShowSuccess] = useState(false);
    const [message, setMessage] = useState(null);
    const params = useParams();

    const handleSubmit = async (e) => {
        e.preventDefault();
        if (password === null || confirmPassword === null) {
            setShowFail(true);
            setMessage("Please fill all fields.");
        }else if (password !== confirmPassword) {
            setShowFail(true);
            setMessage("Password and confirmed password are not the same.");
        }else {
            await axios.put(`${process.env.REACT_APP_API}/reset-password`, {
                password: password,
                confirmPassword: confirmPassword,
                token: params.token
            })
            .then(() => {
                setShowSuccess(true);
            })
            .catch(error => {
                console.error(error);
                setShowFail(true);
                setMessage(error.response.data.message);
            })
        }

    }
    
    return (
        <div className="forgot-password-container">
            {showSuccess ? <Success /> : null}
            {showFail ? <Fail error={message} /> : null}
            <h3 className="forgot-password-header">
                Reset Password
            </h3>
            <div>
                <form>
                    <hr />
                    <div className="row my-3">
                        <div className="col-md-12">
                            <div className="form-outline w-100">
                                <label className="form-label">New Password</label>
                                <input 
                                    type="password" 
                                    className="form-control" 
                                    id="password" 
                                    placeholder="Enter Password" 
                                    onChange={(e) => { setPassword(e.target.value) }}
                                />
                            </div>
                        </div>
                    </div>
                    <div className="row my-3">
                        <div className="col-md-12">
                            <div className="form-outline w-100">
                                <label className="form-label">Confirm Password</label>
                                <input 
                                    type="password" 
                                    className="form-control" 
                                    id="confirmPassword" 
                                    placeholder="Confirm Password" 
                                    onChange={(e) => { setConfirmPassword(e.target.value) }}
                                />
                            </div>
                        </div>
                    </div>
                </form>
                <div className="d-flex justify-content-center">
                    <Button 
                        type="submit" 
                        variant="primary"
                        onClick={(e) => handleSubmit(e)}
                    >
                        Confirm
                    </Button>
                </div>
            </div>
        </div>
    );
};

export default ResetPassword;