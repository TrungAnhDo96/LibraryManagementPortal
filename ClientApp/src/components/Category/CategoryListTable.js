import axios from "axios";
import { useContext, useEffect, useState } from "react";
import { Button, Spinner, Table } from "react-bootstrap";
import { AuthContext, MessageContext } from "../../App";
import { PUSH_MESSAGE } from "../../services/MessageReducer";
import ConfirmationBox from "../ConfirmationBox";
import CategoryForm from "./CategoryForm";

function CategoryListTable() {
  const initialCategoryList = [
    { categoryId: "", categoryName: "", categoryDescription: "" },
  ];
  const initialConfirmBoxState = {
    showConfirm: false,
    body: "",
    title: "",
    item: initialCategoryList,
  };
  const initialQueryState = { isLoading: true, error: null };

  const [categories, setCategories] = useState(initialCategoryList);
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
      title: "Delete category",
      body:
        "Delete is irreversible. Are you sure you want to delete category " +
        item.categoryName,
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
      url:
        "https://localhost:7182/api/categories/" +
        confirmBoxState.item.categoryId,
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
                confirmBoxState.item.categoryName +
                "category",
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
            body: "Failed to delete category data.\n" + e,
          },
        });
      })
      .finally(() => setQueryState({ ...queryState, isLoading: false }));
  }

  useEffect(() => {
    if (queryState.isLoading === true) {
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
  }, [showForm, confirmBoxState.showConfirm]);

  if (queryState.isLoading !== true) {
    return (
      <div className="CategoryListTable">
        <Table striped bordered hover>
          <thead>
            <tr>
              <th>#</th>
              <th>Name</th>
              <th>Description</th>
              <th>Action</th>
            </tr>
          </thead>
          <tbody>
            {categories.map((category, index) => {
              return (
                <tr key={category.categoryName + index}>
                  <td>{index + 1}</td>
                  <td>{category.categoryName}</td>
                  <td>{category.categoryDescription}</td>
                  <td>
                    <Button
                      type="button"
                      variant="secondary"
                      onClick={() => {
                        handleUpdate(category);
                      }}
                    >
                      Update
                    </Button>
                    <Button
                      type="button"
                      variant="danger"
                      onClick={() => handleConfirmDialog(category, index)}
                    >
                      Delete
                    </Button>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </Table>
        <CategoryForm
          showForm={showForm}
          setShowForm={setShowForm}
          mode="update"
          itemToUpdate={itemToUpdate}
        />
        <ConfirmationBox
          confirmBoxState={confirmBoxState}
          setConfirmBoxState={setConfirmBoxState}
          onConfirmAction={handleDelete}
        />
      </div>
    );
  } else
    return (
      <Spinner animation="border" role="status">
        <span className="visually-hidden">Loading...</span>
      </Spinner>
    );
}

export default CategoryListTable;
