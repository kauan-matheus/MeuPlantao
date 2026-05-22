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

export const getProfessionalPlantoes = async () => {
    try {
        const response = await api.get("/api/Profissionais/profissionais/plantoes")

        return {type: "success", result: response.data}
    } catch (error: any) {
        console.log(error.response?.data)
        return {type: "error", "message": "Não foi possível listar os plantões do profissional atual"}
    }
}

export const getProfessionalPlantoesSolicitados = async () => {
    try {
        const response = await api.get("/api/Profissionais/profissionais/plantoes/solicitados")

        return {type: "success", result: response.data}
    } catch (error: any) {
        console.log(error.response?.data)
        return {type: "error", "message": "Não foi possível listar os plantões do profissional atual"}
    }
}

