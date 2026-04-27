import { api } from "./api";

export const getProfessional = async (id: Number) => {
    try {
        const response = await api.get("/api/Profissionais/profissionais/user" + id)

        return response.data
    } catch (error: any) {
        console.log(error.response?.data)
        return null
    }
}

