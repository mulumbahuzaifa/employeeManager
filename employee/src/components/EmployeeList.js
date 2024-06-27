import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchEmployees } from '../redux/employeeSlice';
import { CSVLink } from 'react-csv';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import api from '../utils/api';
import { useNavigate } from 'react-router-dom';

export default function EmployeeList() {
    const dispatch = useDispatch();
    const navigate = useNavigate();
    const employees = useSelector((state) => state.employees.employees);

    useEffect(() => {
      dispatch(fetchEmployees());
    }, [dispatch]);

    const handleAdd = () => {
      navigate('/employee/add');
    };
  
    const handleEdit = (id) => {
      navigate(`/employee/edit/${id}`);
    };
  
    const handleDelete = async (id) => {
      try {
        await api.delete(`/employee/${id}`);
        dispatch(fetchEmployees());
        toast.success('Employee deleted successfully');
      } catch (error) {
        toast.error('Failed to delete employee');
      }
    };

    const headers = [
      { label: 'First Name', key: 'firstName' },
      { label: 'Last Name', key: 'lastName' },
      { label: 'Email', key: 'email' },
      { label: 'Department', key: 'department' },
      { label: 'Base Salary', key: 'salary.baseSalary' },
      { label: 'Bonus', key: 'salary.bonus' },
    ];

    const fetchEmployeeDataForExport = async () => {
      const response = await api.get('/employees/export');
      return response.data;
    };
  

    return (
      <div>
        <h1>Employee List</h1>
        <button onClick={handleAdd}>Add New Employee</button>
        <CSVLink data={employees} headers={headers} filename="employees.csv">
          Export to CSV
        </CSVLink>
        <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Email</th>
            <th>Department</th>
            <th>Base Salary</th>
            <th>Bonus</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {employees.map((employee) => (
            <tr key={employee.id}>
              <td>{employee.firstName} {employee.lastName}</td>
              <td>{employee.email}</td>
              <td>{employee.department}</td>
              <td>{employee.baseSalary}</td>
              <td>{employee.bonus}</td>
              <td>
              <button onClick={() => handleEdit(employee.id)}>Edit</button>
              <button onClick={() => handleDelete(employee.id)}>Delete</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      </div>
    );
}
