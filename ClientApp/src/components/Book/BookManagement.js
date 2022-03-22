import { useState } from "react";
import { Button } from "react-bootstrap";
import BookForm from "./BookForm";
import BookListTable from "./BookListTable";

function BookManagement() {
  const [showForm, setShowForm] = useState(false);

  function handleShow() {
    setShowForm(true);
  }

  return (
    <div className="BookManagement">
      <p></p>
      <Button type="button" variant="primary" onClick={handleShow}>
        Add Book
      </Button>
      <p></p>
      <BookListTable />
      <BookForm showForm={showForm} setShowForm={setShowForm} mode="add" />
    </div>
  );
}

export default BookManagement;
