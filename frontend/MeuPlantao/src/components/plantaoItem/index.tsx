import { View, Text, TouchableOpacity, TouchableOpacityProps } from "react-native";
import { styles } from "./styles";

type Props = TouchableOpacityProps & {
    date: string
    start: string
    duration: string
    locale: string
    sector: string
}

export function PlantaoItem({date, start, duration, locale, sector, ...rest}: Props) {
    return (
        <TouchableOpacity style={styles.container} activeOpacity={0.7}>
            <View>
                <Text style={styles.title}>{locale}</Text>
                <Text style={styles.subTitle}>{sector}</Text>
            </View>
            <View style={styles.info}>
                <Text style={styles.textInfo}>Data: {date}</Text>
                <Text style={styles.textInfo}>Horário: {start}</Text>
                <Text style={styles.textInfo}>Duração: {duration}</Text>
            </View>
        </TouchableOpacity>
    )
}