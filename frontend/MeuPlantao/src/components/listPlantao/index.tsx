import { FlatList, Text, View, Modal, TouchableOpacity } from "react-native"

import { styles } from "./styles"

import { PlantaoItem } from "../plantaoItem"
import { Plantao } from "@/utils/objects"


type Props = {
    plantao: Plantao[],
    showFooter: boolean,
    isEmpty: string
}

export function ListPlantao({plantao, showFooter, isEmpty}: Props) {

    return (
        <FlatList
        data={plantao}
        keyExtractor={(item) => item.id}
        renderItem={({ item }) => (
            <PlantaoItem 
            item={item}
            />
        )}
        style={styles.listContent}
        ListEmptyComponent={() => (
            <Text style={styles.textListEmpty}>{isEmpty}</Text>
        )}
        ListFooterComponent={() => showFooter && (
            <View style={styles.footer}></View>
        )}
        showsVerticalScrollIndicator={false}
        />
    )
}