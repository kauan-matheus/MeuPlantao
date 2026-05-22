import { View, Text, TouchableOpacity, TouchableOpacityProps, Modal } from "react-native";
import { styles } from "./styles";
import { colors } from "@/styles/colors";
import { Ionicons } from "@expo/vector-icons"
import { useState } from "react"

import { ModalPlantao } from "../modalPlantao"
import { Plantao } from "@/utils/objects";

type Props = {
    item: Plantao
}

export function PlantaoItem({item}: Props) {
    const [showModal, setShowModal] = useState(false)

    return (
        <>
            <TouchableOpacity style={styles.container} activeOpacity={0.7} onPress={() => setShowModal(true)}>
                <View style={styles.nav}>
                    <View>
                        <Text style={styles.title}>{item.locale}</Text>
                        <Text style={styles.subTitle}>{item.sector}</Text>
                    </View>
                    <View>
                        <Text style={[styles.status, {backgroundColor: (item.onDuty !== "Disponivel") || (new Date(converterData(item.date)) < new Date) ? colors.red[300] : colors.blue[500]}]}>
                            {new Date(converterData(item.date)) > new Date ? item.onDuty !== "Disponivel" ? "Reservado" : "Disponível" : "Concluido"}
                        </Text>
                    </View>
                </View>
                <View style={styles.info}>
                    <Text style={styles.textInfo}>Data: {item.date}</Text>
                    <Text style={styles.textInfo}>Horário: {item.start}</Text>
                    <Text style={styles.textInfo}>Duração: {item.duration}h</Text>
                </View>
            </TouchableOpacity>
            <Modal transparent visible={showModal} animationType="slide">
                <View style={styles.modal}>
                    <View style={styles.modalContent}>
                        <TouchableOpacity style={styles.close} activeOpacity={0.7} onPress={() => setShowModal(false)}>
                            <Ionicons
                            name="close"
                            size={20}
                            color={colors.gray[500]}
                        />
                        </TouchableOpacity>
                        <ModalPlantao item={item} />
                    </View>
                </View>
            </Modal>  
        </>
    )
}

function converterData(data: string): Date {
  const [dia, mes, ano] = data.split("/")

  return new Date(Number(ano), Number(mes) - 1, Number(dia))
}