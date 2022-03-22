import axios from "axios";
import { useContext, useEffect, useState } from "react";
import { Button, Spinner, Table } from "react-bootstrap";
import { AuthContext, MessageContext } from "../../App";
import { PUSH_MESSAGE } from "../../services/MessageReducer";
import ConfirmationBox from "../ConfirmationBox";
import BookForm from "./BookForm";

function BookListTable() {
  const initialBookList = [
    {
      bookId: 0,
      bookName: "",
      bookAuthor: "",
      category: { categoryName: "", categoryDescription: "" },
    },
  ];
  const initialConfirmBoxState = {
    showConfirm: false,
    body: "",
    title: "",
    item: {},
  };
  const initialQueryState = { isLoading: true, error: null };

  const [books, setBooks] = useState(initialBookList);
  const [showForm, setShowForm] = useState(false);
  const [itemToUpdate, setItemToUpdate] = useState({});
  const [confirmBoxState, setConfirmBoxState] = useState(
    initialConfirmBoxState
  );
  const [queryState, setQueryState] = useState(initialQueryState);

  const { messageDispatch } = useContext(MessageContext);
  const { authState } = useContext(AuthContext);

  function handleConfirmDialog(item) {
    setConfirmBoxState({
      ...confirmBoxState,
      title: "Delete book",
      body:
        "Delete is irreversible. Are you sure you want to delete book " +
        item.bookName,
      item: item,
      showConfirm: true,
    });
  }

  function handleUpdate(item) {
    setItemToUpdate(item);
    setShowForm(true);
  }

  function handleDelete() {
    axios({
      method: "delete",
      url: "https://localhost:7182/api/books/" + confirmBoxState.item.bookId,
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + authState.tokenKey,
      },
    })
      .then((res) => {
        if (res.status === 200) {
          messageDispatch({
            type: PUSH_MESSAGE,
            payload: {
              type: "info",
              header: "Info",
              body:
                "Delete confirmed. Delete " +
                confirmBoxState.item.bookName +
                "book",
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
            body: "Failed to delete book data.\n" + e,
          },
        });
      })
      .finally(() => setQueryState({ ...queryState, isLoading: false }));
  }

  useEffect(() => {
    if (queryState.isLoading === true) {
      axios({
        method: "get",
        url: "https://localhost:7182/api/books",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + authState.tokenKey,
        },
      })
        .then((res) => {
          if (res.status === 200) {
            setBooks(res.data);
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
              body: "Failed to load book data.\n" + e,
            },
          });
        })
        .finally(() => setQueryState({ ...queryState, isLoading: false }));
    }
  }, [showForm, confirmBoxState.showConfirm]);

  if (queryState.isLoading !== true) {
    return (
      <div className="BookListTable">
        <Table striped bordered hover>
          <thead>
            <tr>
              <th>#</th>
              <th>Name</th>
              <th>Author</th>
              <th>Category</th>
              <th>Action</th>
            </tr>
          </thead>
          <tbody>
            {books.map((book, index) => {
              return (
                <tr key={book.bookName + index}>
                  <td>{index + 1}</td>
                  <td>{book.bookName}</td>
                  <td>{book.bookAuthor}</td>
                  <td>{book.category.categoryName}</td>
                  <td>
                    <Button
                      type="button"
                      variant="secondary"
                      onClick={() => handleUpdate(book)}
                    >
                      Update
                    </Button>
                    <Button
                      type="button"
                      variant="danger"
                      onClick={() => handleConfirmDialog(book)}
                    >
                      Delete
                    </Button>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </Table>
        <BookForm
          showForm={showForm}
          setShowForm={setShowForm}
          itemToUpdate={itemToUpdate}
          mode="update"
        />
        <ConfirmationBox
          confirmBoxState={confirmBoxState}
          setConfirmBoxState={setConfirmBoxState}
          onConfirmAction={handleDelete}
        />
      </div>
    );
  } else {
    return (
      <Spinner animation="border" role="status">
        <span className="visually-hidden">Loading...</span>
      </Spinner>
    );
  }
}

export default BookListTable;
