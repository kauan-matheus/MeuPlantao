import { api } from "./api";

export const login = async () => {
    try {
        const response = await api.post("/auth/login",{
            "email": "teste@gmail.com",
            "password": "12345"
        })
        console.log(response)
    } catch (error) {
        console.log(error)
    }
}