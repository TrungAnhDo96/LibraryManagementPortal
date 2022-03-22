import axios from "axios";
import { useContext, useEffect, useState } from "react";
import { Button, Form, Modal } from "react-bootstrap";
import { AuthContext, MessageContext } from "../../App";
import { PUSH_MESSAGE } from "../../services/MessageReducer";

function BookForm(props) {
  const initialFormData = {
    bookId: 0,
    bookName: "",
    bookAuthor: "",
    categoryId: 0,
  };
  const initialCategoryList = [
    { categoryId: 0, categoryName: "", categoryDescription: "" },
  ];
  const initialQueryState = { isLoading: false, error: null };

  const { authState } = useContext(AuthContext);
  const { messageDispatch } = useContext(MessageContext);
  const [formData, setFormData] = useState(initialFormData);
  const [categories, setCategories] = useState(initialCategoryList);
  const [queryState, setQueryState] = useState(initialQueryState);

  function getCategories() {
    axios({
      method: "get",
      url: "https://localhost:7182/api/categories",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + authState.tokenKey,
      },
    })
      .then((res) => {
        if (res.status === 200) {
          setCategories(res.data);
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
            body: "Failed to load category data.\n" + e,
          },
        });
      })
      .finally(() => setQueryState({ ...queryState, isLoading: false }));
  }

  function handleClose() {
    props.setShowForm(false);
  }

  function handleInput(e) {
    setFormData({ ...formData, [e.target.id]: e.target.value });
  }

  function addBook() {
    axios({
      method: "post",
      url: "https://localhost:7182/api/books",
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
              body: "Book is added",
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
            body: "Failed to add book data.\n" + e,
          },
        });
      })
      .finally(() => setQueryState({ ...queryState, isLoading: false }));
  }

  function updateBook() {
    console.log(formData);
    axios({
      method: "put",
      url: "https://localhost:7182/api/books/" + formData.bookId,
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
              body: "Book is updated",
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
            body: "Failed to update book data.\n" + e,
          },
        });
      })
      .finally(() => setQueryState({ ...queryState, isLoading: false }));
  }

  function handleSubmit() {
    if (props.mode === "update") {
      updateBook();
    } else if (props.mode === "add") {
      addBook();
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

  useEffect(() => {
    if (categories[0].categoryId === 0) {
      getCategories();
    }
    if (props.itemToUpdate !== null && props.itemToUpdate !== undefined) {
      setFormData(props.itemToUpdate);
    }
  }, [props.showForm]);

  return (
    <div className="BookForm">
      <Modal centered show={props.showForm} onHide={handleClose}>
        <Form onSubmit={handleSubmit}>
          <Modal.Header closeButton>
            <Modal.Title>Book form</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Form.Group controlId="bookName">
              <Form.Label>Book Name</Form.Label>
              <Form.Control
                type="text"
                value={formData.bookName || ""}
                onChange={handleInput}
              />
            </Form.Group>
            <Form.Group controlId="bookAuthor">
              <Form.Label>Author</Form.Label>
              <Form.Control
                type="text"
                value={formData.bookAuthor || ""}
                onChange={handleInput}
              />
            </Form.Group>
            <Form.Group controlId="categoryId">
              <Form.Label>Category</Form.Label>
              <Form.Select
                value={formData.categoryId || 1}
                onChange={handleInput}
              >
                {categories.map((category) => {
                  return (
                    <option
                      key={category.categoryId}
                      value={category.categoryId}
                    >
                      {category.categoryName}
                    </option>
                  );
                })}
              </Form.Select>
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

export default BookForm;
