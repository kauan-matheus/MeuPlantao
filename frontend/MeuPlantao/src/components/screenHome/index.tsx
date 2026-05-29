import { View, Text, Alert} from "react-native";

import { styles } from "./styles";

import { ListPlantao } from "../listPlantao";
import { Input } from "../input/input";
import { useFocusEffect } from "expo-router";
import { useCallback, useEffect, useState } from "react";
import { Kpi } from "../kpi";
import { colors } from "@/styles/colors";
import { getProfessionalPlantoes, getProfessionalPlantoesSolicitados } from "@/services/professional";
import { Plantao } from "@/utils/objects";

export function ScreenHome() {

    const [search, setSearch] = useState("")
    const [plantoesSolicitados, setPlantoesSolicitados] = useState<Plantao[]>([])
    const [profissionalPlantoes, setProfissionalPlantoes] = useState<Plantao[]>([])
    const [plantoes, setPlantoes] = useState<Plantao[]>([])

    const fetchData = async () => {
        try{
            const response1 = await getProfessionalPlantoesSolicitados()
            const response2 = await getProfessionalPlantoes()

            setPlantoesSolicitados(response1.result)
            setProfissionalPlantoes(response2.result)
            setPlantoes([...response1.result, ...response2.result])
        }catch (error: any) {
            if (error.response) {
                // erro vindo da API (400, 401, etc)
                console.log("Erro da API:", error.response.data)

                Alert.alert("Erro", JSON.stringify(error.response.data))
            } else {
                // erro de rede
                console.log("Erro geral:", error)
                Alert.alert("Erro de conexão")
            }
        }
    }

    useEffect(() => {
        fetchData()

        const interval = setInterval(fetchData, 3000); // atualiza a cada 3s

        return () => clearInterval(interval);
    }, [])

    return (
        <View style={styles.container}>
            <View style={styles.card}>
                <Text style={styles.title}>Em andamento</Text>
                <ListPlantao plantao={profissionalPlantoes.filter(p => (new Date(converterData(p.date)) > new Date))} showFooter={false} isEmpty="Não há plantão em andamento" />
            </View>
            <View style={styles.kpis}>
                <Kpi value={plantoesSolicitados.length.toString()} text="Solicitados" color={colors.red[300]} />
                <Kpi value={plantoes.filter(p => (new Date(converterData(p.date)) > new Date )).length.toString()} text="Pendentes" color={colors.yellow[200]} />
                <Kpi value={plantoes.filter(p => (new Date(converterData(p.date)) < new Date )).length.toString()} text="Concluídos" color={colors.green[100]} />
                <Kpi value={plantoes.length.toString()} text="Total" color={colors.blue[400]} />
            </View>
            <View style={styles.list}>
                <View style={{width: "100%", paddingBottom: 5}}>
                    <Text style={styles.title}>Meus plantões</Text>
                </View>
                <Input 
                    type="text"
                    icon="search-sharp"
                    placeholder="Pesquisar"
                    onChangeText={setSearch}
                />
                <ListPlantao plantao={plantoes.filter(p => (p.locale.toUpperCase().includes(search.toUpperCase()) || p.sector.toUpperCase().includes(search.toUpperCase())))} showFooter={true} isEmpty="Sem histórico de plantões" />
            </View>
        </View>
    )
}

function converterData(data: string): Date {
  const [dia, mes, ano] = data.split("/")

  return new Date(Number(ano), Number(mes) - 1, Number(dia))
}