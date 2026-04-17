import { View, Text} from "react-native";

import { styles } from "./styles";

import { ListPlantao } from "../listPlantao";
import { Plantao, plantoes } from "@/utils/plantoes";
import { Input } from "../input/input";
import { useFocusEffect } from "expo-router";
import { useCallback, useState } from "react";
import { Kpi } from "../kpi";
import { colors } from "@/styles/colors";

export function ScreenHome() {

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
            <View style={styles.card}>
                <Text style={styles.title}>Em andamento</Text>
                <ListPlantao plantao={plantoes.slice(0, 0)} showFooter={false} isEmpty="Não há plantão em andamento" />
                {/* <Text style={styles.title}>Próximos</Text>
                <ListPlantao plantao={plantoes.slice(1, 4)} showFooter={false} isEmpty="Não há próximos plantões" /> */}
            </View>
            <View style={styles.kpis}>
                <Kpi value="3" text="Solicitados" color={colors.red[300]} />
                <Kpi value="2" text="Pendentes" color={colors.yellow[200]} />
                <Kpi value="4" text="Concluídos" color={colors.green[100]} />
                <Kpi value="9" text="Total" color={colors.blue[400]} />
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
                <ListPlantao plantao={plantao} showFooter={true} isEmpty="Sem histórico de plantões" />
            </View>
            {/* <View style={styles.card}>
                <Text style={styles.title}>Controle Financeiro</Text>
                <View style={styles.row}>
                    <View style={styles.col}>
                        <Text style={styles.textValue}>R$ 1.000,00</Text>
                        <Text style={styles.title}>Receita</Text>
                    </View>
                    <View style={styles.col}>
                        <Text style={styles.textValue}>R$ 1.000,00</Text>
                        <Text style={styles.title}>A receber</Text>
                    </View>
                </View>
                <View style={styles.row}>
                    <View style={styles.col}>
                        <Text style={styles.textValue}>R$ 1.000,00</Text>
                        <Text style={styles.title}>Plantões</Text>
                    </View>
                    <View style={styles.col}>
                        <Text style={styles.textValue}>R$ 1.000,00</Text>
                        <Text style={styles.title}>Ticket médio</Text>
                    </View>
                </View>
            </View> */}
        </View>
    )
}