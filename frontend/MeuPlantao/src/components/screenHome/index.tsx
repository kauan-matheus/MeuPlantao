import { View, Text } from "react-native";

import { styles } from "./styles";

import { ListPlantao } from "../listPlantao";
import { plantoes } from "@/utils/plantoes";

export function ScreenHome() {
    return (
        <View style={styles.container}>
            <View style={styles.inProgress}>
                <Text style={styles.title}>Em andamento</Text>
                <ListPlantao plantao={plantoes.slice(0, 0)} showFooter={false} isEmpty="Não há plantão em andamento" />
                <Text style={styles.title}>Próximos</Text>
                <ListPlantao plantao={plantoes.slice(1, 4)} showFooter={false} isEmpty="Não há próximos plantões" />
            </View>
            <View>
                <Text>- Dashboard?</Text>
                <Text>  - KPIs?</Text>
                <Text>  - Controle Financeiro?</Text>
            </View>
            <View>
                <Text>- Encher linguiça?</Text>
            </View>
        </View>
    )
}