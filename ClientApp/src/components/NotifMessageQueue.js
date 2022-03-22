import React from "react";
import { ToastContainer } from "react-bootstrap";
import { MessageContext } from "../App";
import NotifMessage from "./NotifMessage";

function NotifMessageQueue() {
  const { messageState } = React.useContext(MessageContext);

  return (
    <ToastContainer position="top-end" className="p-3">
      {messageState.messages.map((message, index) => {
        return (
          <NotifMessage
            key={"Toast " + index + 1}
            index={index}
            type={message.type}
            header={message.header}
            body={message.body}
          />
        );
      })}
    </ToastContainer>
  );
}

export default NotifMessageQueue;
