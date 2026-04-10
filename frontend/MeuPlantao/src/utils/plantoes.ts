export type Plantao = {
    id: string
    date: string
    start: string
    duration: number
    locale: string
    sector: string
    value: string
    responsable: string
    oncall?: string
}

export const plantoes: Plantao[] = [
    { id: "1", date: new Date().toLocaleDateString("pt-br"), start: "23:00", duration: 6, locale: "Sta. Casa de Getulina" , sector: "Sala de emergências", value: "1500,00", responsable: "Fulano", oncall: "Medico" },
    { id: "2", date: "09/04/2026", start: "07:00", duration: 12, locale: "HBU Unimar" , sector: "Sala de emergências", value: "1500,00", responsable: "Fulano" },
    { id: "3", date: "09/04/2026", start: "07:00", duration: 12, locale: "Sta. Casa de Lins" , sector: "Sala de emergências", value: "1500,00", responsable: "Fulano" },
    { id: "4", date: "10/04/2026", start: "07:00", duration: 6, locale: "Unimar", sector: "Pronto socorro", value: "750,00", responsable: "Fulano", oncall: "Medico" },
    { id: "5", date: "10/04/2026", start: "07:00", duration: 6, locale: "Unimar", sector: "Pronto socorro", value: "750,00", responsable: "Fulano" },
    { id: "6", date: "10/04/2026", start: "07:00", duration: 6, locale: "Unimar", sector: "Pronto socorro", value: "750,00", responsable: "Fulano"},
    { id: "7", date: "09/04/2026", start: "07:00", duration: 6, locale: "Unesp" , sector: "Sala de emergências", value: "1500,00", responsable: "Fulano2", oncall: "Medico" },
    { id: "8", date: "10/04/2026", start: "07:00", duration: 6, locale: "Unesp" , sector: "Sala de emergências", value: "1500,00", responsable: "Fulano2"},
    { id: "9", date: "10/04/2026", start: "07:00", duration: 6, locale: "Unesp" , sector: "Sala de emergências", value: "1500,00", responsable: "Fulano2" },
    { id: "10", date: "08/04/2026", start: "07:00", duration: 6, locale: "Unesp" , sector: "Pronto socorro", value: "1500,00", responsable: "Fulano2", oncall: "Medico" },
    { id: "12", date: "09/04/2026", start: "07:00", duration: 6, locale: "Unesp" , sector: "Pronto socorro", value: "1500,00", responsable: "Fulano2", oncall: "Medico" },
    { id: "13", date: "09/04/2026", start: "07:00", duration: 6, locale: "Unesp" , sector: "Pronto socorro", value: "1500,00", responsable: "Fulano2" },
    { id: "14", date: "09/04/2026", start: "07:00", duration: 6, locale: "Unesp" , sector: "Pronto socorro", value: "1500,00", responsable: "Fulano2" },
    { id: "15", date: "09/04/2026", start: "07:00", duration: 6, locale: "Unesp" , sector: "Pronto socorro", value: "1500,00", responsable: "Fulano2" },
    { id: "16", date: "09/04/2026", start: "07:00", duration: 6, locale: "Unesp" , sector: "Pronto socorro", value: "1500,00", responsable: "Fulano2" },
    { id: "17", date: "09/04/2026", start: "07:00", duration: 6, locale: "Unesp" , sector: "Pronto socorro", value: "1500,00", responsable: "Fulano2" },
]