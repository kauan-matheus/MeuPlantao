import { View, Text, TouchableOpacity, TouchableOpacityProps } from "react-native";
import { styles } from "./styles";
import { colors } from "@/styles/colors";

type Props = TouchableOpacityProps & {
    date: string
    start: string
    duration: number
    locale: string
    sector: string
    oncall?: string
    onDetails: () => void
}

export function PlantaoItem({date, start, duration, locale, sector, oncall, onDetails, ...rest}: Props) {
    return (
        <TouchableOpacity style={styles.container} activeOpacity={0.7} onPress={onDetails}>
            <View style={styles.nav}>
                <View>
                    <Text style={styles.title}>{locale}</Text>
                    <Text style={styles.subTitle}>{sector}</Text>
                </View>
                <View>
                    <Text style={[styles.status, {backgroundColor: oncall ? colors.red[100] : colors.blue[500]}]}>
                        {oncall ? "Reservado" : "Disponível"}
                    </Text>
                </View>
            </View>
            <View style={styles.info}>
                <Text style={styles.textInfo}>Data: {date}</Text>
                <Text style={styles.textInfo}>Horário: {start}</Text>
                <Text style={styles.textInfo}>Duração: {duration}h</Text>
            </View>
        </TouchableOpacity>
    )
}