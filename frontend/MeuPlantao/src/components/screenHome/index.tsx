import { View, Text } from "react-native";

import { styles } from "./styles";

export function ScreenHome() {
    return (
        <View style={styles.container}>
            <Text>Rascunho da Home:</Text>
            <View>
                <Text>- Plantão em andamento</Text>
            </View>
            <View>
                <Text>- Próximos 3?</Text>
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