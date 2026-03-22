import { Text, TouchableOpacity, TouchableOpacityProps } from "react-native";
import { styles } from "./style";


type Props = TouchableOpacityProps & {
    text: string
    text2?:  string
    color: string
    textColor: string
    textColor2?: string
}

export function Button({ text, text2, color, textColor, textColor2, ...rest }: Props) {
    return (
        <TouchableOpacity style={[styles.container, {backgroundColor: color}]} activeOpacity={0.9} {...rest}>
            <Text style={[styles.title, {color: textColor}]}>{text}</Text>
            <Text style={[styles.title, {color: textColor2, fontWeight: "bold"}]}>{text2}</Text>
        </TouchableOpacity>
    )
}