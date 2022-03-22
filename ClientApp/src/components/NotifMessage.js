import React, { useState } from "react";
import { Toast } from "react-bootstrap";

function NotifMessage(props) {
  const [show, setShow] = useState(true);
  const delayTime = 2000;

  return (
    <Toast
      bg={props.type}
      show={show}
      delay={delayTime}
      autohide
      onClose={() => {
        setShow(false);
      }}
    >
      <Toast.Header closeButton={false}>{props.header}</Toast.Header>
      <Toast.Body>{props.body}</Toast.Body>
    </Toast>
  );
}

export default NotifMessage;
