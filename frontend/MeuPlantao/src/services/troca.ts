import { api } from "./api";

export const getTrocas = async () => {
    try {
        const response = await api.get("/api/Trocas/trocas")
        return {type: "success", result: response.data}
    } catch (error: any) {
        console.log(error.response?.data)
        return {type: "error", result: "Não foi possível listar os plantões"}
    }
}

export const postTrocas = async () => {
    try {
        const response = await api.post("/api/Trocas/trocas")
        console.log(response)
    } catch (error) {
        console.error(error)
    }
}

export const putTrocas = async (id: Number) => {
    try {
        const response = await api.put("/api/Trocas/trocas/" + id)
        console.log(response)
    } catch (error) {
        console.error(error)
    }
}

export const deleteTrocas = async (id: Number) => {
    try {
        const response = await api.delete("/api/Trocas/trocas/" + id)
        console.log(response)
    } catch (error) {
        console.error(error)
    }
}