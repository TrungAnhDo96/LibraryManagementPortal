import { useState } from "react";
import { Button } from "react-bootstrap";
import BorrowRequestForm from "./BorrowRequestForm";
import BorrowRequestNormalListTable from "./BorrowRequestNormalListTable";

function BorrowRequestNormalManagement() {
  const [showForm, setShowForm] = useState(false);

  function handleShow() {
    setShowForm(true);
  }

  return (
    <div className="BorrowRequestNormalManagement">
      <p></p>
      <Button type="button" variant="primary" onClick={handleShow}>
        Make a request
      </Button>
      <BorrowRequestNormalListTable />
      <BorrowRequestForm showForm={showForm} setShowForm={setShowForm} />
    </div>
  );
}

export default BorrowRequestNormalManagement;
