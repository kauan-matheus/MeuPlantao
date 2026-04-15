import axios from "axios"
import * as SecureStore from "expo-secure-store"

export const api = axios.create({
    // baseURL: "http://13.220.183.67"
    baseURL: "http://10.0.2.2:5269"
})

api.interceptors.request.use(async (config) => {
    const auth = await SecureStore.getItemAsync("auth")

    if (auth) {
        const { token } = JSON.parse(auth)
        config.headers.Authorization = `Bearer ${token}`
    }

    return config
})