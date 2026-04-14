import axios from "axios"

export const api = axios.create({
    // baseURL: "http://13.220.183.67"
    baseURL: "http://10.0.2.2:5269"
})