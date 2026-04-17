import { View, Text } from "react-native";

import { styles } from "./styles";

export function ScreenFinancial() {

    return (
        <View style={styles.container}>
            <View style={styles.card}>
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
            </View>
        </View>
    )
}