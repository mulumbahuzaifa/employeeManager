import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import api from '../utils/api';
import { fetchEmployees } from '../redux/employeeSlice';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

export default function EmployeeForm() {
  const { id } = useParams();
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const [employee, setEmployee] = useState({
    firstName: '',
    lastName: '',
    email: '',
    department: '',
    baseSalary: '',
    bonus: '',
  });

  useEffect(() => {
    if (id) {
      api.get(`/employee/${id}`).then((response) => {
        setEmployee(response.data);
      }).catch(error => {
        console.error('Error fetching employee:', error);
      });
    }
  }, [id]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setEmployee((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (id) {
      api.put(`/employee/${id}`, employee).then(() => {
        dispatch(fetchEmployees());
        navigate('/employees');
        toast.success('Employee updated successfully');
      }).catch(error => {
          console.error('Error updating employee:', error);
          toast.error('Failed to update employee');
      });
    } else {
      console.log('====================================');
      console.log(employee);
      console.log('====================================');
      api.post('/employee', employee).then(() => {
        dispatch(fetchEmployees());
        console.log(employee);
        navigate('/employees');
        toast.success('Employee added successfully');
      }).catch(error => {
        console.error('Error adding employee:', error);
        toast.error('Failed to add employee');
      });
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label>First Name</label>
        <input
          type="text"
          name="firstName"
          value={employee.firstName}
          onChange={handleChange}
          required
        />
      </div>
      <div>
        <label>Last Name</label>
        <input
          type="text"
          name="lastName"
          value={employee.lastName}
          onChange={handleChange}
          required
        />
      </div>
      <div>
        <label>Email</label>
        <input
          type="email"
          name="email"
          value={employee.email}
          onChange={handleChange}
          required
        />
      </div>
      <div>
        <label>Department</label>
        <input
          type="text"
          name="department"
          value={employee.department}
          onChange={handleChange}
          required
        />
      </div>
      <div>
        <label>Base Salary</label>
        <input
          type="number"
          name="baseSalary"
          value={employee.baseSalary}
          onChange={handleChange}
          required
        />
      </div>
      <div>
        <label>Bonus</label>
        <input
          type="number"
          name="bonus"
          value={employee.bonus}
          onChange={handleChange}
          required
        />
      </div>
      <button type="submit">{id ? 'Update' : 'Add'} Employee</button>
    </form>
  );
}
