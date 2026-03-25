import { Text, View, TouchableOpacity, ImageBackground, Modal } from "react-native"
import { Ionicons } from "@expo/vector-icons"
import { useState } from "react"
import { router } from "expo-router"

import { styles } from "./styles"
import { colors } from "@/styles/colors"


import { Input } from "@/components/input/input"
import { Button } from "@/components/button"

export default function Index() {

    const [showModal, setShowModal] = useState(false)

    return (
        <ImageBackground 
        source={require("@/assets/images/background.png")} 
        style={styles.background} 
        resizeMode="cover"
        >
            <View style={styles.container}>
                {/* <Text style={styles.title}>MEU PLANTÃO</Text> */}
                <Button 
                text="Acessar "
                text2="Meu Plantão"
                color={colors.gray[600]}
                textColor={colors.gray[200]}
                textColor2={colors.blue[400]}
                onPress={() => setShowModal(true)}
                />
            </View>
            
            <Modal transparent visible={showModal} animationType="slide">
                <View style={styles.modal}>
                    <View style={styles.modalContent}>
                        <TouchableOpacity style={styles.close} activeOpacity={0.7} onPress={() => setShowModal(false)}>
                            <Ionicons
                            name="close"
                            size={20}
                            color={colors.gray[500]}
                        />
                        </TouchableOpacity>
                        <Text style={styles.titleModal}>LOGIN</Text>
                        {/* <Image source={require("@/assets/images/logo.png")} style={styles.logo} /> */}

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
                            <Button 
                            text="Entrar"
                            color={colors.blue[500]}
                            textColor={colors.gray[600]}
                            onPress={() => router.navigate("./interfaceUser")}
                            />
                        </View>

                        <TouchableOpacity activeOpacity={0.9}>
                            <Text style={styles.link}>Não tem uma conta? Cadastre-se</Text>
                        </TouchableOpacity>
                    </View>
                </View>
            </Modal>
        </ImageBackground>
    )
}