import { useState } from "react";
import axios from "axios";

const IsUser = () => {
    const [balance, setBalance] = useState(null);

    axios.get(`${process.env.REACT_APP_API}/isUser`, { withCredentials: true })
        .then(response => {
            setBalance(response.data);
        })
        .catch(() => {
            console.log("Not logged in");
        });
    
    if (balance !== null) {
        return balance;
    }
    
    return false;
}

export default IsUser;