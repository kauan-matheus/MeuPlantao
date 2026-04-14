import { View, Text, Image } from "react-native";

import { styles } from "./styles";
import { Button } from "../button";
import { colors } from "@/styles/colors";
import { router } from "expo-router";

export function ScreenProfile() {
    return (
        <View style={styles.container}>
            <Image source={require("@/assets/images/profile.jpg")} style={styles.imageProfile} />
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