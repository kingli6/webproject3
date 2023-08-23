import { useNavigate } from 'react-router-dom';

function UserItem(user, handleDeleteUser) {
  const navigate = useNavigate();

  function onEditClickHandler() {
    //navigate
  }
  const onDeleteClickHandler = () => {
    console.log(`We will delete course${user.idCHANGETHIS}`);
    handleDeleteUser(user.email);
  };
  return (
    <tr>
      <td>
        <span onClick={onEditClickHandler}>
          <i className="fa-solid fa-pencil edit"></i>
        </span>
      </td>
      <td>{user.email}</td>
      <td>
        <span onClick={onDeleteClickHandler}>
          <i className="fa-solid fa-trash-can delete"></i>
        </span>
      </td>
    </tr>
  );
  //xx
}
export default UserItem;
