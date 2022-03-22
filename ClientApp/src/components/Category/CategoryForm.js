import axios from "axios";
import { useContext, useEffect, useState } from "react";
import { Button, Form, Modal } from "react-bootstrap";
import { AuthContext, MessageContext } from "../../App";
import { PUSH_MESSAGE } from "../../services/MessageReducer";

function CategoryForm(props) {
  const initialFormData = {
    categoryId: 0,
    categoryName: "",
    categoryDescription: "",
  };
  const initialQueryState = { isLoading: false, error: null };

  const [formData, setFormData] = useState(initialFormData);
  const [queryState, setQueryState] = useState(initialQueryState);
  const { messageDispatch } = useContext(MessageContext);
  const { authState } = useContext(AuthContext);

  function handleClose() {
    setFormData(initialFormData);
    props.setShowForm(false);
  }

  function handleSubmit() {
    if (props.mode === "update") {
      updateCategory();
    } else if (props.mode === "add") {
      addCategory();
    } else {
      messageDispatch({
        type: PUSH_MESSAGE,
        payload: {
          type: "danger",
          header: "Unknown command",
          body: "Unknown mode",
        },
      });
    }
    handleClose();
  }

  function handleInput(e) {
    setFormData({ ...formData, [e.target.id]: e.target.value });
  }

  function addCategory() {
    axios({
      method: "post",
      url: "https://localhost:7182/api/categories",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + authState.tokenKey,
      },
      data: formData,
    })
      .then((res) => {
        if (res.status === 200) {
          messageDispatch({
            type: PUSH_MESSAGE,
            payload: {
              type: "success",
              header: "Success",
              body: "Category is added",
            },
          });
        }
      })
      .catch((e) => {
        console.log(e);
        setQueryState({ ...queryState, error: e });
        messageDispatch({
          type: PUSH_MESSAGE,
          payload: {
            type: "danger",
            header: "Failed Request",
            body: "Failed to add category data.\n" + e,
          },
        });
      })
      .finally(() => setQueryState({ ...queryState, isLoading: false }));
  }

  function updateCategory() {
    axios({
      method: "put",
      url: "https://localhost:7182/api/categories/" + formData.categoryId,
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + authState.tokenKey,
      },
      data: formData,
    })
      .then((res) => {
        if (res.status === 200) {
          messageDispatch({
            type: PUSH_MESSAGE,
            payload: {
              type: "success",
              header: "Success",
              body: "Category is updated",
            },
          });
        }
      })
      .catch((e) => {
        console.log(e);
        setQueryState({ ...queryState, error: e });
        messageDispatch({
          type: PUSH_MESSAGE,
          payload: {
            type: "danger",
            header: "Failed Request",
            body: "Failed to update category data.\n" + e,
          },
        });
      })
      .finally(() => setQueryState({ ...queryState, isLoading: false }));
  }

  useEffect(() => {
    if (props.itemToUpdate !== null && props.itemToUpdate !== undefined) {
      setFormData(props.itemToUpdate);
    }
  }, [props.itemToUpdate]);

  return (
    <div className="CategoryForm">
      <Modal show={props.showForm} onHide={handleClose}>
        <Form onSubmit={handleSubmit}>
          <Modal.Header closeButton>
            <Modal.Title>Category form</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form.Group controlId="categoryName">
              <Form.Label>Category Name</Form.Label>
              <Form.Control
                type="text"
                value={formData.categoryName || ""}
                onChange={handleInput}
              />
            </Form.Group>
            <Form.Group controlId="categoryDescription">
              <Form.Label>Category Description</Form.Label>
              <Form.Control
                type="text"
                value={formData.categoryDescription || ""}
                onChange={handleInput}
              />
            </Form.Group>
          </Modal.Body>
          <Modal.Footer>
            <Button type="button" variant="secondary" onClick={handleClose}>
              Close
            </Button>
            <Button type="submit" variant="primary">
              Submit
            </Button>
          </Modal.Footer>
        </Form>
      </Modal>
    </div>
  );
}

export default CategoryForm;
