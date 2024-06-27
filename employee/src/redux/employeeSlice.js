import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';
import api from '../utils/api';

const initialState = {
  employees: [],
  status: 'idle',
  error: null,
};

export const fetchEmployees = createAsyncThunk(
    'employees/fetchEmployees',
    async (_, { rejectWithValue }) => {
      try {
        const response = await api.get('/employee');
        return response.data;
      } catch (error) {
        // Use rejectWithValue to return a custom error payload
        return rejectWithValue(error.response.data);
      }
    }
  );
    export const addEmployee = createAsyncThunk('employees/addEmployee', async (employee) => {
        const response = await api.post('/employee', employee);
        return response.data;
    });

    export const updateEmployee = createAsyncThunk('employees/updateEmployee', async (employee) => {
        const response = await api.put(`/employee/${employee.id}`, employee);
        return response.data;
    });

const employeeSlice = createSlice({
  name: 'employees',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchEmployees.pending, (state) => {
        state.status = 'loading';
      })
      .addCase(fetchEmployees.fulfilled, (state, action) => {
        state.status = 'succeeded';
        state.employees = action.payload;
      })
      .addCase(fetchEmployees.rejected, (state, action) => {
        state.status = 'failed';
        state.error = action.error.message;
      });
  },
});

export default employeeSlice.reducer;