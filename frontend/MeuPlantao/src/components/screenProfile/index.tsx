import { View, Text } from "react-native";

import { styles } from "./styles";
import { Button } from "../button";
import { colors } from "@/styles/colors";
import { router } from "expo-router";

export function ScreenProfile() {
    return (
        <View style={styles.container}>
            <Text>Rascunho da Profile:</Text>
            <View>
                <Text>- Foto de perfil</Text>
            </View>
            <View>
                <Text>- Dados do usuário</Text>
            </View>
            <Button
            text="Sair da conta"
            color={colors.red[200]}
            textColor={colors.gray[600]}
            onPress={() => router.back()}
            />
        </View>
    )
}