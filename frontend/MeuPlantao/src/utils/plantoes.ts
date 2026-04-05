export type Plantao = {
    id: string
    date: string
    start: string
    duration: string
    locale: string
    sector: string
    value: string
    responsable: string
    oncall?: string
}

export const plantoes: Plantao[] = [
    { id: "1", date: "05/04/2026", start: "7:00", duration: "12h", locale: "Unimar" , sector: "Sala de emergências", value: "1500,00", responsable: "Fulano", oncall: "Medico" },
    { id: "2", date: "05/04/2026", start: "7:00", duration: "12h", locale: "Unimar" , sector: "Sala de emergências", value: "1500,00", responsable: "Fulano", oncall: "Medico" },
    { id: "3", date: "05/04/2026", start: "7:00", duration: "12h", locale: "Unimar" , sector: "Sala de emergências", value: "1500,00", responsable: "Fulano", oncall: "Medico" },
    { id: "4", date: "06/04/2026", start: "7:00", duration: "6h", locale: "Unimar", sector: "Pronto socorro", value: "750,00", responsable: "Fulano", oncall: "Medico" },
    { id: "5", date: "06/04/2026", start: "7:00", duration: "6h", locale: "Unimar", sector: "Pronto socorro", value: "750,00", responsable: "Fulano", oncall: "Medico" },
    { id: "6", date: "06/04/2026", start: "7:00", duration: "6h", locale: "Unimar", sector: "Pronto socorro", value: "750,00", responsable: "Fulano", oncall: "Medico" },
    { id: "7", date: "07/04/2026", start: "7:00", duration: "12h", locale: "Unesp" , sector: "Sala de emergências", value: "1500,00", responsable: "Fulano2", oncall: "Medico" },
    { id: "8", date: "07/04/2026", start: "7:00", duration: "12h", locale: "Unesp" , sector: "Sala de emergências", value: "1500,00", responsable: "Fulano2", oncall: "Medico" },
    { id: "9", date: "07/04/2026", start: "7:00", duration: "12h", locale: "Unesp" , sector: "Sala de emergências", value: "1500,00", responsable: "Fulano2", oncall: "Medico" },
    { id: "10", date: "08/04/2026", start: "7:00", duration: "24h", locale: "Unesp" , sector: "Pronto socorro", value: "1500,00", responsable: "Fulano2", oncall: "Medico" },
]