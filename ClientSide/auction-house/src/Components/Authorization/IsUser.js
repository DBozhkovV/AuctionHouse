import { useState } from "react";
import axios from "axios";

const IsUser = () => {
    const [isUser, setIsUser] = useState(false);

    axios.get(`${process.env.REACT_APP_API}/isUser`, { withCredentials: true })
        .then(() => {
            setIsUser(true);
        })
        .catch(() => {
            setIsUser(false);
        });
    return isUser;
}

export default IsUser;