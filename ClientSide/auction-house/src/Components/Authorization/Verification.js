import React, { useState } from "react";
import "../../css/Authorization.css"
import Button from 'react-bootstrap/Button';
import axios from "axios";
import { useParams } from "react-router-dom";
import Fail from "../Alerts/Fail";
import Success from "../Alerts/Success";

const Verification = () => {
    const params = useParams();
    const [showFail, setShowFail] = useState(false);
    const [showSuccess, setShowSuccess] = useState(false);
    const [message, setMessage] = useState(null);

    const handleVerify = (e) => {
        e.preventDefault();
        axios.put(`${process.env.REACT_APP_API}/verify/${params.token}`, {})
        .then(() => {
            setShowSuccess(true);
        })
        .catch(error => {
            setShowFail(true);
            setMessage(error.response.data.message);
        })
    }

    return (
        <div>
            {showSuccess ? <Success /> : null}
            {showFail ? <Fail error={message} /> : null}
            <h3 className="verification-header">
                Verification page
            </h3>
            <hr/>
            <div className="verification-container">
                <Button 
                    className="verification-button" 
                    variant="primary"
                    onClick={(e) => handleVerify(e)}
                >
                    Verify!
                </Button>
            </div>
        </div>
    );
};

export default Verification;