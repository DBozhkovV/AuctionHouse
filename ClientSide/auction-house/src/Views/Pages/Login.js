import React, { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import "../../css/Login.css";
import Fail from "../../Components/Alerts/Fail";
import Button from 'react-bootstrap/Button';

const Login = () => {
    const [username, setUsername] = useState(null);
    const [password, setPassword] = useState(null);
    const [showFail, setShowFail] = useState(false);
    const [message, setMessage] = useState(null);
    const navigate = useNavigate();

    const routeChange = () =>{ 
        navigate(`/`);
        window.location.reload();
    }
    
    const handleSubmit = async (e) => {
        e.preventDefault();
        if (username === null || password === null) {
            setShowFail(true);
            setMessage("Please fill all fields.");
        }else {
            await axios.post(`${process.env.REACT_APP_API}/login`, {
                username: username,
                password: password
            }, {
                withCredentials: true
            })
            .then(response => {
                if (response.data === "Admin") {
                    sessionStorage.setItem("isAdmin", true);
                } else if (response.data !== "Admin" && response.data !== null) {
                    sessionStorage.setItem("isUser", response.data);
                }
                routeChange();
            })
            .catch(error => {
                setShowFail(true);
                setMessage("Wrong username or password.");
                console.error(error);
            })
        }
    }

    return (
        // Create login form with bootstrap
        <div className="login-view">
            {showFail ? <Fail error={message} /> : null}
            <h3 className="login-header">
                Login
            </h3>
            <div>
                <form>
                    <hr />
                    <div className="form-group">
                        <label htmlFor="username">Username</label>
                        <input 
                            type="username" 
                            className="form-control" 
                            id="username" 
                            placeholder="Enter username" 
                            onChange={(e) => { setUsername(e.target.value) }}
                        />
                    </div>
                    <br/>
                    <div className="form-group">
                        <label htmlFor="password">Password</label>
                        <input
                            type="password" 
                            className="form-control" 
                            id="password" 
                            placeholder="Enter password" 
                            onChange={(e) => { setPassword(e.target.value) }}
                        />
                    </div>
                    <div className="forgot-button">
                        Forgot password? <a href="/forgotpassword">Click here</a>
                    </div>
                    <br />
                    <div className="d-flex justify-content-center">
                        <Button 
                            type="submit" 
                            variant="primary"
                            onClick={handleSubmit}
                        >
                            Submit
                        </Button>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default Login;