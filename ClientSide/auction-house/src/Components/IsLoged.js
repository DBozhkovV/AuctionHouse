import { useState } from "react";
import axios from "axios";

const IsLoged = () => {
    const [isLogged, setIsLogged] = useState(false);

    axios.get(`${process.env.REACT_APP_API}/isLogged`, { withCredentials: true })
        .then(() => {
            setIsLogged(true);
        })
        .catch(() => {
            setIsLogged(false);
        })
    
    return isLogged;
}

export default IsLoged;