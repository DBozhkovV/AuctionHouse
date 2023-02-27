import React, { useState } from "react";
import "../../css/Authorization.css"
import Button from "react-bootstrap/Button";
import axios from "axios";

const ForgotPassword = () => {
    const [email, setEmail] = useState(null);

    const handleSubmit = async (e) => {
        e.preventDefault();
            await axios.put(`${process.env.REACT_APP_API}/forgot-password?email=${email}`)
            .then(response => {
                console.log(response);
            })
            .catch(error => {
                console.error(error);
            })
    }

    return (
        <div className="forgot-password-container">
            <h3 className="forgot-password-header">
                Forgot Password
            </h3>
            <div>
                <form>
                    <hr />
                    <div className="form-group">
                        <label htmlFor="email">Email</label>
                        <input 
                            type="email" 
                            className="form-control" 
                            id="email" 
                            placeholder="Enter email" 
                            onChange={(e) => { setEmail(e.target.value) }}
                        />
                    </div>
                    <br />
                    <div className="d-flex justify-content-center">
                        <Button 
                            type="submit" 
                            variant="primary"
                            onClick={(e) => handleSubmit(e)}
                        >
                            Confirm
                        </Button>
                    </div>
                </form>
            </div>
        </div>
    );
};

export default ForgotPassword;