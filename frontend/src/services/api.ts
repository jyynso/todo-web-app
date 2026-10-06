import axios from "axios";

const api = axios.create({
  baseURL: 'https://localhost:7290/api/TodoItems',
  headers: {
    'Content-Type': 'application/json',
    'X-API-KEY': 'apikey1234',
  },
  timeout: 5000,
})

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('token')
  if (token) {
    config.headers.Authorization = `Bearer ${token}`
  }
  return config;
});

export default api;
