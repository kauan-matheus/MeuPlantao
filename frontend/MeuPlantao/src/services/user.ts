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
        return {type: "success", message: ["Login efetuado com sucesso"]}
    } catch (error: any) {
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

export const registerProfessional = async (email: string, password: string, name: string, document: string, telephone: string, option: "CRM" | "Coren") => {
    try {
        switch (option) {
            case "CRM":
                var response = await api.post("/auth/register-medico", {
                    "email": email,
                    "password": password,
                    "nome": name,
                    "crm": document,
                    "telefone": telephone
                })
                return {type: "success", message: response.data}
            case "Coren":
                var response = await api.post("/auth/register-enfermeiro", {
                    "email": email,
                    "password": password,
                    "nome": name,
                    "coren": document,
                    "telefone": telephone
                })
                return {type: "success", "message": response.data}
            default:
                return {type: "error", message: "Opção de profissional inexistente"}
        }
    } catch (error: any) {
        return {type: "error", message: error.response?.data}
    }
}