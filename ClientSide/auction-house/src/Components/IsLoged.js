import { useState } from "react";
import axios from "axios";

const IsLoged = () => {
    const [isLogged, setIsLogged] = useState(false);

    axios.get(`https://localhost:7153/isLogged`, { withCredentials: true })
        .then(() => {
            setIsLogged(true);
        })
        .catch((error) => {
            console.log(error);
            setIsLogged(false);
        })
    
    return isLogged;
}

export default IsLoged;