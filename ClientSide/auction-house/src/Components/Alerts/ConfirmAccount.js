import React, { useState } from 'react';
import Alert from 'react-bootstrap/Alert';

const ConfirmAccount = () => {
  const [show, setShow] = useState(true);

  if (show) {
    return (
      <Alert variant="info" onClose={() => setShow(false)} dismissible>
        <Alert.Heading>Please verify your account on the email.</Alert.Heading>
      </Alert>
    );
  }
}

export default ConfirmAccount;