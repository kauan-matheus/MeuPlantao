import { Text, View, TouchableOpacity, ImageBackground, Modal, Alert } from "react-native"
import { Ionicons } from "@expo/vector-icons"
import { useEffect, useState } from "react"
import { router } from "expo-router"
import { useFonts } from "expo-font"

import { styles } from "./styles"
import { colors } from "@/styles/colors"


import { Input } from "@/components/input/input"
import { Button } from "@/components/button"

import { login, getAuth } from "@/services/user"

export default function Index() {

    useEffect(() => {
        async function load() {
            const auth = await getAuth()
    
            if (auth) {
                router.navigate("./interfaceUser")
            }
        }
    
        load()
    }, [])

    const [email, setEmail] = useState("")
    const [password, setPassword] = useState("")

    const [showModal, setShowModal] = useState(false)
    const [loading, setLoading] = useState(false)

    const [fonts] = useFonts({
        'Poppins-Regular': require('@/assets/fonts/Poppins-Regular.ttf'),
        'Poppins-Bold': require('@/assets/fonts/Poppins-Bold.ttf'),
    })

    if (!fonts) {
        return null;
    }

    return (
        <ImageBackground 
        source={require("@/assets/images/background.png")} 
        style={styles.background} 
        resizeMode="cover"
        >
            <View style={styles.container}>
                <Button 
                text="Acessar "
                text2="Meu Plantão"
                color={colors.gray[700]}
                textColor={colors.gray[200]}
                textColor2={colors.blue[400]}
                onPress={() => setShowModal(true)}
                />
            </View>
            
            <Modal transparent visible={showModal} animationType="slide">
                <View style={styles.modal}>
                    <View style={styles.modalContent}>
                        <TouchableOpacity style={styles.close} activeOpacity={0.7} onPress={() => [setShowModal(false), setEmail(""), setPassword("")]}>
                            <Ionicons
                            name="close"
                            size={20}
                            color={colors.gray[500]}
                        />
                        </TouchableOpacity>
                        <Text style={styles.titleModal}>LOGIN</Text>

                        <View style={styles.form}>
                            <Input
                            type="text"
                            placeholder="Email"
                            onChangeText={setEmail}
                            />
                            <Input
                            type="password"
                            placeholder="Senha"
                            onChangeText={setPassword}
                            />
                            <Button 
                            text="Entrar"
                            color={colors.blue[500]}
                            textColor={colors.gray[700]}
                            onPress={async () => {
                                if (loading) return

                                setLoading(true)
                                const data = await login(email, password)
                                setLoading(false)

                                if (data.type === "success")
                                    router.navigate("./interfaceUser")
                                else {
                                    const type = typeof data.message
                                    Alert.alert("Falha no login:", type === "string" ? data.message : data.message.join("\n"))
                                }
                            }}
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