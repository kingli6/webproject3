import { useNavigate } from 'react-router-dom';

function UserItem(user, handleDeleteUser) {
  console.log(user);
  const navigate = useNavigate();

  function onEditClickHandler() {
    //navigate
  }
  const onDeleteClickHandler = () => {
    console.log(`We will delete course${user.user.email}`);
    handleDeleteUser(user.user.email);
  };
  return (
    <tr>
      <td>
        <span onClick={onEditClickHandler}>
          <i className="fa-solid fa-pencil edit"></i>
        </span>
      </td>
      <td>{user.user.firstName}</td>
      <td>{user.user.lastName}</td>
      <td>{user.user.email}</td>
      <td>{user.user.phoneNumber}</td>
      <td>{user.user.address}</td>
      <td>{user.user.role}</td>
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
