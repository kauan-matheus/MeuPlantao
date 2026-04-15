import { api } from "./api";
import * as SecureStore from "expo-secure-store"

export const login = async (email: string, password: string) => {
    try {
        const response = await api.post("/auth/login",{
            "email": email,
            "password": password
        })

        const data = response.data

        await SecureStore.setItemAsync(
            "auth",
            JSON.stringify({
                token: data.token,
                user: data.usuario,
                expiresIn: data.expiresIn
            })
        )

        // console.log(data)
        return {type: "success", message: ["Login efetuado com sucesso"]}
    } catch (error: any) {
        // console.log(error.response?.status)
        return {type: "error", message: error.response?.data}
    }
}

export const getAuth = async () => {
    const data = await SecureStore.getItemAsync("auth")

    if (!data) return null

    return JSON.parse(data)
}

export const logout = async () => {
    await SecureStore.deleteItemAsync('auth')
}

export const registerDoctor = async (email: string, password: string, name: string, crm: string, fone: string) => {
    try {
        const response = await api.post("/auth/register-medico", {
            "email": email,
            "password": password,
            "nome": name,
            "crm": crm,
            "telefone": fone
        })
        console.log(response)
    } catch (error) {
        console.error(error)
    }
}