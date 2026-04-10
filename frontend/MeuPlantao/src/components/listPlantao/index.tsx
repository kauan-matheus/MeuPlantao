import { FlatList, Text, View, Modal, TouchableOpacity } from "react-native"
import { Ionicons } from "@expo/vector-icons"
import { useState } from "react"

import { styles } from "./styles"
import { colors } from "@/styles/colors"

import { PlantaoItem } from "../plantaoItem"
import { Plantao } from "@/utils/plantoes"

type Props = {
    plantao: Plantao[]
}

export function ListPlantao({plantao}: Props) {

    const [showModal, setShowModal] = useState(false)

    return (
        <>
            <FlatList
            data={plantao}
            keyExtractor={(item) => item.id}
            renderItem={({ item }) => (
                <PlantaoItem 
                date={item.date}
                start={item.start}
                duration={item.duration}
                locale={item.locale}
                sector={item.sector}
                oncall={item.oncall}
                onDetails={() => setShowModal(true)}
                />
            )}
            style={styles.listContent}
            ListEmptyComponent={() => (
                <Text style={styles.textListEmpty}>Nenhum plantão disponível</Text>
            )}
            ListFooterComponent={() => (
                <View style={styles.footer}></View>
            )}
            showsVerticalScrollIndicator={false}
            />

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
                    </View>
                </View>
            </Modal>  
        </>
    )
}