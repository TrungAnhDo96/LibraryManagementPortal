import axios from "axios";
import { useContext, useEffect, useState } from "react";
import { Table } from "react-bootstrap";
import { AuthContext, MessageContext } from "../../App";
import { PUSH_MESSAGE } from "../../services/MessageReducer";
import BorrowRequestForm from "./BorrowRequestForm";

function BorrowRequestNormalListTable(props) {
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

  useEffect(() => {
    if (queryState.isLoading === true) {
      axios({
        method: "get",
        url: "https://localhost:7182/api/requests/user/" + authState.userId,
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
    <div className="BorrowRequestNormalListTable">
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>#</th>
            <th>Requested Books</th>
            <th>Request Date</th>
            <th>Processed By</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          {borrowRequests.map((request, index) => {
            return (
              <tr key={"request" + index}>
                <td>{index + 1}</td>
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

export default BorrowRequestNormalListTable;
