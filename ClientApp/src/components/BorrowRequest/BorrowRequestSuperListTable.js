import axios from "axios";
import { useContext, useEffect, useState } from "react";
import { Button, Table } from "react-bootstrap";
import { AuthContext, MessageContext } from "../../App";
import { PUSH_MESSAGE } from "../../services/MessageReducer";
import BorrowRequestForm from "./BorrowRequestForm";

function BorrowRequestSuperListTable(props) {
  const initialBorrowRequestList = [
    {
      requestId: "",
      requestDetails: [{ book: { bookName: "", bookAuthor: "" } }],
      requestedUser: {
        userName: "",
        firstName: "",
        lastName: "",
      },
      requestDate: "",
      requestStatus: "",
      processedUser: { userName: "", firstName: "", lastName: "" },
    },
  ];
  const initialQueryState = { isLoading: true, error: null };

  const [borrowRequests, setBorrowRequests] = useState(
    initialBorrowRequestList
  );
  const [showForm, setShowForm] = useState(false);
  const [queryState, setQueryState] = useState(initialQueryState);
  const { messageDispatch } = useContext(MessageContext);
  const { authState } = useContext(AuthContext);

  function changeRequestStatus(request, status) {
    axios({
      method: "put",
      url:
        "https://localhost:7182/api/requests/" +
        status +
        "/" +
        request.requestId,
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + authState.tokenKey,
      },
      data: authState.userId,
    })
      .then((res) => {
        if (res.status === 200) {
          setQueryState({ ...queryState, isLoading: true });
          messageDispatch({
            type: PUSH_MESSAGE,
            payload: {
              type: "info",
              header: "Info",
              body:
                "State of request made by " +
                request.requestedUser.userName +
                " is changed",
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
            body: "Failed to " + status + " request.\n" + e,
          },
        });
      })
      .finally(() => setQueryState({ ...queryState, isLoading: false }));
  }

  function handleNotify(item, status) {
    changeRequestStatus(item, status);
  }

  useEffect(() => {
    if (queryState.isLoading === true) {
      axios({
        method: "get",
        url: "https://localhost:7182/api/requests",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + authState.tokenKey,
        },
      })
        .then((res) => {
          if (res.status === 200) {
            setBorrowRequests(res.data);
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
              body: "Failed to load request data.\n Error: " + e,
            },
          });
        })
        .finally(() => setQueryState({ ...queryState, isLoading: false }));
    }
  }, [queryState]);

  return (
    <div className="BorrowRequestSuperListTable">
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>#</th>
            <th>Requester</th>
            <th>Requested Books</th>
            <th>Request Date</th>
            <th>Processed By</th>
            <th>Status</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {borrowRequests.map((request, index) => {
            return (
              <tr key={"request" + index}>
                <td>{index + 1}</td>
                <td>{request.requestedUser.userName}</td>
                <td>
                  {request.requestDetails.map((detail, i) => {
                    return (
                      <div key={detail.book.bookName + i}>
                        {detail.book.bookName}
                      </div>
                    );
                  })}
                </td>
                <td>{request.requestDate}</td>
                <td>
                  {!!request.processedUser
                    ? request.processedUser.userName
                    : ""}
                </td>
                <td>{request.requestStatus}</td>
                <td>
                  {request.requestStatus === "Pending" ? (
                    <div className="borrowRequestAction">
                      <Button
                        type="button"
                        variant="primary"
                        onClick={() => handleNotify(request, "approve")}
                      >
                        Approve
                      </Button>
                      <Button
                        type="button"
                        variant="danger"
                        onClick={() => handleNotify(request, "reject")}
                      >
                        Reject
                      </Button>
                    </div>
                  ) : (
                    ""
                  )}
                </td>
              </tr>
            );
          })}
        </tbody>
      </Table>
      <BorrowRequestForm
        showForm={showForm}
        setShowForm={setShowForm}
        item={{}}
      />
    </div>
  );
}

export default BorrowRequestSuperListTable;
