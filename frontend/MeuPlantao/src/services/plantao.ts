import { api } from "./api";

export const getPlantoes = async () => {
    try {
        const response = await api.get("/Plantao/plantoes")
        console.log(response)
    } catch (error) {
        console.error(error)
    }
}

export const postPlantoes = async () => {
    try {
        const response = await api.post("/Plantao/plantoes")
        console.log(response)
    } catch (error) {
        console.error(error)
    }
}

export const putPlantoes = async (id: Number) => {
    try {
        const response = await api.put("/Plantao/plantoes/" + id)
        console.log(response)
    } catch (error) {
        console.error(error)
    }
}

export const deletePlantoes = async (id: Number) => {
    try {
        const response = await api.delete("/Plantao/plantoes/" + id)
        console.log(response)
    } catch (error) {
        console.error(error)
    }
}

export const requestPlantoes = async (id: Number) => {
    try {
        const response = await api.put("/Plantao/plantoes/" + id + "/solicitar")
        console.log(response)
    } catch (error) {
        console.error(error)
    }
}

export const toAcceptPlantoes = async (id: Number) => {
    try {
        const response = await api.put("/Plantao/plantoes/" + id + "/aceitar")
        console.log(response)
    } catch (error) {
        console.error(error)
    }
}

export const refusePlantoes = async (id: Number) => {
    try {
        const response = await api.put("/Plantao/plantoes/" + id + "/recusar")
        console.log(response)
    } catch (error) {
        console.error(error)
    }
}