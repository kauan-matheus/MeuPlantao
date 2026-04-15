import { View, Text, Image } from "react-native";
import { useState } from "react";

import { styles } from "./styles";
import { Button } from "../button";
import { colors } from "@/styles/colors";
import { router } from "expo-router";

import { logout } from "@/services/user";

export function ScreenProfile() {

    const [loading, setLoading] = useState(false)

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
            onPress={async () => {
                if (loading) return
                
                setLoading(true)
                logout()
                setLoading(false)

                router.back()
            }}
            />
        </View>
    )
}