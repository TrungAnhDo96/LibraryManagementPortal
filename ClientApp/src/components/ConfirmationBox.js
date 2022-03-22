import { Button, Modal } from "react-bootstrap";

function ConfirmationBox(props) {
  function handleClose() {
    props.setConfirmBoxState({
      showConfirm: false,
      title: "",
      body: "",
    });
  }

  function handleConfirm() {
    props.onConfirmAction();
    handleClose();
  }

  return (
    <div className="ConfirmationBox">
      <Modal show={props.confirmBoxState.showConfirm} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>{props.confirmBoxState.title}</Modal.Title>
        </Modal.Header>
        <Modal.Body>{props.confirmBoxState.body}</Modal.Body>
        <Modal.Footer>
          <Button type="button" variant="secondary" onClick={handleClose}>
            No
          </Button>
          <Button type="button" variant="danger" onClick={handleConfirm}>
            Delete
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
}

export default ConfirmationBox;
