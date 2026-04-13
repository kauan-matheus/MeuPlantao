import { View, Text } from "react-native";
import { useState, useCallback } from "react";
import { useFocusEffect } from "expo-router";

import { styles } from "./styles";
import { colors } from "@/styles/colors";

import { Input } from "../input/input";
import { ListPlantao } from "../listPlantao";
import { Kpi } from "../kpi";

import { plantoes, Plantao } from "@/utils/plantoes";

export function ScreenHistory() {

    const [search, setSearch] = useState("")
    const [plantao, setPlantao] = useState<Plantao[]>([])

    function getPlantao() {
        const filtered = plantoes.filter(p => (p.locale.toUpperCase().includes(search.toUpperCase()) || p.sector.toUpperCase().includes(search.toUpperCase())))

        setPlantao(filtered)
    }

    useFocusEffect(
        useCallback(() => {
            getPlantao()
        }, [search])
    )

    return (
        <View style={styles.container}>
            <View style={styles.kpis}>
                <Kpi value="3" text="Solicitados" color={colors.red[300]} />
                <Kpi value="2" text="Pendentes" color={colors.yellow[200]} />
                <Kpi value="4" text="Concluídos" color={colors.green[100]} />
                <Kpi value="9" text="Total" color={colors.blue[400]} />
            </View>
            <View style={styles.list}>
                <Input 
                type="text"
                icon="search-sharp"
                placeholder="Pesquisar"
                onChangeText={setSearch}
                />
                <ListPlantao plantao={plantao} showFooter={true} isEmpty="Sem histórico de plantões" />
            </View>
        </View>
    )
}