import { useContext } from "react";
import { AuthContext } from "../../App";
import BorrowRequestNormalManagement from "./BorrowRequestNormalManagement";
import BorrowRequestSuperManagement from "./BorrowRequestSuperManagement";

function BorrowRequestManagement() {
  const { authState } = useContext(AuthContext);

  return (
    <div className="BorrowRequestManagement">
      {authState.accessLevel === "Super" ? (
        <BorrowRequestSuperManagement />
      ) : authState.accessLevel === "Normal" ? (
        <BorrowRequestNormalManagement />
      ) : (
        ""
      )}
    </div>
  );
}

export default BorrowRequestManagement;
