import { api } from "./api";

export const getPlantoes = async () => {
    try {
        const response = await api.get("/api/Plantao/plantoes")
        return {type: "success", result: response.data}
    } catch (error: any) {
        console.log(error.response?.data)
        return {type: "error", result: "Não foi possível listar os plantões"}
    }
}

export const postPlantoes = async () => {
    try {
        const response = await api.post("/api/Plantao/plantoes")
        console.log(response)
    } catch (error) {
        console.error(error)
    }
}

export const putPlantoes = async (id: Number) => {
    try {
        const response = await api.put("/api/Plantao/plantoes/" + id)
        console.log(response)
    } catch (error) {
        console.error(error)
    }
}

export const deletePlantoes = async (id: Number) => {
    try {
        const response = await api.delete("/api/Plantao/plantoes/" + id)
        console.log(response)
    } catch (error) {
        console.error(error)
    }
}

export const requestPlantoes = async (id: Number) => {
    try {
        const response = await api.put("/api/Plantao/plantoes/" + id + "/solicitar")
    } catch (error) {
        console.error(error)
    }
}

export const toAcceptPlantoes = async (id: Number) => {
    try {
        const response = await api.put("/api/Plantao/plantoes/" + id + "/aceitar")
        console.log(response)
    } catch (error) {
        console.error(error)
    }
}

export const refusePlantoes = async (id: Number) => {
    try {
        const response = await api.put("/api/Plantao/plantoes/" + id + "/recusar")
        console.log(response)
    } catch (error) {
        console.error(error)
    }
}