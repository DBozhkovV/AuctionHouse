import { useState } from "react";
import axios from "axios";

const IsAdmin = () => {
    const [isAdmin, setIsAdmin] = useState(false);

    axios.get(`https://localhost:7153/isAdmin`, { withCredentials: true })
        .then(() => {
            setIsAdmin(true);
        })
        .catch(() => {
            setIsAdmin(false);
        })
    return isAdmin;
}

export default IsAdmin;