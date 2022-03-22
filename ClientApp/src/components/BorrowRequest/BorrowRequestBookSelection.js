import axios from "axios";
import { useContext, useEffect, useState } from "react";
import { Button, Spinner, Table } from "react-bootstrap";
import { AuthContext, MessageContext } from "../../App";
import { PUSH_MESSAGE } from "../../services/MessageReducer";

function BorrowRequestBookSelection(props) {
  const initialQueryState = { isLoading: true, error: null };
  const [queryState, setQueryState] = useState(initialQueryState);

  const { messageDispatch } = useContext(MessageContext);
  const { authState } = useContext(AuthContext);

  function handleBookSelect(index) {
    var noOfSelectedBooks = 0;
    props.selectBookState.forEach((book) => {
      if (book.isSelected === true) {
        noOfSelectedBooks += 1;
      }
    });
    if (noOfSelectedBooks <= 5) {
      props.selectBookState[index].isSelected = true;
      props.setSelectBookState([...props.selectBookState]);
    } else {
      messageDispatch({
        type: PUSH_MESSAGE,
        payload: {
          type: "warning",
          header: "Book Selection",
          body: "Maximum selected book reached (5)",
        },
      });
    }
  }

  function handleBookDeselect(index) {
    props.selectBookState[index].isSelected = false;
    props.setSelectBookState([...props.selectBookState]);
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
            var selected = [];
            res.data.map((book) =>
              selected.push({ ...book, isSelected: false })
            );
            props.setSelectBookState(selected);
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
  }, []);

  if (queryState.isLoading !== true) {
    return (
      <div className="BorrowRequestBookSelection">
        <div className="selection">
          Books you selected:{" "}
          <ul>
            {props.selectBookState.map((book) => {
              if (book.isSelected === true)
                return <li key={book.bookId}>{book.bookName}</li>;
              return null;
            })}
          </ul>
        </div>
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
            {props.selectBookState.map((book, index) => {
              return (
                <tr key={book.bookName + index}>
                  <td>{index + 1}</td>
                  <td>{book.bookName}</td>
                  <td>{book.bookAuthor}</td>
                  <td>{book.category.categoryName}</td>
                  <td>
                    {book.isSelected === false ? (
                      <Button
                        type="button"
                        variant="primary"
                        onClick={() => handleBookSelect(index)}
                      >
                        Select
                      </Button>
                    ) : (
                      <Button
                        type="button"
                        variant="secondary"
                        onClick={() => handleBookDeselect(index)}
                      >
                        Deselect
                      </Button>
                    )}
                  </td>
                </tr>
              );
            })}
          </tbody>
        </Table>
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

export default BorrowRequestBookSelection;
