import axios from "axios"
import * as SecureStore from "expo-secure-store"

export const api = axios.create({
    // baseURL: "https://meuplantao.eu1.netbird.services"
    baseURL: "http://192.168.0.38:5269"
    // baseURL: "http://10.0.2.2:5269"
})

api.interceptors.request.use(async (config) => {
    const auth = await SecureStore.getItemAsync("auth")

    if (auth) {
        const { token } = JSON.parse(auth)
        config.headers.Authorization = `Bearer ${token}`
    }

    return config
})