import { View, Text } from "react-native";

import { styles } from "./style";

type Props = {
    title?: string
    text?: string | null
}

export function About({title, text}: Props) {
    return (
        <View style={styles.container}>
            <Text style={styles.title}>{title}</Text>
            <Text style={styles.text}>{text}</Text>
        </View>
    )
}