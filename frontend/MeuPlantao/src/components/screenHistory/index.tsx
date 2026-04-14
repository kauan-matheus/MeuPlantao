import { View, Text } from "react-native";

import { styles } from "./styles";

export function ScreenHistory() {
    return (
        <View style={styles.container}>
            <Text>Rascunho da History:</Text>
            <View>
                <Text>- KPIs?</Text>
            </View>
            <View>
                <Text>- Histórico de plantões</Text>
            </View>
        </View>
    )
}