import React, { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

const Login = () => {
    const [username, setUsername] = useState(null);
    const [password, setPassword] = useState(null);

    const navigate = useNavigate();

    const routeChange = () =>{ 
        navigate(`/`);
        window.location.reload();
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        axios.post(`https://localhost:7153/login`, {
            username: username,
            password: password
        }, {
            withCredentials: true
        })
        .then(response => {
            routeChange();
        })
        .catch(error => {
            console.log(error)
        })
    }

    return (
        // Create login form with bootstrap
        <div>
            <header>
                Login
            </header>
            <div className="form-frame">
                <form>
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
                    <div className="form-group">
                        <label htmlFor="password">Password</label>
                        <input
                            type="password" 
                            className="form-control" 
                            id="password" 
                            placeholder="Password" 
                            onChange={(e) => { setPassword(e.target.value) }}
                        />
                    </div>
                    <br />
                    <div className="d-flex justify-content-center">
                        <button type="submit" className="btn btn-primary" onClick={handleSubmit}>Submit</button>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default Login;