import { useNavigate } from 'react-router-dom';

function UserItem({ user, handleDeleteUser }) {
  console.log(user);
  const navigate = useNavigate();

  function onEditClickHandler() {
    navigate(`/editUser/${user.id}`);
  }
  const onDeleteClickHandler = () => {
    console.log(`We will delete course${user.email}`);
    handleDeleteUser(user.email);
  };
  return (
    <tr>
      <td>
        <span onClick={onEditClickHandler}>
          <i className="fa-solid fa-pencil edit"></i>
        </span>
      </td>
      <td>{user.firstName}</td>
      <td>{user.lastName}</td>
      <td>{user.email}</td>
      <td>{user.phoneNumber}</td>
      <td>{user.address}</td>
      <td>{user.role}</td>
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
