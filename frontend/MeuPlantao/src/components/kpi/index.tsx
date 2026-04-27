import { Text, TouchableOpacity, TouchableOpacityProps } from "react-native";

import { styles } from "./styles";

type Props = TouchableOpacityProps & {
    value: string
    text: string
    color: string
}

export function Kpi({value, text, color, ...rest}: Props) {
    return (
        <TouchableOpacity style={styles.container} activeOpacity={0.9} {...rest}>
            <Text style={[styles.value, {backgroundColor: color}]}>{value}</Text>
            <Text style={styles.text}>{text}</Text>
        </TouchableOpacity>
    )
}