export type Professional = {
    coren: string | null
    crm: string | null
    id: Number
    nome: string
    role: Number
    telefone: string
    user?: {}
    userId: Number
}

export type Plantao = {
    date: string
    duration: number
    id: string
    locale: string
    onDuty?: string
    responsable: string
    sector: string
    start: string
    value: number
}