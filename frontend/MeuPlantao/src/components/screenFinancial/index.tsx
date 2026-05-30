import { View, Text, Dimensions, Alert } from "react-native";
import { LineChart } from "react-native-gifted-charts"

import { styles } from "./styles";
import { colors } from "@/styles/colors";
import { useEffect, useState } from "react";
import { Plantao } from "@/utils/objects";
import { getProfessionalPlantoes, getProfessionalPlantoesSolicitados } from "@/services/professional";

export function ScreenFinancial() {

    const [plantoes, setPlantoes] = useState<Plantao[]>([])

    const screenWidth = Dimensions.get('window').width
    const data = [ 
        {value: plantoes.filter(p => (new Date(converterData(p.date)).getDay() == 0 )).reduce((total, plantao) => total + plantao.value, 0), label: "Dom"}, 
        {value: plantoes.filter(p => (new Date(converterData(p.date)).getDay() == 1 )).reduce((total, plantao) => total + plantao.value, 0), label: "Seg"}, 
        {value: plantoes.filter(p => (new Date(converterData(p.date)).getDay() == 2 )).reduce((total, plantao) => total + plantao.value, 0), label: "Ter"}, 
        {value: plantoes.filter(p => (new Date(converterData(p.date)).getDay() == 3 )).reduce((total, plantao) => total + plantao.value, 0), label: "Qua"}, 
        {value: plantoes.filter(p => (new Date(converterData(p.date)).getDay() == 4 )).reduce((total, plantao) => total + plantao.value, 0), label: "Qui"}, 
        {value: plantoes.filter(p => (new Date(converterData(p.date)).getDay() == 5 )).reduce((total, plantao) => total + plantao.value, 0), label: "Sex"},
        {value: plantoes.filter(p => (new Date(converterData(p.date)).getDay() == 6 )).reduce((total, plantao) => total + plantao.value, 0), label: "Sab"},
    ]

    const fetchData = async () => {
        try{
            const response = await getProfessionalPlantoes()

            setPlantoes(response.result)
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

    const receitaTotal = plantoes.reduce((total, plantao) => total + plantao.value, 0)
    const receitaPendente = plantoes.filter(p => (new Date(converterData(p.date)) >= new Date )).reduce((total, plantao) => total + plantao.value, 0)
    const receitaRecebido = plantoes.filter(p => (new Date(converterData(p.date)) < new Date )).reduce((total, plantao) => total + plantao.value, 0)
    const ticketMedio = plantoes.length > 0 ? receitaTotal / plantoes.length : 0

    useEffect(() => {
        fetchData()

        const interval = setInterval(fetchData, 3000); // atualiza a cada 3s

        return () => clearInterval(interval);
    }, [])

    return (
        <View style={styles.container}>
            <View style={styles.card}>
                <Text style={styles.title}>Controle Financeiro</Text>
                <View style={styles.row}>
                    <View style={styles.col}>
                        <Text style={styles.textValue}>R$ {receitaTotal}</Text>
                        <Text style={styles.title}>Receita</Text>
                    </View>
                    <View style={styles.col}>
                        <Text style={styles.textValue}>R$ {receitaPendente}</Text>
                        <Text style={styles.title}>A receber</Text>
                    </View>
                </View>
                <View style={styles.row}>
                    <View style={styles.col}>
                        <Text style={styles.textValue}>{plantoes.length}</Text>
                        <Text style={styles.title}>Plantões</Text>
                    </View>
                    <View style={styles.col}>
                        <Text style={styles.textValue}>R$ {ticketMedio}</Text>
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
                            curved

                            dataPointsColor={colors.blue[400]}
                            dataPointsRadius={4}

                            areaChart
                            startFillColor={colors.blue[400]}
                            endFillColor={colors.gray[700]}
                            startOpacity={0.25}
                            endOpacity={0.02}

                            xAxisColor={colors.gray[500]}
                            yAxisColor={colors.gray[500]}
                            xAxisThickness={1}
                            yAxisThickness={1}

                            xAxisLabelTextStyle={{color: colors.blue[400], fontFamily: "Poppins-Bold", fontSize: 12}}
                            yAxisTextStyle={{color: colors.gray[100], fontFamily: "Poppins-Regular", fontSize: 12}}
                            hideYAxisText

                            rulesColor="rgba(0, 0, 0, 0.05)"

                            width={screenWidth - 100}
                            height={175}

                            spacing={47}
                            initialSpacing={20}
                            endSpacing={20}

                            focusEnabled
                            showStripOnFocus
                            stripColor={colors.blue[400]}
                            stripWidth={2}

                            showDataPointOnFocus
                            focusedDataPointColor={colors.blue[400]}

                            animateOnDataChange
                            animationDuration={300}
                        />
                    </View>
                </View>
                <View style={styles.row}>
                    <View style={styles.col}>
                        <Text style={styles.title}>Recebido: </Text>
                        <Text style={styles.textValue}>R$ {receitaRecebido}</Text>
                    </View>
                    <View style={styles.col}>
                        <Text style={styles.title}>Pendete: </Text>
                        <Text style={styles.textValue}>R$ {receitaPendente}</Text>
                    </View>
                </View>
            </View>
        </View>
    )
}

function converterData(data: string): Date{
    const [dia, mes, ano] = data.split("/")

    return new Date(Number(ano), Number(mes) - 1, Number(dia))
}