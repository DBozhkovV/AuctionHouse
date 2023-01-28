import React from 'react';
import Alert from 'react-bootstrap/Alert';
import Button from 'react-bootstrap/Button';

const Successful = (props) => {
  return (
    <Alert variant="success">
        <Alert.Heading>How's it going?!</Alert.Heading>
        <p>
        Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget
        lacinia odio sem nec elit. Cras mattis consectetur purus sit amet
        fermentum.
        </p>
        <hr />
        <div className="d-flex justify-content-end">
        <Button variant="outline-success" onClick={props.onHide}>
            Close me y'all!
        </Button>
        </div>
    </Alert>
  );
}

export default Successful;