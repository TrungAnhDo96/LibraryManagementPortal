import { useState } from "react";
import { Button } from "react-bootstrap";
import CategoryForm from "./CategoryForm";
import CategoryListTable from "./CategoryListTable";

function CategoryManagement() {
  const [showForm, setShowForm] = useState(false);

  function handleShow() {
    setShowForm(true);
  }

  return (
    <div className="CategoryManagement">
      <p></p>
      <Button type="button" variant="primary" onClick={handleShow}>
        Add Category
      </Button>
      <p></p>
      <CategoryListTable />
      <CategoryForm showForm={showForm} setShowForm={setShowForm} mode="add" />
    </div>
  );
}

export default CategoryManagement;
