import { View, Text, Dimensions } from "react-native";
import { LineChart } from "react-native-gifted-charts"

import { styles } from "./styles";
import { colors } from "@/styles/colors";

export function ScreenFinancial() {

    const screenWidth = Dimensions.get('window').width
    const data = [ 
        {value: 50, label: "Dom"}, 
        {value: 80, label: "Seg"}, 
        {value: 90, label: "Ter"}, 
        {value: 70, label: "Qua"}, 
        {value: 50, label: "Qui"}, 
        {value: 20, label: "Sex"},
        {value: 40, label: "Sab"},
    ]

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
                <View style={styles.row}>
                    <View style={styles.lineChart}>
                        <Text style={styles.title}>Gráfico de ganhos</Text>
                        <LineChart 
                        data={data}
                        color={colors.blue[400]}
                        thickness={3}

                        hideDataPoints={false}
                        dataPointsColor={colors.blue[400]}
                        dataPointsRadius={3}

                        areaChart
                        startFillColor={colors.blue[400]}
                        endFillColor={colors.gray[700]}
                        startOpacity={0.8}
                        endOpacity={0.2}

                        xAxisColor={colors.gray[100]}
                        yAxisColor={colors.gray[100]}
                        xAxisThickness={1}
                        yAxisThickness={1}

                        xAxisLabelTextStyle={{color: colors.blue[400], fontFamily: "Poppins-Bold", fontSize: 12}}
                        yAxisTextStyle={{color: colors.gray[100], fontFamily: "Poppins-Regular", fontSize: 12}}
                        hideYAxisText

                        rulesColor={colors.gray[500]}

                        width={screenWidth - 100}
                        height={175}
                        />
                    </View>
                </View>
            </View>
        </View>
    )
}