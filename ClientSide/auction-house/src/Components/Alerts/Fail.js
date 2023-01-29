import React, { useState } from 'react';
import Alert from 'react-bootstrap/Alert';

const Fail = ({ error }) => {
  const [show, setShow] = useState(true);

  if (show) {
    return (
      <Alert className="alert-frame" variant="danger" onClose={() => setShow(false)} dismissible>
        <Alert.Heading>You got an error!</Alert.Heading>
        <p>
          {error}
        </p>
      </Alert>
    );
  }
}

export default Fail;