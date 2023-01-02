import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";

const SearchedItems = () => {
    const p = useParams();

    const [items, setItems] = useState(null);
    
    const params = { 
        search: p.search
    };
    
    useEffect(() => {
        const getItems = async () => {
            axios.get(`${process.env.API_URL}/items/search`, { params })
                .then(response => {
                    setItems(response.data);
                })
                .catch(error => {
                    console.log(error);
                })
        }
        getItems();
    }, []);

    return (
        <div>
           {items && items.map(item => (
                    <div key={item.id}>
                        {item.name}
                        {item.description}
                    </div>
                ))}
        </div>
    )
}

export default SearchedItems;