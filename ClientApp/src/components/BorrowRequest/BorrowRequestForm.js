import axios from "axios";
import { useContext, useState } from "react";
import { Button, Modal } from "react-bootstrap";
import { AuthContext, MessageContext } from "../../App";
import { PUSH_MESSAGE } from "../../services/MessageReducer";
import BorrowRequestBookSelection from "./BorrowRequestBookSelection";

function BorrowRequestForm(props) {
  const initialSelectionList = [
    {
      bookId: "",
      bookName: "",
      bookAuthor: "",
      category: { categoryName: "", categoryDescription: "" },
      isSelected: false,
    },
  ];
  const initialQueryState = { isLoading: true, error: null };

  const [selectBookState, setSelectBookState] = useState(initialSelectionList);
  const [queryState, setQueryState] = useState(initialQueryState);

  const { authState } = useContext(AuthContext);
  const { messageDispatch } = useContext(MessageContext);

  function handleClose() {
    setSelectBookState(initialSelectionList);
    props.setShowForm(false);
  }

  function handleSubmit() {
    var selected = [];
    selectBookState.forEach((book) => {
      if (book.isSelected === true) {
        selected.push({ bookId: book.bookId });
      }
    });
    var moment = require("moment");
    var request = {
      requestStatus: "Pending",
      requestDate: moment().format("DD/MM/YYYY"),
      requestedByUserId: authState.userId,
      requestDetails: selected,
    };
    axios({
      method: "post",
      url: "https://localhost:7182/api/requests",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + authState.tokenKey,
      },
      data: request,
    })
      .then((res) => {
        if (res.status === 200) {
          messageDispatch({
            type: PUSH_MESSAGE,
            payload: {
              type: "success",
              header: "Success",
              body: "Request sent",
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
            body: "Failed to load book data.\n" + e,
          },
        });
      })
      .finally(() => setQueryState({ ...queryState, isLoading: false }));
    handleClose();
  }

  return (
    <div className="BorrowRequestForm">
      <Modal size="lg" show={props.showForm} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Borrow Request form</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          Please select the books you wish to borrow
          <BorrowRequestBookSelection
            selectBookState={selectBookState}
            setSelectBookState={setSelectBookState}
          />
        </Modal.Body>
        <Modal.Footer>
          <Button type="button" variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button type="button" variant="primary" onClick={handleSubmit}>
            Request
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
}

export default BorrowRequestForm;
