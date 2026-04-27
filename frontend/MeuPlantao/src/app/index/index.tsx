import { Text, View, TouchableOpacity, ImageBackground, Modal, Animated, ActivityIndicator, TouchableWithoutFeedback, Keyboard, KeyboardAvoidingView, Platform, Touchable } from "react-native"
import { Ionicons } from "@expo/vector-icons"
import { useEffect, useRef, useState } from "react"
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
    const [name, setName] = useState("")
    const [document, setDocument] = useState("")
    const [telephone, setTelephone] = useState("")
    const [confirmEmail, setConfirmEmail] = useState("")
    const [confirmPassword, setConfirmPassword] = useState("")
    const [option, setOption] = useState<"CRM" | "Coren">("CRM")

    const [showModalLogin, setShowModalLogin] = useState(false)
    const [showModalRegister, setShowModalRegister] = useState(false)
    const [loading, setLoading] = useState(false)
    const [error, setError] = useState("")

    const opacity = useRef(new Animated.Value(0)).current;

    const [fonts] = useFonts({
        'Poppins-Regular': require('@/assets/fonts/Poppins-Regular.ttf'),
        'Poppins-Bold': require('@/assets/fonts/Poppins-Bold.ttf'),
    })

    if (!fonts) {
        return null;
    }

    const showError = (message: string) => {
        setError(message)

        Animated.timing(opacity, {
            toValue: 1,
            duration: 100,
            useNativeDriver: true,
        }).start()

        setTimeout(() => {
            Animated.timing(opacity, {
                toValue: 0,
                duration: 100,
                useNativeDriver: true,
            }).start(() => setError(""));
        }, 8000)
    }

    function reset() {
        setError(""),
        setName(""),
        setDocument(""),
        setTelephone(""),
        setEmail(""),
        setPassword(""),
        setConfirmEmail(""),
        setConfirmPassword(""),
        setOption("CRM")
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
                onPress={() => setShowModalLogin(true)}
                />
            </View>
            
            <Modal transparent visible={showModalLogin} animationType="slide">
                <View style={styles.modalLogin}>
                    <TouchableWithoutFeedback onPress={() => (setShowModalLogin(false), reset())}
                        >
                        <View style={{flex: 1}}></View>
                    </TouchableWithoutFeedback>
                    <TouchableWithoutFeedback onPress={Keyboard.dismiss}>
                        <KeyboardAvoidingView behavior={Platform.OS === "ios" ? "padding" : "height"} style={styles.modalContentLogin}>
                            <Text style={styles.titleModal}>LOGIN</Text>

                            <View style={styles.form}>
                                <View style={{height: 28}}>
                                    {loading && error === "" && (
                                        <ActivityIndicator size="small" color={colors.blue[400]} />
                                    )}
                                    {error !== "" && (
                                        <Animated.View style={[styles.error, {opacity}]}>
                                            <Ionicons
                                            name="alert-circle-outline"
                                            size={20}
                                            color={colors.red[300]}
                                            />
                                            <Text style={styles.textError}>{error}</Text>
                                        </Animated.View>
                                    )}
                                </View>
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
                                        showError(type === "string" ? data.message : data.message[0])
                                    }
                                }}
                                />
                            </View>

                            <TouchableOpacity activeOpacity={0.9} onPress={() => (setShowModalRegister(true), setShowModalLogin(false), reset())}>
                                <Text style={styles.link}>Não tem uma conta? Cadastre-se</Text>
                            </TouchableOpacity>
                        </KeyboardAvoidingView>
                    </TouchableWithoutFeedback>
                </View>
            </Modal>

            <Modal transparent visible={showModalRegister} animationType="slide">
                <View style={styles.modalRegister}>
                    <TouchableWithoutFeedback onPress={Keyboard.dismiss}>
                        <KeyboardAvoidingView behavior={Platform.OS === "ios" ? "padding" : "height"} style={styles.modalContentRegister}>
                            <TouchableOpacity style={styles.close} activeOpacity={0.7} onPress={() => (setShowModalRegister(false), reset())}>
                                <Ionicons
                                name="close"
                                size={20}
                                color={colors.gray[500]}
                            />
                            </TouchableOpacity>
                            <Text style={styles.titleModal}>CADASTRE-SE</Text>

                            <View style={styles.form}>
                                <View style={{height: 28}}>
                                    {loading && error === "" && (
                                        <ActivityIndicator size="small" color={colors.blue[400]} />
                                    )}
                                    {error !== "" && (
                                        <Animated.View style={[styles.error, {opacity}]}>
                                            <Ionicons
                                            name="alert-circle-outline"
                                            size={20}
                                            color={colors.red[300]}
                                            />
                                            <Text style={styles.textError}>{error}</Text>
                                        </Animated.View>
                                    )}
                                </View>
                                <View style={styles.formGroup}>
                                    <View style={styles.optionsDocument}>
                                        <TouchableOpacity style={[styles.option, {borderBottomWidth: option === "CRM" ? 3 : 0}]} activeOpacity={0.9} onPress={() => setOption("CRM")}>
                                            <Text style={styles.optionText}>Médico</Text>
                                        </TouchableOpacity>
                                        <TouchableOpacity style={[styles.option, {borderBottomWidth: option === "Coren" ? 3 : 0}]} activeOpacity={0.9} onPress={() => setOption("Coren")}>
                                            <Text style={styles.optionText}>Enfermeiro</Text>
                                        </TouchableOpacity>
                                    </View>
                                    <Input
                                    type="text"
                                    placeholder="Nome"
                                    onChangeText={setName}
                                    />
                                </View>
                                <Input
                                type="text"
                                placeholder={option}
                                onChangeText={setDocument}
                                />
                                <Input
                                type="text"
                                placeholder="Telefone"
                                onChangeText={setTelephone}
                                />
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
                                <Input
                                type="text"
                                placeholder="Confirme seu email"
                                onChangeText={setConfirmEmail}
                                />
                                <Input
                                type="password"
                                placeholder="Confirm sua senha"
                                onChangeText={setConfirmPassword}
                                />
                                <Button 
                                text="Enviar"
                                color={colors.blue[500]}
                                textColor={colors.gray[700]}
                                onPress={() => null}
                                />
                            </View>
                        </KeyboardAvoidingView>
                    </TouchableWithoutFeedback>
                </View>
            </Modal>  
        </ImageBackground>
    )
}