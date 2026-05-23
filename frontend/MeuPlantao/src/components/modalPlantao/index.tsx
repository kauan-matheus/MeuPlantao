import { View, Text, Alert } from "react-native"
import MapView, { UrlTile } from "react-native-maps";

import { styles } from "./styles";
import { About } from "../about";
import { Button } from "../button";
import { colors } from "@/styles/colors";
import { Plantao } from "@/utils/objects";
import { requestPlantoes } from "@/services/plantao";

type Props = {
    item: Plantao
}

export function ModalPlantao({item}: Props) {

    const handleSolicitacao = async () => {
        try{
            await requestPlantoes(Number(item.id))

            Alert.alert("Solicitação efetuada")
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
    
    return (
        <View style={styles.container}>
            <Text style={styles.title}>
                {
                    item.onDuty !== "Disponivel" ? "DESEJA SOLICITAR UMA TROCA?" : "DESEJA SOLICITAR ESTE PLANTÃO?"
                }
            </Text>
            <View style={styles.map}>
                <MapView
                style={{ flex: 1 }}
                initialRegion={{
                    latitude: -23.5505,
                    longitude: -46.6333,
                    latitudeDelta: 0.05,
                    longitudeDelta: 0.05,
                }}
                >
                <UrlTile
                    urlTemplate="https://tile.openstreetmap.org/{z}/{x}/{y}.png"
                    maximumZ={19}
                    />
                </MapView>
            </View>
            <View style={styles.data}>
                <View style={styles.row}>
                    <View style={styles.col}>
                        <About title="Local" text={item.locale} />
                        <About title="Responsável" text={item.responsable} />
                    </View>
                    <View style={styles.col}>
                        <About title="Setor" text={item.sector} />
                    </View>
                </View>
                <View style={styles.row}>
                    <View style={styles.col}>
                        <About title="Data" text={item.date} />
                        <About title="Valor" text={"R$ " + item.value} />
                    </View>
                    <View style={styles.col}>
                        <About title="Horário" text={item.start} />
                    </View>
                    <View style={styles.col}>
                        <About title="Tempo de plantão" text={item.duration + " horas"} />
                    </View>
                </View>
                <View style={styles.row}>
                    <View style={styles.col}>
                        <About title="Status" text={item.onDuty !== "Disponivel" ? "Reservado por " + item.onDuty : item.onDuty} />
                    </View>
                </View>
            </View>
            <Button text="SOLICITAR" color={colors.blue[500]} textColor={colors.gray[700]} onPress={handleSolicitacao}/>
        </View>
    )
}