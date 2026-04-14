import axios from "axios"

export const api = axios.create({
    baseURL: "http://3.227.243.120/api"
})