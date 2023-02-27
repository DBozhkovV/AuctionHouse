import React from "react";
import Navbar from "./Navbar";
import "../../App.css"

const Layout = (props) => {
    return(
        <div>
            <Navbar/>
            {props.children}
        </div>
    );
};

export default Layout;