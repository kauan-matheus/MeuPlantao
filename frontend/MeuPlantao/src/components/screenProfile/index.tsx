import { View, Text } from "react-native";

import { styles } from "./styles";
import { Button } from "../button";
import { colors } from "@/styles/colors";
import { router } from "expo-router";

export function ScreenProfile() {
    return (
        <View style={styles.container}>
            <Button
            text="Sair da conta"
            color={colors.red[200]}
            textColor={colors.gray[600]}
            onPress={() => router.back()}
            />
        </View>
    )
}