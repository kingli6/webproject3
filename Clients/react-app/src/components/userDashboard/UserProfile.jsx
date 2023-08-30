// UserDashboard.jsx (simplified version)
import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';
import './UserProfile.css';

function UserProfile() {
  const { userDetails } = useAuth(); // Access userRole and userDetails from context
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [address, setAddress] = useState('');
  const [isEditing, setIsEditing] = useState(false);

  useEffect(() => {
    if (userDetails) {
      setFirstName(userDetails.firstName);
      setLastName(userDetails.lastName);
      setPhoneNumber(userDetails.phoneNumber);
      setAddress(userDetails.address);
    }
  }, [userDetails]);

  const handleEditField = (field) => {
    setIsEditing(field);
  };
  const handleSaveField = async () => {
    try {
      const token = JSON.parse(localStorage.getItem('token'));
      const url = `${process.env.REACT_APP_BASEURL}/auth/update-user/${userDetails.id}`;
      const response = await fetch(url, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `bearer ${token}`,
        },
        body: JSON.stringify({
          firstName,
          lastName,
          phoneNumber,
          address,
        }),
      });

      if (response.ok) {
        setIsEditing(false);
      }
    } catch (error) {
      console.error('Error updating user data:', error);
    }
  };
  const fetchUserData = async () => {
    try {
      const token = JSON.parse(localStorage.getItem('token'));
      const url = `${process.env.REACT_APP_BASEURL}/auth/getUserById/${userDetails.id}`;
      const response = await fetch(url, {
        method: 'GET',
        headers: {
          Authorization: `bearer ${token}`,
        },
      });

      if (response.ok) {
        const userData = await response.json();
        setFirstName(userData.firstName);
        setLastName(userData.lastName);
        setPhoneNumber(userData.phoneNumber);
        setAddress(userData.address);
      }
    } catch (error) {
      console.error('Error fetching user data:', error);
    }
  };

  useEffect(() => {
    if (userDetails) {
      fetchUserData();
    }
  }, [userDetails]);

  const handleDeleteAccount = () => {
    // Logic to handle account deletion and logout
  };

  return (
    <>
      <h2>User Profile</h2>
      <p>
        Name:{' '}
        {isEditing === 'firstName' ? (
          <input
            type="text"
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
          />
        ) : (
          firstName
        )}
        {isEditing !== 'firstName' && (
          <button onClick={() => handleEditField('firstName')}>Edit</button>
        )}
        {isEditing === 'firstName' && (
          <button onClick={handleSaveField}>Save</button>
        )}
      </p>
      <p>
        Last Name:{' '}
        {isEditing === 'lastName' ? (
          <input
            type="text"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
          />
        ) : (
          lastName
        )}
        {isEditing !== 'lastName' && (
          <button onClick={() => handleEditField('lastName')}>Edit</button>
        )}
        {isEditing === 'lastName' && (
          <button onClick={handleSaveField}>Save</button>
        )}
      </p>
      <p>
        Phone Number:{' '}
        {isEditing === 'phoneNumber' ? (
          <input
            type="text"
            value={phoneNumber}
            onChange={(e) => setPhoneNumber(e.target.value)}
          />
        ) : (
          phoneNumber
        )}
        {isEditing !== 'phoneNumber' && (
          <button onClick={() => handleEditField('phoneNumber')}>Edit</button>
        )}
        {isEditing === 'phoneNumber' && (
          <button onClick={handleSaveField}>Save</button>
        )}
      </p>
      <p>
        Address:{' '}
        {isEditing === 'address' ? (
          <input
            type="text"
            value={address}
            onChange={(e) => setAddress(e.target.value)}
          />
        ) : (
          address
        )}
        {isEditing !== 'address' && (
          <button onClick={() => handleEditField('address')}>Edit</button>
        )}
        {isEditing === 'address' && (
          <button onClick={handleSaveField}>Save</button>
        )}
      </p>
      <button onClick={handleDeleteAccount}>Delete Account</button>
      <Link to="/userDashboard">Back to Dashboard</Link>
    </>
  );
}

export default UserProfile;
