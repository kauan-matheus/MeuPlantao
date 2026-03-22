import { Text, View, Image, TouchableOpacity, ImageBackground } from "react-native"

import { styles } from "./index/styles"
import { colors } from "@/styles/colors"

import { Input } from "@/components/input/input"
import { Button } from "@/components/button"

export default function Index() {

    
    return (
        <ImageBackground 
        source={require("@/assets/images/background.png")} 
        style={styles.container} 
        resizeMode="cover"
        >
            <View style={styles.modalLogin}>
                <Text style={styles.title}>MEU PLANTÃO</Text>
                <Image source={require("@/assets/images/logo.png")} style={styles.logo} />

                <View style={styles.form}>
                    <Input
                    type="text"
                    icon="person"
                    placeholder="Nome de Usuário"
                    placeholderTextColor={colors.gray[200]}
                    onChangeText={console.log}
                    />
                    <Input
                    type="password"
                    icon="pencil"
                    placeholder="Senha"
                    placeholderTextColor={colors.gray[200]}
                    onChangeText={console.log}
                    />
                    <Button title="Entrar" />
                </View>

                <TouchableOpacity activeOpacity={0.9}>
                    <Text style={styles.link}>Não tem uma conta? Cadastre-se</Text>
                </TouchableOpacity>
            </View>
        </ImageBackground>
    )
}