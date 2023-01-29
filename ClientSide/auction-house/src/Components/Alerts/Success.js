import React, { useState } from 'react';
import Alert from 'react-bootstrap/Alert';

const Success = () => {
  const [show, setShow] = useState(true);

  if (show) {
    return (
      <Alert variant="success" onClose={() => setShow(false)} dismissible>
        <Alert.Heading>Successfully !</Alert.Heading>
      </Alert>
    );
  }
}

export default Success;