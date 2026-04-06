import { useState } from "react";
import { Ionicons } from "@expo/vector-icons"
import { colors } from "@/styles/colors";
import { TextInput, TextInputProps, View, TouchableOpacity } from "react-native";
import { styles } from "./styles";

type Props = TextInputProps & {
    type: "text" | "password"
}

export function Input({ type, ...rest }: Props) {

    const [showPassword, setShowPassword] = useState(false)

    return (
        <View style={styles.container}>
            <TextInput
            style={styles.input}
            placeholderTextColor={colors.gray[200]}
            secureTextEntry={type === "password" ? !showPassword : false}
            autoCorrect={false}
            autoCapitalize="none"
            {...rest}
            />

            {type === "password" && (
                <TouchableOpacity onPress={() => {showPassword ? setShowPassword(false) : setShowPassword(true)}} activeOpacity={0.8}>
                    <Ionicons
                    name={showPassword ? "eye" : "eye-off"}
                    size={20}
                    color={colors.gray[200]}
                    />
                </TouchableOpacity>
            )}
        </View>
    )
}