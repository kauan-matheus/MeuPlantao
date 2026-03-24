import { Ionicons } from "@expo/vector-icons"
import { Pressable, PressableProps, } from "react-native"

import { colors } from "@/styles/colors"
import { styles } from "./styles"

type Props = PressableProps & {
    icon: keyof typeof Ionicons.glyphMap
    isSelected?: boolean
}

export function NavLink({icon, isSelected, ...rest}: Props) {
    return (
        <Pressable style={[styles.container, {backgroundColor: isSelected ? colors.blue[500] : "transparent"}]} {...rest}>
            <Ionicons
            name={icon}
            size={30}
            color={isSelected ? colors.gray[700] : colors.gray[300]}
            />
        </Pressable>
    )
}